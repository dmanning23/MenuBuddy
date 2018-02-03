using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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
		public MenuTitle(string text, ContentManager content)
			: base(text, content, FontSize.Large)
		{
			Horizontal = HorizontalAlignment.Center;
			Highlightable = false;
		}

		#endregion
	}
}