﻿using System.Text.Json;
using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Game.Rooms;
using Skylight.API.Game.Rooms.Items.Floor;
using Skylight.API.Game.Users;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Rooms.Items.Floor;

internal sealed class FurniMaticGiftRoomItem(IRoom room, int id, IUserInfo owner, IFurniMaticGiftFurniture furniture, Point3D position, int direction, DateTime recycledAt)
	: PlainFloorRoomItem<IFurniMaticGiftFurniture>(room, id, owner, furniture, position, direction), IFurniMaticGiftRoomItem
{
	public DateTime RecycledAt { get; } = recycledAt;

	public new IFurniMaticGiftFurniture Furniture => this.furniture;

	public JsonDocument GetExtraData() => JsonSerializer.SerializeToDocument(this.RecycledAt);
}
