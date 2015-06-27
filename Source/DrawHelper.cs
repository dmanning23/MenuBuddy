using BasicPrimitiveBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System.Diagnostics;

namespace MenuBuddy
{
	/// <summary>
	/// This dude just helps out with some of the drawing tasks needed for menu buddies.
	/// </summary>
	public class DrawHelper
	{
		#region Properties

		public SpriteBatch SpriteBatch { get; set; }

		/// <summary>
		/// basic primitive for drawing the outline arounds touch buttons
		/// </summary>
		public XnaBasicPrimitive Prim { get; set; }

		#endregion //Properties

		#region Methods

		public DrawHelper(GraphicsDevice graphicsDevice, SpriteBatch spritebatch)
		{
			Debug.Assert(null != spritebatch);

			SpriteBatch = spritebatch;

			//init the basic primitive
			Prim = new XnaBasicPrimitive(graphicsDevice, spritebatch);
			Prim.Thickness = 5.0f;
			Prim.NumCircleSegments = 4;
		}

		/// <summary>
		/// Helper draws a translucent black fullscreen sprite, used for fading
		/// screens in and out, and for darkening the background behind popups.
		/// </summary>
		public void FadeBackground(float fAlpha)
		{
			BlackRect(fAlpha, Resolution.ScreenArea);
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void DrawRect(Color color, Rectangle rect)
		{
			SpriteBatch.Draw(Prim.Texture, rect, color);
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void BlackRect(float fAlpha, Rectangle rect)
		{
			DrawRect(new Color(0.0f, 0.0f, 0.0f, fAlpha), rect);
		}

		public void DrawOutline(Transition transition, StyleSheet style, Rectangle rect)
		{
			if (style.HasOutline)
			{
				//get teh correct color
				Color borderColor = style.SelectedOutlineColor;
				borderColor.A = (byte)(255f * transition.Alpha);

				//set the transition location
				rect.Location = transition.Position(rect, style.Transition);

				//draw the button outline
				Prim.Rectangle(rect, borderColor);
			}
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void DrawBackground(Transition transition, StyleSheet style, Rectangle rect)
		{
			if (style.HasBackground)
			{
				//get the color for the background & border
				Color backgroundColor = style.SelectedBackgroundColor;
				backgroundColor.A = (byte)(backgroundColor.A * transition.Alpha);

				//set the transition location
				rect.Location = transition.Position(rect, style.Transition);

				//draw the filled background
				SpriteBatch.Draw(style.Texture, rect, backgroundColor);
			}
		}

		#endregion //Methods
	}
}
