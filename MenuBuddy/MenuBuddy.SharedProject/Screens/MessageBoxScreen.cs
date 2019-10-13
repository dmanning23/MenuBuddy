using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A popup message box screen, used to display "are you sure?" confirmation messages.
	/// </summary>
	public class MessageBoxScreen : WidgetScreen, IClickable, IDisposable
	{
		#region Properties

		/// <summary>
		/// The message to be displayed 
		/// </summary>
		public string Message { get; private set; }

		public event EventHandler<ClickEventArgs> OnSelect;

		public event EventHandler<ClickEventArgs> OnCancel;

		protected IStackLayout ControlStack { get; private set; }

		public string OkText { get; set; }

		public string CancelText { get; set; }

		public bool HasBackground { get; set; }

		public bool ExitOnOk { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor lets the caller specify whether to include the standard
		/// "A=ok, B=cancel" usage text prompt.
		/// </summary>
		public MessageBoxScreen(string message, string menuTitle = "", ContentManager content = null) :
			base(menuTitle, content)
		{
			ExitOnOk = true;

			//grab the message
			Message = message;

			HasBackground = true;
			CoverOtherScreens = true;

			Transition.OnTime = 0.2f;
			Transition.OffTime = 0.2f;
			OkText = "Ok";
			CancelText = "Cancel";
			Modal = true;
		}

		/// <summary>
		/// Loads graphics content for this screen. This uses the shared ContentManager
		/// provided by the Game class, so the content will remain loaded forever.
		/// Whenever a subsequent MessageBoxScreen tries to load this same content,
		/// it will just get back another reference to the already loaded data.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			var screenLayout = new RelativeLayout()
			{
				Size = new Vector2(Resolution.ScreenArea.Width, Resolution.ScreenArea.Height),
				Position = new Point(0),
				Horizontal = HorizontalAlignment.Left, 
				Vertical = VerticalAlignment.Top,
			};

			//Create the stack for the label text
			var labelStack = new StackLayout()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Alignment = StackAlignment.Top,
			};

			ControlStack = new StackLayout()
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
				//split the line into lines that will actuall fit on the screen
				var tempFont = new FontBuddy();
				tempFont.LoadContent(Content, StyleSheet.SmallFontResource);
				var splitLines = tempFont.BreakTextIntoList(line, Resolution.TitleSafeArea.Width - 64);
				foreach (var splitLine in splitLines)
				{
					//Set the label text
					var label = new Label(splitLine, Content, FontSize.Small)
					{
						Highlightable = false,
						TextColor = StyleSheet.MessageBoxTextColor,
					};
					ControlStack.AddItem(label);
				}
			}

			try
			{
				await AddAdditionalControls();
			}
			catch (Exception ex)
			{
				await ScreenManager.AddScreen(new ErrorScreen(ex));
			}

			//add a shim between the text and the buttons
			ControlStack.AddItem(new Shim() { Size = new Vector2(0, 32f) });

			labelStack.AddItem(ControlStack);

			//Add the buttons
			await AddButtons(labelStack);

			//Set the position of the labelstack
			labelStack.Position = new Point(labelStack.Rect.Width / 2, 0);
			var absScreenLayout = new AbsoluteLayout()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Size = new Vector2(labelStack.Rect.Width, labelStack.Rect.Height),
			};
			absScreenLayout.AddItem(labelStack);

			screenLayout.AddItem(absScreenLayout);
			AddItem(screenLayout);

			if (HasBackground)
			{
				AddBackgroundImage(labelStack);
			}
		}

		/// <summary>
		/// Override this method to add any additional controls to the ControlStack
		/// </summary>
		protected virtual Task AddAdditionalControls()
		{
			return Task.CompletedTask;
		}

		/// <summary>
		/// Override with method to validate any data that has been entered before exiting the screen.
		/// </summary>
		/// <returns></returns>
		protected virtual bool Validate()
		{
			return true;
		}

		protected virtual async Task AddButtons(StackLayout stack)
		{
			var buttonLayout = new RelativeLayout()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2( Resolution.TitleSafeArea.Width * 0.8f, 64f)
			};

			var okButton = await AddMessageBoxOkButton();
			okButton.Horizontal = HorizontalAlignment.Left;
			buttonLayout.AddItem(okButton);

			var cancelButton = await AddMessageBoxCancelButton();
			cancelButton.Horizontal = HorizontalAlignment.Right;
			buttonLayout.AddItem(cancelButton);

			stack.AddItem(buttonLayout);
			stack.AddItem(new Shim(0, 16));
		}

		protected async Task<IButton> AddMessageBoxOkButton()
		{
			var button = await CreateButton(true);

			button.OnClick += ((obj, e) =>
			{
				if (Validate())
				{
					if (null != OnSelect)
					{
						OnSelect(obj, e);
					}

					if (ExitOnOk)
					{
						ExitScreen();
					}
				}
			});

			return button;
		}

		protected async Task<IButton> AddMessageBoxCancelButton()
		{
			var button = await CreateButton(false);
			button.OnClick += ((obj, e) =>
			{
				if (null != OnCancel)
				{
					OnCancel(obj, e);
				}
				ExitScreen();
			});

			return button;
		}

		private async Task<IButton> CreateButton(bool okButton)
		{
			//Create the menu entry "Cancel"
			var label = new Label(okButton ? OkText : CancelText, Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Layer = 1,
			};

			//check if there is a background image for this button
			var backgrounImage = okButton ? StyleSheet.MessageBoxOkImageResource : StyleSheet.MessageBoxCancelImageResource;
			var hasBackgroundImage = !string.IsNullOrEmpty(backgrounImage);

			//Create the menu entry for "OK"
			var button = new RelativeLayoutButton()
			{
				HasBackground = !hasBackgroundImage,
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(Resolution.TitleSafeArea.Width * 0.4f - 16, label.Rect.Height * 2f)
			};
			button.AddItem(label);

			if (hasBackgroundImage)
			{
				button.AddItem(new Image(Content.Load<Texture2D>(backgrounImage))
				{
					Horizontal = HorizontalAlignment.Center,
					Vertical = VerticalAlignment.Center,
					Size = button.Rect.Size.ToVector2(),
					Highlightable = false,
					PulsateOnHighlight = false,
					FillRect = true,
					Layer = 0
				});
			}

			await button.LoadContent(this);
			return button;
		}

		public virtual void AddBackgroundImage(ILayout labelStack)
		{
			//get the background image dimensions
			var width = Math.Min(Resolution.TitleSafeArea.Width, labelStack.Rect.Width + 128);
			var height = Math.Min(Resolution.TitleSafeArea.Height, labelStack.Rect.Height + 200);

			//Add the background image
			var bkgImage = new BackgroundImage(Content.Load<Texture2D>(StyleSheet.MessageBoxBackgroundImageResource))
			{
				FillRect = true,
				Position = new Point(labelStack.Rect.Center.X, labelStack.Rect.Center.Y),
				Size = new Vector2(width, height),
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};
			AddItem(bkgImage);
		}

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

		public override void Dispose()
		{
			base.Dispose();
			OnSelect = null;
			OnCancel = null;
		}

		#endregion //Methods
	}
}