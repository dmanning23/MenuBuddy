using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Helper class represents a single entry in a touch screen. 
	/// </summary>
	public class TouchEntry : MenuEntry
	{
		#region Properties

		/// <summary>
		/// Whether or not the outline should be drawn around this touch entry
		/// </summary>
		public bool DrawOutline { get; set; }

		/// <summary>
		/// The image to draw on this button
		/// </summary>
		public Texture2D Image { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public TouchEntry(string text, bool drawOutline = true, bool messageBoxEntry = false)
			: base(text, messageBoxEntry)
		{
			DrawOutline = drawOutline;
		}

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public TouchEntry(string text, bool drawOutline, bool messageBoxEntry, Texture2D image)
			: this(text, drawOutline, messageBoxEntry)
		{
			Image = image;
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Draws the menu entry. This can be overridden to customize the appearance.
		/// </summary>
		public override void Draw(MenuScreen screen, Vector2 position, bool isSelected, GameClock gameTime)
		{
			//create a rect that takes into account the transition offset
			Rectangle offsetRect = new Rectangle((int)screen.XPositionWithOffset(ButtonRect.X),
				ButtonRect.Y,
				ButtonRect.Width, ButtonRect.Height);

			//Draw the outline
			if (DrawOutline)
			{
				DrawBackground(screen, offsetRect);
			}

			//Draw the image
			if (null != Image)
			{
				// Modify the alpha to fade text out during transitions.
				Color color = Color.White;
				float alpha = (screen.TransitionAlpha * 255.0f);
				color.A = Convert.ToByte(alpha);

				//draw the image
				DrawButtonImage(screen, color, offsetRect);
			}

			//draw the text
			if (!string.IsNullOrEmpty(Text))
			{
				Vector2 loc = new Vector2(
					offsetRect.Center.X,
					offsetRect.Center.Y - (GetHeight(screen) / 2f));

				DrawText(screen, loc, isSelected, gameTime);
			}
		}

		protected virtual void DrawButtonImage(GameScreen screen, Color color, Rectangle rect)
		{
			//get the center of the selection rect, subtract half the texture size
			Vector2 center = new Vector2(
				rect.Center.X - (Image.Bounds.Width / 2),
				rect.Center.Y - (Image.Bounds.Height / 2));

			//draw the image
			screen.ScreenManager.SpriteBatch.Draw(Image, center, color);
		}

		#endregion
	}
}