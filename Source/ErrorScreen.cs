using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Specialized message box subclass, used to display network error messages.
	/// </summary>
	public class ErrorScreen : MessageBoxScreen
	{
		#region Methods

		/// <summary>
		/// Constructs an error message box from the specified exception.
		/// </summary>
		public ErrorScreen(Exception exception) : base(GetErrorMessage(exception), false)
		{
		}

		/// <summary>
		/// Converts a network exception into a user friendly error message.
		/// </summary>
		private static string GetErrorMessage(Exception exception)
		{
			//Trace.WriteLine("Network operation threw " + exception);

			// Otherwise just a generic error message.
			return "An unknown error occurred:\n" + exception.ToString();
		}

		/// <summary>
		/// Dont load any content for this screen, because a lot of the time the missing content will be the gradient texture.
		/// </summary>
		public override void LoadContent()
		{
		}

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			//Draw at the upper left to try and fit as much text as possible on teh screen
			Vector2 textPosition = new Vector2(ScreenRect.Left, ScreenRect.Top);

			ScreenManager.SpriteBatchBegin();

			// Darken down any other screens that were drawn beneath the popup.
			ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2.0f / 3.0f);

			// Draw the message box text.
			ScreenManager.SpriteBatch.DrawString(ScreenManager.MessageBoxFont, Message, textPosition, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 0.4f, SpriteEffects.None, 1.0f);

			ScreenManager.SpriteBatchEnd();
		}

		#endregion
	}
}