using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitiveBuddy;
using ResolutionBuddy;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This dude just helps out with some of the drawing tasks needed for menu buddies.
	/// </summary>
	public class DrawHelper : IDisposable
	{
		#region Properties

		public SpriteBatch SpriteBatch { get; set; }

		/// <summary>
		/// basic primitive for drawing the outline arounds touch buttons
		/// </summary>
		public Primitive Prim { get; set; }

		public Texture2D FadeBackgroundImage { get; set; }

		#endregion //Properties

		#region Methods

		public DrawHelper(ScreenManager screenManager)
		{
			SpriteBatch = screenManager.SpriteBatch;

			//init the basic primitive
			Prim = new Primitive(screenManager.GraphicsDevice, screenManager.SpriteBatch);
			Prim.Thickness = 5.0f;
			Prim.NumCircleSegments = 4;

			if (!string.IsNullOrEmpty(StyleSheet.FadeBackgroundImageResource))
			{
				FadeBackgroundImage = screenManager.Game.Content.Load<Texture2D>(StyleSheet.FadeBackgroundImageResource);
			}
		}

		/// <summary>
		/// Helper draws a translucent black fullscreen sprite, used for fading
		/// screens in and out, and for darkening the background behind popups.
		/// </summary>
		public void FadeBackground(float alpha)
		{
			if (null == FadeBackgroundImage)
			{
				BlackRect(alpha, Resolution.ScreenArea);
			}
			else
			{
				DrawRect(new Color(0.0f, 0.0f, 0.0f, alpha), Resolution.ScreenArea, FadeBackgroundImage);
			}
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void BlackRect(float alpha, Rectangle rect)
		{
			DrawRect(new Color(0.0f, 0.0f, 0.0f, alpha), rect);
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
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void DrawRect(IScreen screen, Color color, Rectangle rect, ITransitionObject transition, Texture2D tex)
		{
			//set the transition location
			rect.Location = transition.Position(screen, rect);

			//draw the filled background
			var screenTransition = transition.GetScreenTransition(screen);
			DrawRect(screenTransition.AlphaColor(color), rect, tex);
		}

		/// <summary>
		/// Draw a 
		/// </summary>
		public void DrawRect(IScreen screen, Color color, Rectangle rect, ITransitionObject transition)
		{
			//get the color for the background & border
			var screenTransition = transition.GetScreenTransition(screen);
			color.A = (byte)(color.A * screenTransition.Alpha);

			//set the transition location
			rect.Location = transition.Position(screen, rect);

			//draw the filled background
			DrawRect(screenTransition.AlphaColor(color), rect);
		}

		public void DrawOutline(IScreen screen, Color color, Rectangle rect, ITransitionObject transition, float lineWidth = 5f)
		{
			//set the transition location
			rect.Location = transition.Position(screen, rect);

			//draw the button outline
			var screenTransition = transition.GetScreenTransition(screen);
			DrawOutline(screenTransition.AlphaColor(color), rect, lineWidth);
		}

		public void DrawOutline(Color color, Rectangle rect, float lineWidth = 5f)
		{
			//draw the button outline
			Prim.Thickness = lineWidth;
			Prim.Rectangle(rect, color);
		}

		public void Dispose()
		{
			Prim?.Dispose();
			Prim = null;
		}

		#endregion //Methods
	}
}
