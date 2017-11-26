using FontBuddyLib;
using GameTimer;
using InputHelper;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	public class Label : Widget, ILabel, IDisposable
	{
		#region Fields

		private string _text;

		private FontSize _fontSize;

		private IFontBuddy _font;

#pragma warning disable 0414
		public event EventHandler<ClickEventArgs> OnClick;
#pragma warning restore 0414

		#endregion //Fields

		#region Properties

		public bool Clickable { get; set; }

		/// <summary>
		/// The text of this label
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
					CalculateRect();
				}
			}
		}

		public FontSize FontSize
		{
			get
			{
				return _fontSize;
			}
			set
			{
				if (_fontSize != value)
				{
					_fontSize = value;
					CalculateRect();
				}
			}
		}

		public bool IsClicked { get; set; }

		/// <summary>
		/// If this is set, use it to draw this label
		/// </summary>
		public IFontBuddy Font
		{
			get
			{
				if (null == _font)
				{
					_font = GetFont();
				}
				return _font;
			}
			set
			{
				_font = value;
			}
		}

		/// <summary>
		/// If this is not null, use it as the shadow color.
		/// </summary>
		public Color? ShadowColor { get; set; }

		/// <summary>
		/// If this is not null, use it as the text color.
		/// </summary>
		public Color? TextColor { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="text"></param>
		public Label(string text = "", FontSize fontSize = FontSize.Medium)
		{
			_fontSize = fontSize;
			Text = text;
			Clickable = true;
		}

		public Label(Label inst) : base(inst)
		{
			if (null == inst)
			{
				throw new ArgumentNullException("inst");
			}

			Clickable = inst.Clickable;
			_text = inst.Text;
			_fontSize = inst.FontSize;
			TextColor = inst.TextColor;
			ShadowColor = inst.ShadowColor;
		}

		/// <summary>
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public override IScreenItem DeepCopy()
		{
			return new Label(this);
		}

		#endregion //Initialization

		#region Methods

		protected Vector2 TextPosition(IScreen screen)
		{
			//get the draw position
			switch (Horizontal)
			{
				case HorizontalAlignment.Left:
					{
						return TransitionObject.Position(screen.Transition, new Point(Rect.X, Rect.Y));
					}
				case HorizontalAlignment.Center:
					{
						return TransitionObject.Position(screen.Transition, new Point(Rect.Center.X, Rect.Y));
					}
				default:
					{
						return TransitionObject.Position(screen.Transition, new Point(Rect.Right, Rect.Y));
					}
			}
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen))
			{
				return;
			}

			//Get the font to use
			var font = GetFont();

			//make sure the shadow color is correct
			var shadow = font as ShadowTextBuddy;
			if (null != shadow)
			{
				shadow.ShadowColor = screen.Transition.AlphaColor(GetShadowColor());
			}

			//adjust the pulsate scale
			var pulsate = font as PulsateBuddy;
			if (Highlightable && null != pulsate)
			{
				pulsate.PulsateSize = IsClicked ? 1.2f : 1f;
			}

			//Write the text
			font.Write(Text,
				TextPosition(screen),
				AlignmentToJustify(),
				Scale,
				screen.Transition.AlphaColor(GetColor()),
				screen.ScreenManager.SpriteBatch,
				HighlightClock);
		}

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

		private Justify AlignmentToJustify()
		{
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: return Justify.Center;
				case HorizontalAlignment.Left: return Justify.Left;
				default: return Justify.Right;
			}
		}

		protected virtual IFontBuddy GetFont()
		{
			if (null != _font)
			{
				return _font;
			}

			switch (FontSize)
			{
				case FontSize.Large:
					{
						return IsHighlighted ? StyleSheet.Instance().LargeHighlightedFont : StyleSheet.Instance().LargeNeutralFont;
					}
				case FontSize.Medium:
					{
						return IsHighlighted ? StyleSheet.Instance().MediumHighlightedFont : StyleSheet.Instance().MediumNeutralFont;
					}
				default:
					{
						return IsHighlighted ? StyleSheet.Instance().SmallHighlightedFont : StyleSheet.Instance().SmallNeutralFont;
					}
			}
		}

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

		public bool CheckClick(ClickEventArgs click)
		{
			return false;
		}

		private Vector2 MeasureText(IFontBuddy font)
		{
			if (!string.IsNullOrEmpty(Text) && null != font)
			{
				return font.MeasureString(Text);
			}
			else
			{
				return Vector2.Zero;
			}
		}

		public void ScaleToFit(int rowWidth)
		{
			Scale = Font.ScaleToFit(Text, rowWidth);
		}

		public void ShrinkToFit(int rowWidth)
		{
			Scale = Font.ShrinkToFit(Text, rowWidth);
		}

		public override void Dispose()
		{
			base.Dispose();
			OnClick = null;
		}

		#endregion //Methods
	}
}