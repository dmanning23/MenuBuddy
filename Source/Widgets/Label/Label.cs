using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using MouseBuddy;

namespace MenuBuddy
{
	public class Label : Widget, ILabel
	{
		#region Fields

		private string _text;

		#endregion //Fields

		#region Properties

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

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="text"></param>
		public Label(string text = "")
		{
			Text = text;
		}

		public Label(Label inst) : base(inst)
		{
			_text = inst._text;
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

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			if (!ShouldDraw(screen))
			{
				return;
			}

			//Get the font to use
			var font = Highlight ? Style.SelectedFont : Style.UnselectedFont;

			//get the shadow color
			var shadow = font as ShadowTextBuddy;
			if (null != shadow)
			{
				var shadowColor = Highlight ? Style.SelectedShadowColor : Style.UnselectedShadowColor;
				shadow.ShadowColor = screen.Transition.AlphaColor(shadowColor);
			}

			//get the color to use
			var color = Highlight ? Style.SelectedTextColor : Style.UnselectedTextColor;
			color = screen.Transition.AlphaColor(color);

			//Write the text
			font.Write(Text,
				TextPosition(screen),
				AlignmentToJustify(),
				Scale,
				color,
				screen.ScreenManager.SpriteBatch,
				HighlightClock);
		}

		protected override void CalculateRect()
		{
			//get the size of the rect
			var size = Style.SelectedFont.MeasureString(Text);
			size = (size + (Padding * 2f)) * Scale;

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

		#endregion //Methods
	}
}