using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public class Label : Widget
	{
		#region Properties

		public override Rectangle Rect
		{
			get { return base.Rect; }
			set
			{
				//set the rectangle
				var size = Style.SelectedFont.Font.MeasureString(Text);
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
			var color = Style.SelectedTextColor;
			color = screen.Transition.AlphaColor(color);

			//set the shadow color?
			var shadow = Style.SelectedFont as ShadowTextBuddy;
			if (null != shadow)
			{
				shadow.ShadowColor = screen.Transition.AlphaColor(Style.SelectedShadowColor);
			}

			//get teh position
			var pos = new Vector2(Rect.Center.X, Rect.Top);
			pos = screen.Transition.Position(pos, Style.Transition);

			//Write the text
			Style.SelectedFont.Write(Text,
				pos,
				Justify.Center,
				1.0f,
				color,
				screen.ScreenManager.SpriteBatch,
				gameTime);
		}

		#endregion //Methods
	}
}