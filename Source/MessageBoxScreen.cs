using System;
using System.Text;
using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// A popup message box screen, used to display "are you sure?"
	/// confirmation messages.
	/// </summary>
	public class MessageBoxScreen : GameScreen
	{
		#region Fields

		string message;

		Texture2D gradientTexture;

		#endregion

		#region Events

		public event EventHandler<PlayerIndexEventArgs> Accepted;
		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		#endregion

		#region Initialization


		/// <summary>
		/// Constructor automatically includes the standard "A=ok, B=cancel"
		/// usage text prompt.
		/// </summary>
		public MessageBoxScreen(string message) : this(message, true)
		{
		}

		/// <summary>
		/// Constructor lets the caller specify whether to include the standard
		/// "A=ok, B=cancel" usage text prompt.
		/// </summary>
		public MessageBoxScreen(string message, bool includeUsageText)
		{
			if (includeUsageText)
			{
				//First construct the usage text
				StringBuilder okText = new StringBuilder();
				StringBuilder cancelText = new StringBuilder();

				//Put the correct button text in the message
#if OUYA
				okText.Append("\nO button");
				cancelText.Append("\nA button");
#else
				okText.Append("\nA button");
				cancelText.Append("\nB button");
#endif

				//Add the keyboard text if we have a keyboard
#if KEYBOARD
				okText.Append(", Space, Enter");
				cancelText.Append(", Esc");
#endif

				//append the usage text
				okText.Append(": OK");
				cancelText.Append(": Cancel");

				//Put the whole message together
				StringBuilder completeMessage = new StringBuilder(message);
				completeMessage.Append(okText.ToString());
				completeMessage.Append(cancelText.ToString());

				this.message = completeMessage.ToString();
			}
			else
			{
				this.message = message;
			}

			IsPopup = true;

			TransitionOnTime = TimeSpan.FromSeconds(0.2);
			TransitionOffTime = TimeSpan.FromSeconds(0.2);
		}

		/// <summary>
		/// Loads graphics content for this screen. This uses the shared ContentManager
		/// provided by the Game class, so the content will remain loaded forever.
		/// Whenever a subsequent MessageBoxScreen tries to load this same content,
		/// it will just get back another reference to the already loaded data.
		/// </summary>
		public override void LoadContent()
		{
			base.LoadContent();
			ContentManager content = ScreenManager.Game.Content;

			gradientTexture = content.Load<Texture2D>(@"gradient");
		}

		#endregion

		#region Handle Input

		/// <summary>
		/// Responds to user input, accepting or cancelling the message box.
		/// </summary>
		public override void HandleInput(InputState input, GameTime rGameTime)
		{
			PlayerIndex playerIndex;

			// We pass in our ControllingPlayer, which may either be null (to
			// accept input from any player) or a specific index. If we pass a null
			// controlling player, the InputState helper returns to us which player
			// actually provided the input. We pass that through to our Accepted and
			// Cancelled events, so they can tell which player triggered them.
			if (input.IsMenuSelect(ControllingPlayer, out playerIndex))
			{
				// Raise the accepted event, then exit the message box.
				if (Accepted != null)
					Accepted(this, new PlayerIndexEventArgs(playerIndex));

				ExitScreen();
			}
			else if (input.IsMenuCancel(ControllingPlayer, out playerIndex))
			{
				// Raise the cancelled event, then exit the message box.
				if (Cancelled != null)
				{
					Cancelled(this, new PlayerIndexEventArgs(playerIndex));
				}
				else
				{
					//else just pop it off the stack
					ExitScreen();
				}

				ExitScreen();
			}
		}

		#endregion

		#region Draw

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

			// Darken down any other screens that were drawn beneath the popup.
			ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2.0f / 3.0f);

			// Center the message text in the viewport.
			Vector2 windowSize = new Vector2(ScreenRect.Center.X, ScreenRect.Center.Y);
			Vector2 textSize = ScreenManager.MessageBoxFont.MeasureString(message);
			Vector2 textPosition = windowSize - (textSize / 2);

			// The background includes a border somewhat larger than the text itself.
			const int hPad = 32;
			const int vPad = 16;

			Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
														  (int)textPosition.Y - vPad,
														  (int)textSize.X + hPad * 2,
														  (int)textSize.Y + vPad * 2);

			// Fade the popup alpha during transitions.
			Color color = new Color(1.0f, 1.0f, 1.0f, TransitionAlpha);

			// Draw the background rectangle.
			spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

			// Draw the message box text.
			spriteBatch.DrawString(ScreenManager.MessageBoxFont, message, textPosition, color);
		}

		#endregion
	}
}