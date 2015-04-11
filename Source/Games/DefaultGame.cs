using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;
using TouchScreenBuddy;

namespace MenuBuddySample
{
	/// <summary>
	/// This is some boilerplate code for doing a game.
	/// </summary>
	public abstract class DefaultGame : Game
	{
		#region Properties

		/// <summary>
		/// The graphics device used by this game
		/// </summary>
		protected GraphicsDeviceManager Graphics { get; private set; }

		/// <summary>
		/// The screenmanager used by this game
		/// </summary>
		protected ScreenManager ScreenManager { get; private set; }

		#endregion //Properties

		#region Methods

		protected DefaultGame()
		{
			Graphics = new GraphicsDeviceManager(this);
			Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Content.RootDirectory = "Content";
		}

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public abstract IScreen[] GetMainMenuScreenStack();

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			Resolution.Init(Graphics);

			//create the touch manager component
			var touches = new TouchManager(this, Resolution.ScreenToGameCoord);

			//add the input helper for menus
			InitInput();

			InitStyles();

			// Create the screen manager component.
			ScreenManager = new ScreenManager(this, GetMainMenuScreenStack);
			ScreenManager.ClearColor = new Color(0.0f, 0.1f, 0.2f);

			// Change Virtual Resolution
			Resolution.SetDesiredResolution(1280, 720);

			//set the desired resolution
			Resolution.SetScreenResolution(1280, 720, false);

			// Activate the first screens.
			ScreenManager.AddScreen(GetMainMenuScreenStack(), null);

			base.Initialize();
		}

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected virtual void InitStyles()
		{
			var gameStyle = new StyleSheet();
			gameStyle.HasOutline = true;
			var styles = new DefaultStyles(this, gameStyle);

			styles.MenuTitleFontName = @"Fonts\ArialBlack72";
			styles.MenuEntryFontName = @"Fonts\ArialBlack48";
			styles.MessageBoxFontName = @"Fonts\ArialBlack24";
			styles.MenuSelectSoundName = @"MenuSelect";
			styles.MenuChangeSoundName = @"MenuMove";
			styles.MessageBoxBackground = @"gradient";
			styles.ButtonBackground = @"AlphaGradient";
		}

		/// <summary>
		/// initialize the input to use for this game
		/// </summary>
		protected abstract void InitInput();

		/// <summary>
		/// Allows the game to run logic such as updating the world,
		/// checking for collisions, gathering input, and playing audio.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Update(GameTime gameTime)
		{
			// For Mobile devices, this logic will close the Game when the Back button is pressed
			if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed)
			{
				Exit();
			}

			base.Update(gameTime);
		}

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear to Black
			Graphics.GraphicsDevice.Clear(ScreenManager.ClearColor);

			// Calculate Proper Viewport according to Aspect Ratio
			Resolution.ResetViewport();

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}
