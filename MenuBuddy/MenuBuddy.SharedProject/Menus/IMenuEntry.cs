using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Helper class represents a single entry in a MenuScreen. By default this
	/// just draws the entry text string, but it can be customized to display menu
	/// entries in different ways. This also provides an event that will be raised
	/// when the menu entry is selected.
	/// </summary>
	public interface IMenuEntry : IButton, ILabel
	{
		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		event EventHandler Left;

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		event EventHandler Right;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		void OnLeftEntry();

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		void OnRightEntry();
	}
}