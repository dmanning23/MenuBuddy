using InputHelper;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Delegate for handling click events on context menu items.
	/// </summary>
	/// <param name="obj">The source of the click event.</param>
	/// <param name="clickEvent">The click event arguments.</param>
	public delegate void ClickDelegate(object obj, ClickEventArgs clickEvent);

	/// <summary>
	/// Interface for a context menu that displays a list of actions when activated.
	/// </summary>
	public interface IContextMenu
	{
		/// <summary>
		/// Adds a menu item with an icon, label text, and click handler.
		/// </summary>
		/// <param name="icon">The icon texture to display next to the item.</param>
		/// <param name="iconText">The text label for the menu item.</param>
		/// <param name="clickEvent">The delegate invoked when the item is clicked.</param>
		void AddItem(Texture2D icon, string iconText, ClickDelegate clickEvent);
	}
}
