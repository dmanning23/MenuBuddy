using PrimitiveBuddy;
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
		public Primitive Prim { get; set; }

		#endregion //Properties

		#region Methods

		public DrawHelper(GraphicsDevice graphicsDevice, SpriteBatch spritebatch)
		{
			Debug.Assert(null != spritebatch);

			SpriteBatch = spritebatch;

			//init the basic primitive
			Prim = new Primitive(graphicsDevice, spritebatch);
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
		public void BlackRect(float fAlpha, Rectangle rect)
		{
			DrawRect(new Color(0.0f, 0.0f, 0.0f, fAlpha), rect);
		}

		/// <summary>
		/// Draw a solid colored rect at a specific location
		/// </summary>
		public void DrawRect(Color color, Rectangle rect)
		{
			DrawRect(color, rect, Prim.Texture);
		}

		/// <summary>
		/// straight up draw a rect at a location with no transitioning
		/// </summary>
		/// <param name="color"></param>
		/// <param name="rect"></param>
		/// <param name="tex"></param>
		public void DrawRect(Color color, Rectangle rect, Texture2D tex)
		{
			SpriteBatch.Draw(tex, rect, color);
		}

		/// <summary>
		/// Draw a 
		/// </summary>
		public void DrawRect(Color color, Rectangle rect, ScreenTransition screen, ITransitionObject transition)
		{
			//get the color for the background & border
			color.A = (byte)(color.A * screen.Alpha);

			//set the transition location
			rect.Location = transition.Position(screen, rect);

			//draw the filled background
			DrawRect(screen.AlphaColor(color), rect);
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void DrawRect(Color color, Rectangle rect, ScreenTransition screen, ITransitionObject transition, Texture2D tex)
		{
			//set the transition location
			rect.Location = transition.Position(screen, rect);

			//draw the filled background
			DrawRect(screen.AlphaColor(color), rect, tex);
		}

		public void DrawOutline(Color color, Rectangle rect, ScreenTransition screen, ITransitionObject transition)
		{
			//set the transition location
			rect.Location = transition.Position(screen, rect);

			//draw the button outline
			Prim.Rectangle(rect, screen.AlphaColor(color));
		}

		public void DrawOutline(Color color, Rectangle rect)
		{
			//draw the button outline
			Prim.Rectangle(rect, color);
		}

		#endregion //Methods
	}
}
