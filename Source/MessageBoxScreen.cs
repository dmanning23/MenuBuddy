using System;
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

		/// <summary>
		/// Default sprite font.
		/// </summary>
		private SpriteFont m_Font;

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
#if XBOX
			const string usageText = "\nA button: OK" +
									 "\nB button: Cancel";
#else
			const string usageText = "\nA button, Space, Enter = ok" +
									 "\nB button, Esc = cancel";
#endif
			if (includeUsageText)
			{
				this.message = message + usageText;
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

			//TODO: load these from a content project

			gradientTexture = content.Load<Texture2D>(@"Resources\Textures\gradient");

			// Create the default sprite font.
			m_Font = content.Load<SpriteFont>(@"Resources\Fonts\GameplayFont");
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
					Cancelled(this, new PlayerIndexEventArgs(playerIndex));

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
			Viewport viewport = ScreenManager.GraphicsDevice.Viewport;
			Vector2 viewportSize = new Vector2(viewport.Width, viewport.Height);
			Vector2 textSize = m_Font.MeasureString(message);
			Vector2 textPosition = (viewportSize - textSize) / 2;

			// The background includes a border somewhat larger than the text itself.
			const int hPad = 32;
			const int vPad = 16;

			Rectangle backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
														  (int)textPosition.Y - vPad,
														  (int)textSize.X + hPad * 2,
														  (int)textSize.Y + vPad * 2);

			// Fade the popup alpha during transitions.
			Color color = new Color(1.0f, 1.0f, 1.0f, TransitionAlpha);

			spriteBatch.Begin();

			// Draw the background rectangle.
			spriteBatch.Draw(gradientTexture, backgroundRectangle, color);

			// Draw the message box text.
			spriteBatch.DrawString(m_Font, message, textPosition, color);

			spriteBatch.End();
		}

		#endregion
	}
}