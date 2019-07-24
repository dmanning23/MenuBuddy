using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Specialized message box subclass, used to display network error messages.
	/// </summary>
	public class ErrorScreen : WidgetScreen
	{
		#region Fields

		private string _message;

		private Exception _error;

		private SpriteFont _font;

		#endregion //Fields

		#region Methods

		public ErrorScreen(string error) : base("Error Screen")
		{
			_message = error;
		}

		/// <summary>
		/// Constructs an error message box from the specified exception.
		/// </summary>
		public ErrorScreen(Exception exception) : this(exception.Message)
		{
			_error = exception;
		}

		/// <summary>
		/// Dont load any content for this screen, because a lot of the time the missing content will be the gradient texture.
		/// </summary>
		public override void LoadContent()
		{
			base.LoadContent();

			//Pop up a message box with the error message
			ScreenManager.AddScreen(new OkScreen(_message));

			AddCancelButton();
			_font = Content.Load<SpriteFont>(StyleSheet.SmallFontResource);
		}

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			//Draw at the upper left to try and fit as much text as possible on teh screen
			Vector2 textPosition = new Vector2(Resolution.TitleSafeArea.Left, Resolution.TitleSafeArea.Top);

			ScreenManager.SpriteBatchBegin();

			// Darken down any other screens that were drawn beneath the popup.
			FadeBackground();

			// Draw the message box text.
			ScreenManager.SpriteBatch.DrawString(_font, _error.ToString(), textPosition, Color.White, 0.0f, new Vector2(0.0f, 0.0f), 0.6f, SpriteEffects.None, 1.0f);

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}