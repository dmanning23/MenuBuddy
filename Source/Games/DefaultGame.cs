using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;

namespace MenuBuddy
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

		/// <summary>
		/// The desired resolution of the game
		/// </summary>
		protected Point DesiredScreenResolution { get; set; }

		/// <summary>
		/// whether or not the game should be displayed in fullscreen
		/// </summary>
		protected bool FullScreen { get; set; }

		#endregion //Properties

		#region Methods

		protected DefaultGame()
		{
			Graphics = new GraphicsDeviceManager(this);
			Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft;
			Content.RootDirectory = "Content";

			DesiredScreenResolution = new Point(1280, 720);
			FullScreen = false;

			DefaultStyles.MenuTitleFontName = @"Fonts\ArialBlack72";
			DefaultStyles.MenuEntryFontName = @"Fonts\ArialBlack48";
			DefaultStyles.MessageBoxFontName = @"Fonts\ArialBlack24";
			DefaultStyles.MenuSelectSoundName = @"MenuSelect";
			DefaultStyles.MenuChangeSoundName = @"MenuMove";
			DefaultStyles.MessageBoxBackground = @"gradient";
			DefaultStyles.ButtonBackground = @"AlphaGradient";
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

			//add the input helper for menus
			InitInput();

			InitStyles();

			// Create the screen manager component.
			ScreenManager = new ScreenManager(this, GetMainMenuScreenStack);

			//Change Virtual Resolution
			Resolution.SetDesiredResolution(DesiredScreenResolution.X, DesiredScreenResolution.Y);

			//set the desired resolution
			Resolution.SetScreenResolution(DesiredScreenResolution.X, DesiredScreenResolution.Y, FullScreen);

			// Activate the first screens.
			ScreenManager.AddScreen(GetMainMenuScreenStack(), null);

			base.Initialize();
		}

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected abstract void InitStyles();

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
