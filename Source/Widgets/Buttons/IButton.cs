using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the button object
	/// This is a widget that can be selected
	/// </summary>
	public interface IButton : IWidget, IScreenItemContainer
	{
		Vector2 Size { set; }

		/// <summary>
		/// A description of the function of the button.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		event EventHandler<PlayerIndexEventArgs> Selected;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		void OnSelect(PlayerIndex? playerIndex);
	}
}