using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// A popup message box screen, used to display "are you sure?" confirmation messages.
	/// </summary>
	public class MessageBoxScreen : MenuScreen
	{
		#region Properties

		public event EventHandler<PlayerIndexEventArgs> Accepted;

		public event EventHandler<PlayerIndexEventArgs> Cancelled;

		/// <summary>
		/// The message to be displayed 
		/// </summary>
		public string Message { get; private set; }

		/// <summary>
		/// The gradient that is drawn behind the message box.
		/// </summary>
		private Texture2D GradientTexture { get; set; }

		private MenuEntry _cancelEntry;

		/// <summary>
		/// If this is true, will pad out the message box to make it taller.
		/// Set to false for multiline message boxes, true for touchscreen games!
		/// </summary>
		public bool InflateMessageBox { get; set; }

		#endregion //Properties

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
			InflateMessageBox = true;

			//grab the message
			Message = message;

			//create the strings to hold the menu text
			var okText = new StringBuilder();
			okText.Append("Ok");
			var cancelText = new StringBuilder();
			cancelText.Append("Cancel");

			if (includeUsageText)
			{
				//Put the correct button text in the message
#if OUYA
				okText.Append(": O button");
				cancelText.Append(": A button");
#else
				okText.Append(": A button");
				cancelText.Append(": B button");
#endif

				//Add the keyboard text if we have a keyboard
#if KEYBOARD
				okText.Append(", Space, Enter");
				cancelText.Append(", Esc");
#endif
			}

			//Create the menu entries for "OK" and "Cancel"
			var okEntry = new MenuEntry(okText.ToString(), true);
			okEntry.Selected += OnAccept;
			MenuEntries.Add(okEntry);

			_cancelEntry = new MenuEntry(cancelText.ToString(), true);
			_cancelEntry.Selected += OnCancel;
			MenuEntries.Add(_cancelEntry);

			IsPopup = true;

			TransitionOnTime = TimeSpan.FromSeconds(0.2);
			TransitionOffTime = TimeSpan.FromSeconds(0.2);

			TextSelectionRect = false;
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

			//load the gradient texture
			ContentManager content = ScreenManager.Game.Content;
			GradientTexture = content.Load<Texture2D>(@"gradient");
		}

		#endregion

		#region Handle Input

		private void OnAccept(object sender, PlayerIndexEventArgs e)
		{
			// Raise the accepted event, then exit the message box.
			if (Accepted != null)
			{
				Accepted(sender, e);
			}

			ExitScreen();
		}

		protected override void OnCancel(PlayerIndex playerIndex)
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

		#endregion

		#region Draw

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;

			ScreenManager.SpriteBatchBegin();

			// Darken down any other screens that were drawn beneath the popup.
			ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2.0f / 3.0f);

			// Center the message text in the viewport.
			Vector2 textSize = TotalMessageSize();
			Vector2 textPosition = new Vector2(
				Resolution.TitleSafeArea.Center.X - (textSize.X / 2),
				_cancelEntry.ButtonRect.Bottom - textSize.Y);

			// The background includes a border somewhat larger than the text itself.
			const int hPad = 32;
			const int vPad = 16;

			var backgroundRectangle = new Rectangle((int)textPosition.X - hPad,
			                                        (int)textPosition.Y - vPad,
			                                        (int)textSize.X + hPad * 2,
			                                        (int)textSize.Y + vPad * 2);

			// Fade the popup alpha during transitions.
			var color = new Color(1.0f, 1.0f, 1.0f, TransitionAlpha);

			// Draw the background rectangle.
			spriteBatch.Draw(GradientTexture, backgroundRectangle, color);

			// Draw the message box text.
			spriteBatch.DrawString(ScreenManager.MessageBoxFont, Message, textPosition, color);

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		private Vector2 TotalMessageSize()
		{
			//measure the message
			Vector2 messageSize = ScreenManager.MessageBoxFont.MeasureString(Message);

			if (InflateMessageBox)
			{
				messageSize.Y *= 1.5f;
			}

			//measure the menu entries text
			foreach (var entry in MenuEntries)
			{
				Vector2 entrySize = Vector2.Zero;
				if (ScreenManager.TouchMenus && IsActive)
				{
					//use the button rect
					entrySize = new Vector2(entry.ButtonRect.Width, entry.ButtonRect.Height);
				}
				else
				{
					//measure the text...
					entrySize.X = entry.GetWidth(this);
					entrySize.Y = entry.GetHeight(this);
				}

				messageSize.X = Math.Max(messageSize.X, entrySize.X);
				messageSize.Y += entrySize.Y;

				//set the button rect width too
				entry.Width = messageSize.X;
			}

			return messageSize;
		}

		#endregion
	}
}