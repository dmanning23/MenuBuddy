using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A TextEdit control. 
	/// This is a widget with text in it. 
	/// When the widget is clicked, the user can edit the text using the keyboard.
	/// After the user finishes editing the text, a special event is fired off.
	/// </summary>
	public abstract class BaseTextEdit : RelativeLayoutButton, ITextEdit, IDisposable
	{
		#region Properties

		/// <summary>
		/// the screen that holds this guy
		/// </summary>
		protected IScreen Screen
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

		public bool IsPassword
		{
			get
			{
				return (null != TextLabel) ? TextLabel.IsPassword : false;
			}
			set
			{
				if (TextLabel != null)
				{
					TextLabel.IsPassword = value;
				}
			}
		}

		/// <summary>
		/// The label that displays the text
		/// </summary>
		public Label TextLabel { get; protected set; }

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

		public IFontBuddy Font
		{
			get
			{
				return TextLabel.Font;
			}
			set
			{
				if (null != TextLabel)
				{
					TextLabel.Font = value;
				}
			}
		}

		/// <summary>
		/// Event that gets fired when the user finishes changing the number from the numpad
		/// </summary>
		public event EventHandler<TextChangedEventArgs> OnTextEdited;

		#endregion //Properties

		#region Methods

		protected BaseTextEdit(string text, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			TextLabel = new Label(text, content, fontSize)
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
			};
			AddItem(TextLabel);
		}

		protected BaseTextEdit(string text, IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			TextLabel = new Label(text, font, highlightedFont)
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
			};
			AddItem(TextLabel);
		}

		public override async Task LoadContent(IScreen screen)
		{
			Screen = screen;
			await base.LoadContent(screen);
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
