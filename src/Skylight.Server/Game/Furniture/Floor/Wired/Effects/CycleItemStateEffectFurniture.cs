﻿using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Game.Furniture.Floor.Wired.Effects;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Furniture.Floor.Wired.Effects;

internal sealed class CycleItemStateEffectFurniture(int id, FloorFurnitureType type, Point2D dimensions, double height) : WiredEffectFurniture(id, type, dimensions, height), ICycleItemStateEffectFurniture;
