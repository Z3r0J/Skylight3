﻿using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Game.Rooms;
using Skylight.API.Game.Rooms.Items.Floor;
using Skylight.API.Game.Users;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Rooms.Items.Floor;

internal abstract class StatefulFloorRoomItem<T>(IRoom room, int id, IUserInfo owner, T furniture, Point3D position, int direction)
	: FloorRoomItem<T>(room, id, owner, furniture, position, direction), IStatefulFloorRoomItem
	where T : IStatefulFloorFurniture
{
	public new IStatefulFloorFurniture Furniture => this.furniture;

	public abstract int State { get; }
}
