using Skylight.API.Game.Furniture.Floor;
using Skylight.API.Game.Rooms.Items.Floor;
using Skylight.API.Game.Rooms.Items.Interactions;
using Skylight.API.Game.Rooms.Private;
using Skylight.API.Game.Users;
using Skylight.API.Numerics;

namespace Skylight.Server.Game.Rooms.Items.Floor;

internal sealed class DiceRoomItem(
	IPrivateRoom room,
	int id,
	IUserInfo owner,
	Point3D location,
	int direction,
	IDiceFurniture furniture,
	IDiceInteractionManager handler
) : StatefulFloorRoomItem<IDiceFurniture>(room, id, owner, furniture, location, direction), IDiceRoomItem
{
	// Current state of the dice (e.g., number displayed or rolling)
	private int currentState;

	// Getter for State property
	public override int State => this.currentState;

	// Access to Furniture
	public new IDiceFurniture Furniture { get; } = furniture;
	public IDiceInteractionManager Handler { get; } = handler;

	/// <summary>
	/// Called when the dice is rolled. Updates the state and notifies the room.
	/// </summary>
	private void OnDiceRolled(int number)
	{
		this.currentState = number; // Update state to the rolled number

	}

	/// <summary>
	/// Handles logic when the dice is placed in the room.
	/// </summary>
	public void OnPlace(IDiceFurniture dice)
	{
		this.currentState = 0; // Default state when placed

		this.Handler.OnPlace(this); // Notify the interaction handler
	}

	/// <summary>
	/// Handles logic when the dice is removed from the room.
	/// </summary>
	public void OnRemove(IDiceFurniture dice)
	{
		this.currentState = -1; // Represent removed state
		this.Handler.OnRemove(this); // Notify the interaction handler
	}

	/// <summary>
	/// Simulates a dice roll with a random value between 1 and 6.
	/// </summary>
	public void RollDice()
	{
		// Simulate a rolling animation (optional)
		this.currentState = -1; // State indicating "rolling"

		this.Room.PostTask(_ =>
		{
			// Simulate a dice roll
			Random random = new();
			int number = random.Next(1, 7);
			// Update the state and notify the room
			this.OnDiceRolled(number);
		});
	}
}
