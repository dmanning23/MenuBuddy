using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is a touch entry that the image fills the whole button
	/// </summary>
	public class TouchFillEntry : TouchEntry
	{
		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public TouchFillEntry(string text)
			: base(text)
		{
		}

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public TouchFillEntry(string text, Texture2D image)
			: base(text, image)
		{
		}

		#endregion

		#region Update and Draw

		protected override void DrawButtonImage(GameScreen screen, Color color, Rectangle rect)
		{
			//draw the image to fill the whole button
			screen.ScreenManager.SpriteBatch.Draw(Image, rect, color);
		}

		#endregion
	}
}