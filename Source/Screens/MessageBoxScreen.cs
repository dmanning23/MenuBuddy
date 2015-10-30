using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Text;
using FontBuddyLib;
using System.Linq;

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

			//Create the stack for the label text
			var labelStack = new StackLayout()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Alignment = StackAlignment.Top,
			};

			//Split up the label text into lines
			var lines = Message.Split('\n').Reverse().ToList();

			//Add all the label text to the stack
			foreach (var line in lines)
			{
				//Set the label text
				var label = new Label(line)
				{
					Style = DefaultStyles.Instance().MessageBoxStyle
				};
				label.Style.SelectedFont = new FontBuddy();
				label.Style.SelectedFont.Font = DefaultStyles.Instance().MessageBoxStyle.SelectedFont.Font;
				label.Style.SelectedTextColor = label.Style.UnselectedTextColor;
				label.Style.HasOutline = false;
				label.Style.HasBackground = false;
				labelStack.AddItem(label);
            }

			//add a shim between the text and the buttons
			labelStack.AddItem(new Shim() { Size = new Vector2(0, 16f) });

			//Add the buttons
			AddButtons(labelStack, IncludeUsageText);

			//Set the position of the labelstack
			labelStack.Position = new Point(Resolution.TitleSafeArea.Center.X,
				Resolution.TitleSafeArea.Center.Y - (labelStack.Rect.Height / 2));

			AddItem(labelStack);

			//Add the background image
			var bkgImage = new BackgroundImage(DefaultStyles.Instance().MessageBoxStyle.BackgroundImage)
			{
				Layer = -1.0f,
				FillRect = true,
				Style = DefaultStyles.Instance().MessageBoxStyle,
				Padding = new Vector2(64, 32),
				Position = new Point(labelStack.Rect.Center.X, labelStack.Rect.Center.Y),
                Size = new Vector2(labelStack.Rect.Width, labelStack.Rect.Height),
                Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};
			AddItem(bkgImage);
        }

		protected virtual void AddButtons(StackLayout stack, bool includeUsageText)
		{
			AddOkButton(stack, includeUsageText);

			AddCancelButton(stack, includeUsageText);
		}

		protected void AddOkButton(StackLayout stack, bool includeUsageText)
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
			okEntry.Style.BackgroundImage = DefaultStyles.Instance().MenuEntryStyle.BackgroundImage;
			okEntry.Selected += OnSelect;
			okEntry.LoadContent(this);
			stack.AddItem(okEntry);
		}

		protected void AddCancelButton(StackLayout stack, bool includeUsageText)
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
			cancel.Style.BackgroundImage = DefaultStyles.Instance().MenuEntryStyle.BackgroundImage;
			cancel.Selected += OnCancel;
			cancel.LoadContent(this);
			stack.AddItem(cancel);
		}

		#endregion

		#region Handle Input

		public override void OnSelect(object sender, PlayerIndexEventArgs e)
		{
			// Raise the accepted event, then exit the message box.
			if (Accepted != null)
			{
				Accepted(sender, e);
			}

			ExitScreen();
		}

		public override void OnCancel(object sender, PlayerIndexEventArgs e)
		{
			// Raise the cancelled event, then exit the message box.
			if (Cancelled != null)
			{
				Cancelled(this, new PlayerIndexEventArgs(e.PlayerIndex));
			}
			
			base.OnCancel(sender, e);
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