using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// A widget is a screen item that can be displayed
	/// </summary>
	public abstract class Widget : IWidget
	{
		#region Properties

		/// <summary>
		/// The area of this item
		/// </summary>
		public virtual Rectangle Rect { get; set; }

		/// <summary>
		/// set the position of the item
		/// </summary>
		public virtual Point Position
		{
			get { return Rect.Location; }
			set
			{ 
				Rect = new Rectangle(value.X, value.Y, Rect.Width, Rect.Height);
			}
		}

		/// <summary>
		/// The stylesheet of this item
		/// </summary>
		public StyleSheet Style { get; private set; }

		#endregion //Properties

		#region Methods

		#endregion //Methods

		/// <summary>
		/// constructor!
		/// </summary>
		/// <param name="style"></param>
		protected Widget(StyleSheet style)
		{
			Style = style;
		}

		public abstract void Update(IScreen screen, GameClock gameTime);

		public virtual void DrawBackground(IScreen screen, GameClock gameTime)
		{
			//darw the background rectangle if in touch mode
			if (screen.IsActive)
			{
				//draw the rect!
				screen.ScreenManager.DrawHelper.DrawBackground(screen.Transition, Style, Rect);

				//draw the outline!
				screen.ScreenManager.DrawHelper.DrawOutline(screen.Transition, Style, Rect);
			}
		}

		public abstract void Draw(IScreen screen, GameClock gameTime);
	}
}