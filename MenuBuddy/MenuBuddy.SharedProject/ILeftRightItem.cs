using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	///This is an item that the user can hit left/right on 
	/// </summary>
	public interface ILeftRightItem
	{
		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		event EventHandler OnLeft;

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		event EventHandler OnRight;

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