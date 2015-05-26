using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public class Label : Widget, ILabel
	{
		#region Properties

		public override Rectangle Rect
		{
			get { return base.Rect; }
			set
			{
				//set the rectangle
				var size = Style.SelectedFont.Font.MeasureString(Text) * Scale;
				size += Padding * 2;
				base.Rect = new Rectangle(value.X, value.Y, (int)size.X, (int)size.Y);
			}
		}

		/// <summary>
		/// The text of this label
		/// </summary>
		public string Text { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="style"></param>
		/// <param name="text"></param>
		public Label(StyleSheet style, string text)
			: base(style)
		{
			Text = text;
		}

		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

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
				Justify.Center,
				Scale,
				color,
				screen.ScreenManager.SpriteBatch,
				gameTime);
		}

		#endregion //Methods
	}
}