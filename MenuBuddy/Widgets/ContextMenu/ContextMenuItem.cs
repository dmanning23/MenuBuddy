using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// Data class representing a single item in a context menu, with an icon, label, and click handler.
	/// </summary>
	public class ContextMenuItem
	{
		/// <summary>
		/// The icon texture displayed next to the menu item.
		/// </summary>
		public Texture2D Icon { get; set; }

		/// <summary>
		/// The text label for this menu item.
		/// </summary>
		public string IconText { get; set; }

		/// <summary>
		/// The delegate invoked when this menu item is clicked.
		/// </summary>
		public ClickDelegate ClickEvent { get; set; }

		/// <summary>
		/// Initializes a new <see cref="ContextMenuItem"/> with the specified icon, text, and click handler.
		/// </summary>
		/// <param name="icon">The icon texture to display.</param>
		/// <param name="iconText">The text label for the item.</param>
		/// <param name="clickEvent">The delegate invoked when clicked.</param>
		public ContextMenuItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			Icon = icon;
			IconText = iconText;
			ClickEvent = clickEvent;
		}
	}
}
