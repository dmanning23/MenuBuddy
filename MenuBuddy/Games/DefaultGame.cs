using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Abstract base class for MenuBuddy games. Provides boilerplate setup for screen management,
	/// resolution handling, and input initialization.
	/// </summary>
	public abstract class DefaultGame : Game
	{
		#region Properties

		/// <summary>
		/// Gets or sets the input helper used for processing user input.
		/// </summary>
		public IInputHelper InputHelper { get; protected set; }

		/// <summary>
		/// Gets the graphics device manager used by this game.
		/// </summary>
		public GraphicsDeviceManager Graphics { get; private set; }

		/// <summary>
		/// Gets the screen manager used by this game.
		/// </summary>
		protected ScreenManager ScreenManager { get; private set; }

		/// <summary>
		/// Gets or sets the resolution component for handling screen resolution and scaling.
		/// </summary>
		public ResolutionComponent ResolutionComponent { get; set; }

		/// <summary>
		/// Gets the input type used by this game (Controller, Mouse, or Touch).
		/// </summary>
		public GameType GameType { get; private set; }

		/// <summary>
		/// Gets or sets the virtual resolution used for game logic and rendering.
		/// </summary>
		protected Point VirtualResolution { get; set; }

		/// <summary>
		/// Gets or sets the actual screen resolution.
		/// </summary>
		protected Point ScreenResolution { get; set; }

		/// <summary>
		/// Gets or sets whether the game runs in fullscreen mode.
		/// </summary>
		protected bool Fullscreen { get; set; }

		/// <summary>
		/// Gets or sets whether to use the device's native resolution. If null, uses <see cref="ScreenResolution"/>.
		/// </summary>
		protected bool? UseDeviceResolution { get; set; }

		/// <summary>
		/// Gets or sets whether to use letterboxing when aspect ratios don't match.
		/// </summary>
		protected bool Letterbox { get; set; }

		/// <summary>
		/// Gets or sets whether to display a loading screen while loading initial content. Default is true.
		/// </summary>
		protected bool LoadContentWithLoadingScreen { get; set; } = true;

		/// <summary>
		/// Throws an exception to prevent use of Game.Content. Use screen-specific content managers instead.
		/// </summary>
		/// <exception cref="System.Exception">Always thrown to prevent direct access.</exception>
		public new ContentManager Content
		{
			get
			{
				throw new System.Exception("Don't use the Game.Content ContentManager!");
			}
			set
			{
			}
		}

		/// <summary>
		/// Gets or sets the root directory for content loading. Default is "Content".
		/// </summary>
		public string ContentRootDirectory { get; protected set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new instance of the <see cref="DefaultGame"/> class.
		/// </summary>
		/// <param name="gameType">The input type used by this game.</param>
		protected DefaultGame(GameType gameType)
		{
			Graphics = new GraphicsDeviceManager(this)
			{
				GraphicsProfile = GraphicsProfile.HiDef
			};
			Graphics.SupportedOrientations = DisplayOrientation.LandscapeLeft | DisplayOrientation.LandscapeRight;
			ContentRootDirectory = "Content";

			VirtualResolution = new Point(1280, 720);
			ScreenResolution = new Point(1280, 720);
			Fullscreen = false;
			Letterbox = false;
			UseDeviceResolution = null;

			GameType = gameType;

			//add the input helper for menus
			InitInput();
		}

		/// <summary>
		/// Gets the initial stack of screens to display when the game starts.
		/// </summary>
		/// <returns>An array of screens to add to the screen manager, typically including a background and main menu.</returns>
		public abstract IScreen[] GetMainMenuScreenStack();

		/// <summary>
		/// Allows the game to perform any initialization it needs to before starting to run.
		/// This is where it can query for any required services and load any non-graphic
		/// related content.  Calling base.Initialize will enumerate through any components
		/// and initialize them as well.
		/// </summary>
		protected override void Initialize()
		{
			ResolutionComponent = new ResolutionComponent(this, Graphics, VirtualResolution, ScreenResolution, Fullscreen, Letterbox, UseDeviceResolution);
			InitStyles();

			// Create the screen manager component.
			ScreenManager = new ScreenManager(this, GetMainMenuScreenStack);

			base.Initialize();
		}

		/// <summary>
		/// Loads game content and initializes the main menu screens.
		/// </summary>
		protected override void LoadContent()
		{
			try
			{
				if (LoadContentWithLoadingScreen)
				{
					// Activate the first screens.
					LoadingScreen.Load(ScreenManager, GetMainMenuScreenStack());
				}
				else
				{
					ScreenManager.AddScreen(GetMainMenuScreenStack(), null);
				}
			}
			catch (Exception ex)
			{
				ScreenManager.ErrorScreen(ex);
			}

			base.LoadContent();
		}

		/// <summary>
		/// Initialize the default styles to use for this game.
		/// </summary>
		protected virtual void InitStyles()
		{
		}

		/// <summary>
		/// Initializes the input system for this game. Must be implemented by derived classes.
		/// </summary>
		protected abstract void InitInput();

		/// <summary>
		/// This is called when the game should draw itself.
		/// </summary>
		/// <param name="gameTime">Provides a snapshot of timing values.</param>
		protected override void Draw(GameTime gameTime)
		{
			// Clear to Black
			Graphics.GraphicsDevice.Clear(ScreenManager.ClearColor);

			// The real drawing happens inside the screen manager component.
			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}
