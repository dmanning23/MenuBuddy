using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public class Shim : Widget, IShim
	{
		#region Properties

		public override Rectangle Rect
		{
			get { return base.Rect; }
			set
			{
				//set the rectangle
				var size = new Vector2(value.Width, value.Height);
				size += Padding * 2;
				base.Rect = new Rectangle(value.X, value.Y, (int)size.X, (int)size.Y);
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		/// <param name="style"></param>
		public Shim(StyleSheet style)
			: base(style)
		{
		}

		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
		}

		#endregion //Methods
	}
}