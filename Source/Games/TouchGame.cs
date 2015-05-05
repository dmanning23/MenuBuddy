using MenuBuddy;
using MouseBuddy;
using ResolutionBuddy;
using TouchScreenBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a controller based game.
	/// </summary>
	public abstract class TouchGame : DefaultGame
	{
		#region Methods

		protected TouchGame()
		{
		}

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected override void InitStyles()
		{
			var gameStyle = new StyleSheet();

			//uncomment this line if need to test widget placement
			//gameStyle.HasOutline = true;

			GameStyle = new TouchStyles(this, gameStyle);

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
			//create the touch manager component
			var touches = new TouchManager(this, Resolution.ScreenToGameCoord);

#if MOUSE
			var mouse = new MouseManager(this);
#endif

			//add the input helper for menus
			var input = new TouchInputHelper(this);
			//var input = new ControllerInputHelper(this);
		}

		#endregion //Methods
	}
}
