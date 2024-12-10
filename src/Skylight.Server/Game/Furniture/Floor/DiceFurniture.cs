using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Furniture.Floor;
internal sealed class DiceFurniture(
	int id,
	FloorFurnitureType type,
	Point2D dimensions,
	double height
	) : FixedHeightStatefulFloorFurniture(id, type, dimensions, height), IDiceFurniture

{
	public int DiceValue => -1;
}
