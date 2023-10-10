﻿using System.Net;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Net.Metadata;
using Net.Sockets.Listener;
using Skylight.API.DependencyInjection;
using Skylight.Protocol.Packets.Manager;
using Skylight.Server.Game.Clients;
using Skylight.Server.Net.Communication;
using Skylight.Server.Net.Handlers;

namespace Skylight.Server.Net;

internal sealed class NetworkManager
{
	internal static readonly MetadataKey<Client> GameClientMetadataKey = MetadataKey<Client>.Create("GameClient");

	private readonly IServiceProvider serviceProvider;

	private readonly ILogger<NetworkManager> logger;

	private readonly NetworkSettings settings;

	private readonly PacketManagerCache packetManagerCache;

	private readonly Lazy<ILoadableServiceManager> loadableServiceManager;

	public NetworkManager(IServiceProvider serviceProvider, ILogger<NetworkManager> logger, IOptions<NetworkSettings> settings, PacketManagerCache packetManagerCache, Lazy<ILoadableServiceManager> loadableServiceManager)
	{
		this.serviceProvider = serviceProvider;

		this.logger = logger;

		this.settings = settings.Value;

		this.packetManagerCache = packetManagerCache;

		this.loadableServiceManager = loadableServiceManager;
	}

	public void Start()
	{
		foreach (NetworkSettings.ListenerSettings listenerSettings in this.settings.Listeners)
		{
			if (!this.packetManagerCache.TryCreatePacketManager(listenerSettings.Revision, out Func<AbstractGamePacketManager>? packetManagerGetter))
			{
				this.logger.LogWarning($"Did not find a packet manager for revision {listenerSettings.Revision}.");

				continue;
			}

			foreach (string endPoint in listenerSettings.EndPoints)
			{
				if (IPEndPoint.TryParse(endPoint, out IPEndPoint? ipEndPoint))
				{
					this.logger.LogInformation($"Listening on {ipEndPoint}");

					IListener.CreateTcpListener(ipEndPoint, socket =>
					{
						this.loadableServiceManager.Value.WaitForInitialization().GetAwaiter().GetResult();

						socket.Metadata.Set(NetworkManager.GameClientMetadataKey, new Client(socket));

						socket.Pipeline.AddHandlerFirst(new LeftOverHandler());

						AbstractGamePacketManager packetManager = packetManagerGetter();
						if (packetManager.Modern)
						{
							socket.Pipeline.AddHandlerFirst(new HotSwapPacketHandler(packetManagerGetter));
							socket.Pipeline.AddHandlerFirst(FlashSocketPolicyRequestHandler.Instance);
						}
						else
						{
							socket.Pipeline.AddHandlerFirst(new Base64PacketHeaderHandler(this.serviceProvider.GetRequiredService<ILogger<Base64PacketHeaderHandler>>(), packetManager, BigInteger.Parse(listenerSettings.CryptoPrime!), BigInteger.Parse(listenerSettings.CryptoGenerator!)));
						}
					}, this.serviceProvider);
				}
			}
		}
	}
}
