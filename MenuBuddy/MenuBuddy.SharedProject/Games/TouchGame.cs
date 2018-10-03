using TouchScreenBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a controller based game.
	/// </summary>
	public abstract class TouchGame : DefaultGame
	{
		#region Methods

		protected TouchGame() : base(GameType.Touch)
		{
		}

		protected override void InitInput()
		{
			//create the touch manager component
			InputHelper = new TouchComponent(this, ResolutionBuddy.Resolution.ScreenToGameCoord);

			//add the input helper for menus
			var input = new TouchInputHelper(this);
		}

		protected override void InitStyles()
		{
			base.InitStyles();

			//don't make a highlight sound
			StyleSheet.HighlightedSoundResource = null;
		}

		#endregion //Methods
	}
}
