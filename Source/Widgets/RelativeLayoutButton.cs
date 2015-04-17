using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button thhat contains a relaitve layout
	/// </summary>
	public class RelativeLayoutButton : Button
	{
		#region Properties

		public override Rectangle Rect
		{
			get { return base.Rect; }
			set 
			{ 
				base.Rect = value;
Layout.Rectangle = value;			
			}
		}

		public override Point Position
		{
			get { return base.Position; }
			set
			{
				base.Position = value;
				Layout.Position = value;
			}
		}

		/// <summary>
		/// The layout to add to this button
		/// </summary>
		public RelativeLayout Layout { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public RelativeLayoutButton(StyleSheet style, string text)
			: base(style, text)
		{
			Layout = new RelativeLayout();
		}

		#endregion

		#region Update and Draw

		public override void Update(IScreen screen, GameClock gameTime)
		{
			Layout.Update(screen, gameTime);
			base.Update(screen, gameTime);
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			base.Draw(screen, gameTime);
			Layout.Draw(screen, gameTime);
		}

		#endregion
	}
}