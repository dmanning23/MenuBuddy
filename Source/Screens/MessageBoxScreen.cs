using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Text;
using Vector2Extensions;

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

		private bool IncludeUsageText { get; set; }

		public Image BackgroundImage { get; set; }

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
			//grab the message
			Message = message;
			IncludeUsageText = includeUsageText;

			CoverOtherScreens = false;

			Transition.OnTime = TimeSpan.FromSeconds(0.2);
			Transition.OffTime = TimeSpan.FromSeconds(0.2);
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

			//add the label text
			var label = new Label(Style, Message);
			AddItem(label);

			AddButtons(IncludeUsageText);

			//set the background image size
			BackgroundImage = new Image(Style, Style.Texture);
			const int hPad = 64;
			const int vPad = 32;
			var rect = Layout.Rect;
			var backgroundRectangle = new Rectangle(rect.X - hPad,
				rect.Y - vPad,
				rect.Width + hPad * 2,
				rect.Height + vPad * 2);
			BackgroundImage.Rect = backgroundRectangle;
		}

		protected virtual void AddButtons(bool includeUsageText)
		{
			AddOkButton(includeUsageText);

			AddCancelButton(includeUsageText);
		}

		protected void AddOkButton(bool includeUsageText)
		{
			//create the strings to hold the menu text
			var okText = new StringBuilder();
			okText.Append("Ok");

			if (includeUsageText)
			{
				//Put the correct button text in the message
#if OUYA
				okText.Append(": O button");
#else
				okText.Append(": A button");
#endif

				//Add the keyboard text if we have a keyboard
#if KEYBOARD
				okText.Append(", Space, Enter");
#endif
			}

			//Create the menu entry for "OK"
			var okEntry = new MenuEntry(Style, okText.ToString());
			okEntry.Selected += OnAccept;
			AddMenuEntry(okEntry);
		}

		protected void AddCancelButton(bool includeUsageText)
		{
			//create the strings to hold the menu text
			var cancelText = new StringBuilder();
			cancelText.Append("Cancel");

			if (includeUsageText)
			{
				//Put the correct button text in the message
#if OUYA
				cancelText.Append(": A button");
#else
				cancelText.Append(": B button");
#endif

				//Add the keyboard text if we have a keyboard
#if KEYBOARD
				cancelText.Append(", Esc");
#endif
			}

			//Create the menu entry "Cancel"
			var cancel = new MenuEntry(Style, cancelText.ToString());
			cancel.Selected += OnCancel;
			AddMenuEntry(cancel);
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

		public override void OnCancel(PlayerIndex? playerIndex)
		{
			// Raise the cancelled event, then exit the message box.
			if (Cancelled != null)
			{
				Cancelled(this, new PlayerIndexEventArgs(playerIndex));
			}
			
			base.OnCancel(playerIndex);
		}

		#endregion

		#region Draw

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatchBegin();

			//Darken down any other screens that were drawn beneath the popup.
			FadeBackground();

			//Draw the background image.
			BackgroundImage.Draw(this, Time);

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion
	}
}