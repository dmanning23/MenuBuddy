using TouchScreenBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for games that use touch screen input.
	/// Automatically sets up touch input handling and disables highlight sounds.
	/// </summary>
	public abstract class TouchGame : DefaultGame
	{
		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="TouchGame"/> class.
		/// </summary>
		protected TouchGame() : base(GameType.Touch)
		{
		}

		/// <summary>
		/// Initializes touch input handling for this game.
		/// </summary>
		protected override void InitInput()
		{
			//create the touch manager component
			InputHelper = new TouchComponent(this, ResolutionBuddy.Resolution.ScreenToGameCoord);

			//add the input helper for menus
			var input = new TouchInputHelper(this);
		}

		/// <summary>
		/// Initializes default styles for touch-based games.
		/// Disables highlight sounds since touch interfaces don't have hover states.
		/// </summary>
		protected override void InitStyles()
		{
			base.InitStyles();

			//don't make a highlight sound
			StyleSheet.HighlightedSoundResource = null;
		}

		#endregion //Methods
	}
}
