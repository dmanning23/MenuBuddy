using InputHelper;
using System;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// A TextEdit control. 
	/// This is a widget with text in it. 
	/// When the widget is clicked, the user can edit the text using the keyboard.
	/// After the user finishes editing the text, a special event is fired off.
	/// </summary>
	public class TextEdit : RelativeLayoutButton, ITextEdit, IDisposable, ILabel
	{
		#region Properties

		/// <summary>
		/// the screen that holds this guy
		/// </summary>
		private IScreen Screen
		{
			get; set;
		}

		public string Text
		{
			get
			{
				return (null != TextLabel) ? TextLabel.Text : string.Empty;
			}
			set
			{
				if (TextLabel != null)
				{
					//check for empty string
					if (string.IsNullOrEmpty(value))
					{
						TextLabel.Text = "";
					}
					else
					{
						TextLabel.Text = value;
					}
				}
			}
		}

		/// <summary>
		/// The label that displays the text
		/// </summary>
		private Label TextLabel { get; set; }

		public FontSize FontSize
		{
			get
			{
				return TextLabel.FontSize;
			}
			set
			{
				TextLabel.FontSize = value;
			}
		}

		public Color? ShadowColor
		{
			get
			{
				return TextLabel.ShadowColor;
			}
			set
			{
				TextLabel.ShadowColor = value;
			}
		}

		public Color? TextColor
		{
			get
			{
				return TextLabel.TextColor;
			}
			set
			{
				TextLabel.TextColor = value;
			}
		}

		/// <summary>
		/// Event that gets fired when the user finishes changing the number from the numpad
		/// </summary>
		public event EventHandler<TextChangedEventArgs> OnTextEdited;

		#endregion //Properties

		#region Methods

		public TextEdit(string text, FontSize fontSize = FontSize.Medium)
		{
			Init(text, fontSize);
		}

		public TextEdit(FontSize fontSize = FontSize.Medium)
		{
			Init("", fontSize);
		}

		private void Init(string text, FontSize fontSize)
		{
			OnClick += CreateTextPad;
			TextLabel = new Label(text, fontSize)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
			};
			AddItem(TextLabel);
		}

		public override void LoadContent(IScreen screen)
		{
			Screen = screen;
			base.LoadContent(screen);
		}

		/// <summary>
		/// Method that gets called when the label is clicked to create the numpad.
		/// Adds a new screen with a numpad.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
		public void CreateTextPad(object obj, ClickEventArgs e)
		{
			//create the dropdown screen
			var numpad = new TextEditScreen(this);

			//add the screen over the current one
			Screen.ScreenManager.AddScreen(numpad);
		}

		public void SetText(string text)
		{
			if (Text != text)
			{
				Text = text;
				if (null != OnTextEdited)
				{
					OnTextEdited(this, new TextChangedEventArgs(Text));
				}

				PlaySelectedSound(this, new ClickEventArgs());
			}
		}

		public override void Dispose()
		{
			base.Dispose();
			OnTextEdited = null;
		}

		#endregion //Methods
	}
}
