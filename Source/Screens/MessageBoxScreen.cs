using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Text;
using FontBuddyLib;
using System.Linq;
using MouseBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// A popup message box screen, used to display "are you sure?" confirmation messages.
	/// </summary>
	public class MessageBoxScreen : MenuScreen, ISelectable
	{
		#region Properties

		/// <summary>
		/// The message to be displayed 
		/// </summary>
		public string Message { get; private set; }

		public event EventHandler<SelectedEventArgs> OnSelect;

		public event EventHandler<SelectedEventArgs> OnCancel;

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor lets the caller specify whether to include the standard
		/// "A=ok, B=cancel" usage text prompt.
		/// </summary>
		public MessageBoxScreen(string message, string menuTitle = "") :
			base(menuTitle)
		{
			//grab the message
			Message = message;

			CoverOtherScreens = true;

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

			//Create the stack for the label text
			var labelStack = new StackLayout()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Alignment = StackAlignment.Top,
			};

			//Split up the label text into lines
			var lines = Message.Split('\n').ToList();

			//Add all the label text to the stack
			foreach (var line in lines)
			{
				//Set the label text
				var label = new Label(line)
				{
					Style = DefaultStyles.Instance().MessageBoxStyle
				};
				label.Style.SelectedFont = label.Style.UnselectedFont;
				label.Style.HasOutline = false;
				label.Style.HasBackground = false;
				labelStack.AddItem(label);
            }

			//add a shim between the text and the buttons
			labelStack.AddItem(new Shim() { Size = new Vector2(0, 16f) });

			//Add the buttons
			AddButtons(labelStack);

			//Set the position of the labelstack
			labelStack.Position = new Point(Resolution.TitleSafeArea.Center.X,
				Resolution.TitleSafeArea.Center.Y - (labelStack.Rect.Height / 2));

			AddItem(labelStack);

			AddBackgroundImage(labelStack);
        }

		protected virtual void AddButtons(StackLayout stack)
		{
			AddOkButton(stack);

			AddCancelButton(stack);
		}

		protected void AddOkButton(StackLayout stack)
		{
			//Create the menu entry for "OK"
			var ok = new MenuEntry("Ok")
			{
				Style = DefaultStyles.Instance().MessageBoxStyle
			};
			ok.Style.BackgroundImage = DefaultStyles.Instance().MenuEntryStyle.BackgroundImage;
			ok.OnSelect += ((obj, e) =>
			{
				if (null != OnSelect)
				{
					OnSelect(obj, e);
				}
				ExitScreen();
			});
			ok.LoadContent(this);
			stack.AddItem(ok);
			MenuEntries.AddItem(ok);
		}

		protected void AddCancelButton(StackLayout stack)
		{
			//Create the menu entry "Cancel"
			var cancel = new MenuEntry("Cancel")
			{
				Style = DefaultStyles.Instance().MessageBoxStyle
			};
			cancel.Style.BackgroundImage = DefaultStyles.Instance().MenuEntryStyle.BackgroundImage;
			cancel.OnSelect += ((obj, e) =>
			{
				if (null != OnCancel)
				{
					OnCancel(obj, e);
				}
				ExitScreen();
			});
			cancel.LoadContent(this);
			stack.AddItem(cancel);
			MenuEntries.AddItem(cancel);
		}

		public virtual void AddBackgroundImage(ILayout labelStack)
		{
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

		#endregion //Initialization

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

		public void Selected(object obj, PlayerIndex player)
		{
			throw new NotImplementedException();
		}

		#endregion
	}
}