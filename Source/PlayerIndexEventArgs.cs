using System;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Custom event argument which includes the index of the player who triggered the event. 
	/// This is used by the MenuEntry.Selected & Cancel event.
	/// </summary>
	public class PlayerIndexEventArgs : EventArgs
	{
		private readonly PlayerIndex playerIndex;

		/// <summary>
		/// Constructor.
		/// </summary>
		public PlayerIndexEventArgs(PlayerIndex playerIndex)
		{
			this.playerIndex = playerIndex;
		}

		/// <summary>
		/// Gets the index of the player who triggered this event.
		/// </summary>
		public PlayerIndex PlayerIndex
		{
			get { return playerIndex; }
		}
	}
}