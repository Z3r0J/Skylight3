using Skylight.API.Game.Rooms.Items.Floor;

namespace Skylight.API.Game.Rooms.Items.Interactions;
public interface IDiceInteractionManager : IRoomItemInteractionHandler
{
	public IDiceRoomItem? Dice { get; }

	public void OnPlace(IDiceRoomItem dice);
	public void OnRemove(IDiceRoomItem dice);
}
