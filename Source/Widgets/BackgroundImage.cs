using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	public class BackgroundImage : Image
	{
		#region Members

		/// <summary>
		/// Override the rectangle to return the area of the whole screen
		/// </summary>
		public override Rectangle Rect
		{
			get { return ResolutionBuddy.Resolution.ScreenArea; }
			set { base.Rect = value; }
		}

		#endregion //Members

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public BackgroundImage(StyleSheet styleSheet)
			: base(styleSheet)
		{
		}

		#endregion //Initialization
	}
}