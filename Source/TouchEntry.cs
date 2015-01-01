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
	public class TouchEntry
	{
		#region Properties

		/// <summary>
		/// Take everything into consideration: TouchMenus, CascadeMenuEntries, & TextSelectionRect
		/// the menu entry will set this button rect when it is updated.
		/// </summary>
		public Rectangle ButtonRect { get; set; }

		/// <summary>
		/// Whether or not the outline should be drawn around this touch entry
		/// </summary>
		public bool DrawOutline { get;set; }

		/// <summary>
		/// The image to draw on this button
		/// </summary>
		public Texture2D Image { get;set;}

		/// <summary>
		/// The text rendered for this entry.
		/// </summary>
		public string Text { get; set; }

		#endregion

		#region Events

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler<PlayerIndexEventArgs> Selected;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
		{
			if (Selected != null)
			{
				Selected(this, new PlayerIndexEventArgs(playerIndex));
			}
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public TouchEntry(string text, bool drawOutline = true)
		{
			Text = text;
			DrawOutline = drawOutline;
		}

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public TouchEntry(string text, Texture2D image, Rectangle rect, bool drawOutline = true)
		{
			Text = text;
			Image = image;
			ButtonRect = rect;
			DrawOutline = drawOutline;
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Draws the menu entry. This can be overridden to customize the appearance.
		/// </summary>
		public void Draw(GameScreen screen, FontBuddy font, GameClock gameTime)
		{
			// Draw the selected entry in yellow, otherwise white.
			Color color = Color.White;

			// Modify the alpha to fade text out during transitions.
			float alpha = (screen.TransitionAlpha * 255.0f);
			color.A = Convert.ToByte(alpha);
			Color backgroundColor = Color.Black;
			backgroundColor.A = Convert.ToByte(alpha);

			//Draw the outline
			if (DrawOutline)
			{
				//check if the mouse is over this entry
				float buttonAlpha = screen.TransitionAlpha * 0.5f;
				if (screen.ScreenManager.TouchMenus)
				{
					//get the mouse location
					if (ButtonHighlight(screen))
					{
						buttonAlpha = screen.TransitionAlpha * 0.2f;
					}
				}

				//draw the rect!
				screen.ScreenManager.DrawButtonBackground(buttonAlpha, ButtonRect);
			}

			//Draw the image
			if (null != Image)
			{
				//get the center of the selection rect, subtract half the texture size
				Vector2 center = new Vector2(
					ButtonRect.Center.X - (Image.Bounds.Width / 2),
					ButtonRect.Center.Y - (Image.Bounds.Height / 2));

				//draw the image
				screen.ScreenManager.SpriteBatch.Draw(Image, center, color);
			}

			//draw the text
			if (!string.IsNullOrEmpty(Text))
			{
				Vector2 loc = new Vector2(
					ButtonRect.Center.X,
					ButtonRect.Center.Y);
				font.Write(Text, loc, Justify.Center, 1.0f, color, screen.ScreenManager.SpriteBatch, gameTime.CurrentTime);
			}
		}

		public bool ButtonHighlight(GameScreen screen)
		{
			//Check if the mouse cursor is inside this menu entry
			if (ButtonRect.Contains(screen.ScreenManager.MousePos))
			{
				return true;
			}

			//check if the user is holding the thouch screen inside this menu entry
			if (null != screen.ScreenManager.Touch)
			{
				foreach (Vector2 touch in screen.ScreenManager.Touch.Touches)
				{
					if (ButtonRect.Contains(touch))
					{
						return true;
					}
				}
			}

			//nothing held in this dude
			return false;
		}

		#endregion
	}
}