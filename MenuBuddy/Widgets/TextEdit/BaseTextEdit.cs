using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Abstract base class for text input widgets. Displays editable text in a button and raises
	/// <see cref="OnTextEdited"/> when the user finishes editing.
	/// </summary>
	public abstract class BaseTextEdit : RelativeLayoutButton, ITextEdit, IDisposable
	{
		#region Properties

		/// <summary>
		/// The screen that contains this text edit widget.
		/// </summary>
		protected IScreen Screen
		{
			get; set;
		}

		/// <summary>
		/// The current text value. Setting this updates the inner label.
		/// </summary>
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
		/// Whether the text should be masked with asterisks. Delegates to the inner label.
		/// </summary>
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
		/// The label widget that displays the current text.
		/// </summary>
		public Label TextLabel { get; protected set; }

		/// <inheritdoc/>
		public FontSize FontSize
		{
			get
			{
				return TextLabel.FontSize;
			}
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
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
		/// The horizontal alignment. Setting this also updates the inner label's alignment.
		/// </summary>
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

		/// <summary>
		/// The vertical alignment. Setting this also updates the inner label's alignment.
		/// </summary>
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
		/// The font used to render text. Delegates to the inner label.
		/// </summary>
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
		/// Raised when the user finishes editing and the text value has changed.
		/// </summary>
		public event EventHandler<TextChangedEventArgs> OnTextEdited;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="BaseTextEdit"/> with managed fonts from the content manager.
		/// </summary>
		/// <param name="text">The initial text value.</param>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontSize">The font size category. Defaults to <see cref="FontSize.Medium"/>.</param>
		protected BaseTextEdit(string text, ContentManager content, FontSize fontSize = FontSize.Medium)
		{
			TextLabel = new Label(text, content, fontSize)
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
			};
			AddItem(TextLabel);
		}

		/// <summary>
		/// Initializes a new <see cref="BaseTextEdit"/> with pre-loaded fonts.
		/// </summary>
		/// <param name="text">The initial text value.</param>
		/// <param name="font">The font to use for normal rendering.</param>
		/// <param name="highlightedFont">An optional font to use when highlighted.</param>
		protected BaseTextEdit(string text, IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			TextLabel = new Label(text, font, highlightedFont)
			{
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
			};
			AddItem(TextLabel);
		}

		/// <inheritdoc/>
		public override async Task LoadContent(IScreen screen)
		{
			Screen = screen;
			await base.LoadContent(screen);
		}

		/// <summary>
		/// Unloads content and releases the <see cref="OnTextEdited"/> event handler.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();
			OnTextEdited = null;
		}

		/// <inheritdoc/>
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

		/// <inheritdoc/>
		public void ScaleToFit(int rowWidth)
		{
			TextLabel.ScaleToFit(rowWidth);
		}

		/// <inheritdoc/>
		public void ShrinkToFit(int rowWidth)
		{
			TextLabel.ShrinkToFit(rowWidth);
		}

		#endregion //Methods
	}
}
