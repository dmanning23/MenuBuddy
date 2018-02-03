using InputHelper;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using FontBuddyLib;

namespace MenuBuddy
{
	/// <summary>
	/// A TextEdit control. 
	/// This is a widget with text in it. 
	/// When the widget is clicked, the user can edit the text using the keyboard.
	/// After the user finishes editing the text, a special event is fired off.
	/// </summary>
	public class TextEdit : RelativeLayoutButton, ITextEdit, IDisposable
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

		public override HorizontalAlignment Horizontal
		{
			get
			{
				return base.Horizontal;
			}

			set
			{
				base.Horizontal = value;
				if (null != TextLabel)
				{
					TextLabel.Horizontal = value;
				}
			}
		}

		public override VerticalAlignment Vertical
		{
			get
			{
				return base.Vertical;
			}

			set
			{
				base.Vertical = value;
				if (null != TextLabel)
				{
					TextLabel.Vertical = value;
				}
			}
		}

		/// <summary>
		/// Event that gets fired when the user finishes changing the number from the numpad
		/// </summary>
		public event EventHandler<TextChangedEventArgs> OnTextEdited;

		#endregion //Properties

		#region Methods

		public TextEdit(string text, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			OnClick += CreateTextPad;
			TextLabel = new Label(text, content, fontSize)
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
			};
			AddItem(TextLabel);
		}

		public TextEdit(string text, IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			OnClick += CreateTextPad;
			TextLabel = new Label(text, font, highlightedFont)
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
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

		public void ScaleToFit(int rowWidth)
		{
			TextLabel.ScaleToFit(rowWidth);
		}

		public void ShrinkToFit(int rowWidth)
		{
			TextLabel.ShrinkToFit(rowWidth);
		}

		#endregion //Methods
	}
}
