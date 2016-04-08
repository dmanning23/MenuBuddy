using System;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Custom event argument which includes the index of the player who triggered the event. 
	/// </summary>
	public class SelectedEventArgs : EventArgs
	{
		private readonly PlayerIndex? _playerIndex;

		/// <summary>
		/// Constructor.
		/// </summary>
		public SelectedEventArgs(PlayerIndex? playerIndex = null)
		{
			_playerIndex = playerIndex;
		}

		/// <summary>
		/// Gets the index of the player who triggered this event.
		/// </summary>
		public PlayerIndex? PlayerIndex
		{
			get { return _playerIndex; }
		}
	}
}