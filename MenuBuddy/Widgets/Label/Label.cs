using FontBuddyLib;
using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// A text display widget that supports multiple font sizes, shadow text, pulsate effects, and password masking.
	/// </summary>
	public class Label : Widget, ILabel, IDisposable
	{
		#region Fields

		private string _text;
		private string _renderedText;
		private bool _isPassword;

		private FontSize _fontSize;

#pragma warning disable 0414
		/// <summary>
		/// Raised when this label is clicked.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnClick;
#pragma warning restore 0414

		#endregion //Fields

		#region Properties

		/// <summary>
		/// Whether this label can be clicked.
		/// </summary>
		public bool Clickable { get; set; }

		/// <summary>
		/// The text content displayed by this label. Setting this recalculates the rendered text and bounding rectangle.
		/// </summary>
		public string Text
		{
			get
			{
				return _text;
			}
			set
			{
				if (_text != value)
				{
					_text = value;
					SetRenderText();
					CalculateRect();
				}
			}
		}

		/// <summary>
		/// The font size category for this label. Setting this recalculates the bounding rectangle.
		/// </summary>
		public FontSize FontSize
		{
			get
			{
				return _fontSize;
			}
			private set
			{
				if (_fontSize != value)
				{
					_fontSize = value;
					CalculateRect();
				}
			}
		}

		/// <summary>
		/// Whether this label is currently in a clicked visual state.
		/// </summary>
		public bool IsClicked { get; set; }

		/// <summary>
		/// Whether this label created and owns its own fonts (and is responsible for disposing them).
		/// </summary>
		private bool ManagedFonts { get; set; } = false;

		/// <summary>
		/// The font used to render this label's text in its normal state.
		/// </summary>
		public IFontBuddy Font { get; set; }

		/// <summary>
		/// The font used to render this label's text when highlighted. Falls back to <see cref="Font"/> if <c>null</c>.
		/// </summary>
		public IFontBuddy HighlightedFont { get; set; }

		/// <summary>
		/// An optional override color for the text shadow. If <c>null</c>, the style sheet default is used.
		/// </summary>
		public Color? ShadowColor { get; set; }

		/// <summary>
		/// An optional override color for the text. If <c>null</c>, the style sheet default is used.
		/// </summary>
		public Color? TextColor { get; set; }

		/// <summary>
		/// Whether the text should be masked with asterisks, as for password input.
		/// Setting this recalculates the rendered text and bounding rectangle.
		/// </summary>
		public bool IsPassword
		{
			get
			{
				return _isPassword;
			}
			set
			{
				if (_isPassword != value)
				{
					_isPassword = value;
					SetRenderText();
					CalculateRect();
				}
			}
		}

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="Label"/> that creates and manages its own fonts from the content manager.
		/// </summary>
		/// <param name="text">The text to display.</param>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontSize">The font size category. Defaults to <see cref="FontSize.Medium"/>.</param>
		/// <param name="fontResource">An optional font resource name override. If <c>null</c>, the style sheet default is used.</param>
		/// <param name="useFontPlus">Whether to use FontBuddyPlus fonts. If <c>null</c>, the style sheet default is used.</param>
		/// <param name="fontPlusSize">An optional font size for FontBuddyPlus. If <c>null</c>, the style sheet default is used.</param>
		public Label(string text, ContentManager content, FontSize fontSize = FontSize.Medium, string fontResource = null, bool? useFontPlus = null, int? fontPlusSize = null)
		{
			_fontSize = fontSize;
			Text = text;
			Clickable = true;

			ManagedFonts = true;

			//If there is no value for the font plus flag, grab it from the stylesheet
			if (!useFontPlus.HasValue)
			{
				useFontPlus = StyleSheet.UseFontPlus;
			}

			if (!useFontPlus.Value)
			{
				//load the fonts from the stylesheet
				InitializeFonts(content, fontResource);
			}
			else
			{
				InitializeFontPlus(content, fontResource, fontPlusSize);
			}

			CalculateRect();
		}

		/// <summary>
		/// Initializes a new <see cref="Label"/> with pre-loaded fonts.
		/// </summary>
		/// <param name="text">The text to display.</param>
		/// <param name="font">The font to use for normal rendering.</param>
		/// <param name="highlightedFont">An optional font to use when highlighted.</param>
		public Label(string text, IFontBuddy font, IFontBuddy highlightedFont = null)
		{
			_fontSize = FontSize.Medium;
			Text = text;
			Clickable = true;

			//hold onto the provided fonts
			Font = font;
			HighlightedFont = highlightedFont;
			CalculateRect();
		}

		/// <summary>
		/// Initializes a new <see cref="Label"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The label to copy from.</param>
		public Label(Label inst) : base(inst)
		{
			if (null == inst)
			{
				throw new ArgumentNullException("inst");
			}

			Font = inst.Font;
			HighlightedFont = inst.HighlightedFont;
			Clickable = inst.Clickable;
			_text = inst.Text;
			_renderedText = inst._renderedText;
			_fontSize = inst.FontSize;
			TextColor = inst.TextColor;
			ShadowColor = inst.ShadowColor;
		}

		/// <summary>
		/// Creates a deep copy of this label.
		/// </summary>
		/// <returns>A new <see cref="Label"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new Label(this);
		}

		/// <summary>
		/// Initializes standard SpriteFont-based fonts for this label based on the current <see cref="FontSize"/>.
		/// </summary>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontResource">The font resource name, or <c>null</c> to use the style sheet default.</param>
		protected virtual void InitializeFonts(ContentManager content, string fontResource)
		{
			fontResource = GetFontResource(fontResource);

			switch (FontSize)
			{
				case FontSize.Large:
					{
						Font = new FontBuddy();
						HighlightedFont = new PulsateBuddy();
					}
					break;
				case FontSize.Medium:
					{
						Font = new ShadowTextBuddy()
						{
							ShadowSize = 1.0f,
							ShadowOffset = new Vector2(7.0f, 7.0f),
						};
						HighlightedFont = new PulsateBuddy()
						{
							ShadowSize = 1.0f,
							ShadowOffset = new Vector2(7.0f, 7.0f),
						};
					}
					break;
				default:
					{
						if (StyleSheet.SmallFontHasShadow)
						{
							Font = new ShadowTextBuddy()
							{
								ShadowSize = 1.0f,
								ShadowOffset = new Vector2(3.0f, 4.0f),
							};
						}
						else
						{
							Font = new FontBuddy();
						}

						HighlightedFont = new PulsateBuddy()
						{
							ShadowSize = 1.0f,
							ShadowOffset = new Vector2(3.0f, 4.0f),
						};
					}
					break;
			}

			Font.LoadContent(content, fontResource);
			HighlightedFont.LoadContent(content, fontResource);
		}

		/// <summary>
		/// Initializes FontBuddyPlus-based fonts for this label based on the current <see cref="FontSize"/>.
		/// </summary>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontResource">The font resource name, or <c>null</c> to use the style sheet default.</param>
		/// <param name="fontPlusSize">The font size for FontBuddyPlus, or <c>null</c> to use the style sheet default.</param>
		protected virtual void InitializeFontPlus(ContentManager content, string fontResource, int? fontPlusSize)
		{
			fontResource = GetFontResource(fontResource);
			var fontSize = GetFontPlusSize(fontPlusSize);

			switch (FontSize)
			{
				case FontSize.Large:
					{
						Font = new FontBuddyPlus();
						HighlightedFont = new PulsateBuddy();
					}
					break;
				case FontSize.Medium:
					{
						Font = new ShadowTextBuddy()
						{
							ShadowSize = 1.0f,
							ShadowOffset = new Vector2(7.0f, 7.0f),
						};
						HighlightedFont = new PulsateBuddy()
						{
							ShadowSize = 1.0f,
							ShadowOffset = new Vector2(7.0f, 7.0f),
						};
					}
					break;
				default:
					{
						if (StyleSheet.SmallFontHasShadow)
						{
							Font = new ShadowTextBuddy()
							{
								ShadowSize = 1.0f,
								ShadowOffset = new Vector2(3.0f, 4.0f),
							};
						}
						else
						{
							Font = new FontBuddyPlus();
						}

						HighlightedFont = new PulsateBuddy()
						{
							ShadowSize = 1.0f,
							ShadowOffset = new Vector2(3.0f, 4.0f),
						};
					}
					break;
			}

			Font.LoadContent(content, fontResource, true, fontSize);
			HighlightedFont.LoadContent(content, fontResource, true, fontSize);
		}

		/// <summary>
		/// Returns the font resource name, falling back to the style sheet default for the current <see cref="FontSize"/> if none is specified.
		/// </summary>
		/// <param name="fontResource">The font resource name, or <c>null</c>/empty to use the default.</param>
		/// <returns>The resolved font resource name.</returns>
		private string GetFontResource(string fontResource)
		{
			if (string.IsNullOrEmpty(fontResource))
			{
				switch (FontSize)
				{
					case FontSize.Large:
						{
							fontResource = StyleSheet.LargeFontResource;
						}
						break;
					case FontSize.Medium:
						{
							fontResource = StyleSheet.MediumFontResource;
						}
						break;
					case FontSize.Small:
						{
							fontResource = StyleSheet.SmallFontResource;
						}
						break;
				}
			}

			return fontResource;
		}

		/// <summary>
		/// Returns the FontBuddyPlus font size, falling back to the style sheet default for the current <see cref="FontSize"/> if none is specified.
		/// </summary>
		/// <param name="fontPlusSize">The font size, or <c>null</c> to use the default.</param>
		/// <returns>The resolved font size.</returns>
		private int GetFontPlusSize(int? fontPlusSize)
		{
			if (fontPlusSize.HasValue)
			{
				return fontPlusSize.Value;
			}
			else
			{
				switch (FontSize)
				{
					case FontSize.Large:
						{
							return StyleSheet.LargeFontSize;
						}
					case FontSize.Medium:
						{
							return StyleSheet.MediumFontSize;
						}
					default:
						{
							return StyleSheet.SmallFontSize;
						}
				}
			}
		}

		/// <summary>
		/// Unloads content and disposes managed fonts if this label owns them.
		/// </summary>
		public override void UnloadContent()
		{
			base.UnloadContent();

			OnClick = null;

			if (ManagedFonts)
			{
				Font?.Dispose();
				Font = null;

				HighlightedFont?.Dispose();
				HighlightedFont = null;
			}
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Computes the text draw position, accounting for horizontal alignment and screen transitions.
		/// </summary>
		/// <param name="screen">The screen used to compute the transition offset.</param>
		/// <returns>The position at which to draw the text.</returns>
		protected Vector2 TextPosition(IScreen screen)
		{
			//get the draw position
			switch (Horizontal)
			{
				case HorizontalAlignment.Left:
					{
						return TransitionObject.Position(screen, new Point(Rect.X, Rect.Y));
					}
				case HorizontalAlignment.Center:
					{
						return TransitionObject.Position(screen, new Point(Rect.Center.X, Rect.Y));
					}
				default:
					{
						return TransitionObject.Position(screen, new Point(Rect.Right, Rect.Y));
					}
			}
		}

		/// <inheritdoc/>
		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen))
			{
				return;
			}

			//Get the font to use
			var font = GetFont();

			var screenTransition = TransitionObject.GetScreenTransition(screen);

			//make sure the shadow color is correct
			var shadow = font as ShadowTextBuddy;
			if (null != shadow)
			{
				shadow.ShadowColor = screenTransition.AlphaColor(GetShadowColor());
			}

			//adjust the pulsate scale
			var pulsate = font as PulsateBuddy;
			if (Highlightable && null != pulsate)
			{
				pulsate.PulsateSize = IsClicked ? 1.2f : 1f;
			}

			//Write the text
			font.Write(_renderedText,
				TextPosition(screen),
				AlignmentToJustify(),
				Scale,
				screenTransition.AlphaColor(GetColor()),
				screen.ScreenManager.SpriteBatch,
				HighlightClock);
		}

		/// <inheritdoc/>
		protected override void CalculateRect()
		{
			//get the size of the rect
			var font = GetFont();
			var size = MeasureText(font) * Scale;

			//set the x component
			Vector2 pos = Position.ToVector2();
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X -= size.X / 2f; } break;
				case HorizontalAlignment.Right: { pos.X -= size.X; } break;
			}

			//set the y component
			switch (Vertical)
			{
				case VerticalAlignment.Center: { pos.Y -= size.Y / 2f; } break;
				case VerticalAlignment.Bottom: { pos.Y -= size.Y; } break;
			}

			_rect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);
		}

		/// <summary>
		/// Updates the rendered text string, applying password masking if <see cref="IsPassword"/> is <c>true</c>.
		/// </summary>
		protected void SetRenderText()
		{
			if (!IsPassword)
			{
				_renderedText = _text;
			}
			else
			{
				var textBuilder = new StringBuilder();
				textBuilder.Append('*', _text?.Length ?? 0);
				_renderedText = textBuilder.ToString();
			}
		}

		/// <summary>
		/// Converts the current <see cref="HorizontalAlignment"/> to the equivalent <see cref="Justify"/> value.
		/// </summary>
		/// <returns>The justify value matching the current horizontal alignment.</returns>
		private Justify AlignmentToJustify()
		{
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: return Justify.Center;
				case HorizontalAlignment.Left: return Justify.Left;
				default: return Justify.Right;
			}
		}

		/// <summary>
		/// Returns the appropriate font based on the current highlight state.
		/// </summary>
		/// <returns>The highlighted font if highlighted and available; otherwise, the normal font.</returns>
		protected virtual IFontBuddy GetFont()
		{
			if (!IsHighlighted || null == HighlightedFont)
			{
				return Font;
			}
			else
			{
				return HighlightedFont;
			}
		}

		/// <summary>
		/// Returns the text color based on the current state (custom override, clicked, highlighted, or neutral).
		/// </summary>
		/// <returns>The color to use when drawing text.</returns>
		protected virtual Color GetColor()
		{
			if (TextColor.HasValue)
			{
				return TextColor.Value;
			}
			else if (IsClicked)
			{
				return StyleSheet.SelectedTextColor;
			}
			else if (IsHighlighted)
			{
				return StyleSheet.HighlightedTextColor;
			}
			else
			{
				return StyleSheet.NeutralTextColor;
			}
		}

		/// <summary>
		/// Returns the shadow color, using the custom override if set, or the style sheet default.
		/// </summary>
		/// <returns>The color to use for the text shadow.</returns>
		protected virtual Color GetShadowColor()
		{
			if (ShadowColor.HasValue)
			{
				return ShadowColor.Value;
			}
			else
			{
				return StyleSheet.TextShadowColor;
			}
		}

		/// <summary>
		/// Labels do not handle clicks directly. Always returns <c>false</c>.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns>Always <c>false</c>.</returns>
		public bool CheckClick(ClickEventArgs click)
		{
			return false;
		}

		/// <summary>
		/// Measures the rendered text using the specified font.
		/// </summary>
		/// <param name="font">The font to measure with.</param>
		/// <returns>The size of the rendered text, or <see cref="Vector2.Zero"/> if no text or font is available.</returns>
		private Vector2 MeasureText(IFontBuddy font)
		{
			if (!string.IsNullOrEmpty(_renderedText) && null != font)
			{
				return font.MeasureString(_renderedText);
			}
			else
			{
				return Vector2.Zero;
			}
		}

		/// <inheritdoc/>
		public void ScaleToFit(int rowWidth)
		{
			Scale = Font.ScaleToFit(Text, rowWidth);
		}

		/// <inheritdoc/>
		public void ShrinkToFit(int rowWidth)
		{
			if (Font.NeedsToShrink(Text, Scale, rowWidth))
			{
				Scale = Font.ShrinkToFit(Text, rowWidth);
			}
		}

		#endregion //Methods
	}
}