using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Game.Rooms.Items.Floor;
using Skylight.API.Game.Rooms.Items.Interactions;
using Skylight.Server.Game.Rooms.Items.Floor;

namespace Skylight.Server.Game.Rooms.Items.Builders.Floor;
internal class DiceRoomItemBuilder : FloorItemBuilder<IDiceFurniture,IDiceRoomItem,DiceRoomItemBuilder>
{
	public override IDiceRoomItem Build()
	{
		this.CheckValid();

		if (!this.RoomValue.ItemManager.TryGetInteractionHandler(out IDiceInteractionManager? handler))
		{
			throw new Exception($"{typeof(IDiceInteractionManager)} not found");
		}

		return new DiceRoomItem(this.RoomValue, this.IdValue, this.OwnerValue, this.PositionValue, this.DirectionValue,this.FurnitureValue, handler);
	}
}
