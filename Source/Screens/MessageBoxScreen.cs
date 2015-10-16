using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Text;
using FontBuddyLib;

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

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor lets the caller specify whether to include the standard
		/// "A=ok, B=cancel" usage text prompt.
		/// </summary>
		public MessageBoxScreen(string message, bool includeUsageText, string menuTitle = "") :
			base(menuTitle)
		{
			//grab the message
			Message = message;
			IncludeUsageText = includeUsageText;

			CoverOtherScreens = false;

			Transition.OnTime = TimeSpan.FromSeconds(0.2);
			Transition.OffTime = TimeSpan.FromSeconds(0.2);
		}

		/// <summary>
		/// Constructor automatically includes the standard "A=ok, B=cancel"
		/// usage text prompt.
		/// </summary>
		public MessageBoxScreen(string message)
			: this(message, true)
		{
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

			//Set the label text
			var label = new Label(Message)
			{
				Style = DefaultStyles.Instance().MessageBoxStyle
			};
			label.Style.SelectedFont = new FontBuddy();
			label.Style.SelectedFont.Font = DefaultStyles.Instance().MessageBoxStyle.SelectedFont.Font;
			label.Style.SelectedTextColor = label.Style.UnselectedTextColor;
			label.Style.HasOutline = false;
			label.Style.HasBackground = false;
			label.Horizontal = HorizontalAlignment.Center;
			label.Vertical = VerticalAlignment.Bottom;
			label.Position = MenuEntries.Position + new Point(0, -64);
			AddItem(label);

			//Add the buttons
			AddButtons(IncludeUsageText);

			//Add the background image
			var bkgImage = new BackgroundImage
			{
				Layer = -1.0f,
				FillRect = true,
				Style = DefaultStyles.Instance().MessageBoxStyle,
				Padding = new Vector2(64, 32),
				Position = new Point(MenuEntries.Position.X, Layout.Rect.Y),
                Size = new Vector2(Layout.Rect.Width, Layout.Rect.Height),
                Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Top
			};
			AddItem(bkgImage);
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
			var okEntry = new MenuEntry(okText.ToString())
			{
				Style = DefaultStyles.Instance().MessageBoxStyle
			};
			okEntry.Style.Texture = DefaultStyles.Instance().MenuEntryStyle.Texture;
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
			var cancel = new MenuEntry(cancelText.ToString())
			{
				Style = DefaultStyles.Instance().MessageBoxStyle
			};
			cancel.Style.Texture = DefaultStyles.Instance().MenuEntryStyle.Texture;
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

			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion
	}
}