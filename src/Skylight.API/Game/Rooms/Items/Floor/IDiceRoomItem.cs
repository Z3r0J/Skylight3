using Skylight.API.Game.Furniture;
using Skylight.API.Game.Furniture.Floor;

namespace Skylight.API.Game.Rooms.Items.Floor;
public interface IDiceRoomItem : IStatefulFloorRoomItem, IFurnitureItem<IDiceFurniture>
{
	public new IDiceFurniture Furniture { get; }
	public void OnPlace(IDiceFurniture dice);
	public void OnRemove(IDiceFurniture dice);
	public void RollDice();
	IStatefulFloorFurniture IStatefulFloorRoomItem.Furniture => this.Furniture;
	IDiceFurniture IFurnitureItem<IDiceFurniture>.Furniture => this.Furniture;
}
