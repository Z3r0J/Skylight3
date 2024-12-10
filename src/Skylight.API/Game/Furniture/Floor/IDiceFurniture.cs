namespace Skylight.API.Game.Furniture.Floor;

public interface IDiceFurniture : IStatefulFloorFurniture
{
	public int DiceValue { get; }
}
