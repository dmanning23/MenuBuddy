using MenuBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a controller based game.
	/// </summary>
	public abstract class ControllerGame : DefaultGame
	{
		#region Methods

		protected ControllerGame()
		{
		}

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected override void InitStyles()
		{
			var gameStyle = new StyleSheet();

			GameStyle = new DefaultStyles(this, gameStyle);

			GameStyle.MenuTitleFontName = @"Fonts\ArialBlack72";
			GameStyle.MenuEntryFontName = @"Fonts\ArialBlack48";
			GameStyle.MessageBoxFontName = @"Fonts\ArialBlack24";
			GameStyle.MenuSelectSoundName = @"MenuSelect";
			GameStyle.MenuChangeSoundName = @"MenuMove";
			GameStyle.MessageBoxBackground = @"gradient";
			GameStyle.ButtonBackground = @"AlphaGradient";
		}

		protected override void InitInput()
		{
			//add the input helper for menus
			//var input = new TouchInputHelper(this);
			var input = new ControllerInputHelper(this);
		}

		#endregion //Methods
	}
}
