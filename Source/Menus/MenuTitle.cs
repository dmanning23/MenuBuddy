using System;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	///This is the title text for a menu screen.
	/// </summary>
	public class MenuTitle : Label
	{
		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuTitle(string text)
			: base(text)
		{
			FontSize = FontSize.Large;
			Horizontal = HorizontalAlignment.Center;
			Highlightable = false;
		}

		#endregion
	}
}