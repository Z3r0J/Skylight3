using Skylight.API.Game.Furniture;
using Skylight.API.Game.Rooms.Items.Floor;
using Skylight.API.Game.Rooms.Items.Interactions;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Rooms.Items.Interactions;
internal sealed class DiceInteractionHandler : IDiceInteractionManager
{
	public bool CanPlaceItem(IFurniture furniture, Point2D location)
	{
		return true;
	}

	public IDiceRoomItem? Dice { get; private set; }

	public void OnPlace(IDiceRoomItem dice)
	{
		this.Dice = dice;
	}

	public void OnRemove(IDiceRoomItem dice)
	{
		if (this.Dice == dice)
		{
			this.Dice = null;
		}
	}
}
