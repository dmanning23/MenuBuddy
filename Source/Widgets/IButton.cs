using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the button object
	/// This is a widget that can be selected
	/// </summary>
	public interface IButton : IWidget
	{
		/// <summary>
		/// A description of the function of the menu entry.
		/// </summary>
		string Description { get; }

		/// <summary>
		/// The text rendered for this entry.
		/// </summary>
		string Text { get; }

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		event EventHandler<PlayerIndexEventArgs> Selected;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		void OnSelect(PlayerIndex? playerIndex);

		/// <summary>
		/// This item is currently highlighted
		/// </summary>
		void OnHighlighted();

		/// <summary>
		/// This item is currently NOT highlighted
		/// </summary>
		void OnNotHighlighted();
	}
}