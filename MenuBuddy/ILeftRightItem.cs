using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for items that respond to left/right input actions.
	/// </summary>
	public interface ILeftRightItem
	{
		/// <summary>
		/// Event raised when the user presses left on this item.
		/// </summary>
		event EventHandler OnLeft;

		/// <summary>
		/// Event raised when the user presses right on this item.
		/// </summary>
		event EventHandler OnRight;

		/// <summary>
		/// Triggers the <see cref="OnLeft"/> event.
		/// </summary>
		void OnLeftEntry();

		/// <summary>
		/// Triggers the <see cref="OnRight"/> event.
		/// </summary>
		void OnRightEntry();
	}
}