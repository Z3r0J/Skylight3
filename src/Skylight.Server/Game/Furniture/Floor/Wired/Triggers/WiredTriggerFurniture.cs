﻿using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Game.Furniture.Floor.Wired.Triggers;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Furniture.Floor.Wired.Triggers;

internal abstract class WiredTriggerFurniture(int id, FloorFurnitureType type, Point2D dimensions, double height) : FloorFurniture(id, type, dimensions), IWiredTriggerFurniture
{
	public override double DefaultHeight => height;
}
