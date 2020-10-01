using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is some boilerplate code for doing a game.
	/// </summary>
	public abstract class DefaultGame : Game
	{
		#region Properties

		public IInputHelper InputHelper { get; protected set; }

		/// <summary>
		/// The graphics device used by this game
		/// </summary>
		public GraphicsDeviceManager Graphics { get; private set; }

		/// <summary>
		/// The screenmanager used by this game
		/// </summary>
		protected ScreenManager ScreenManager { get; private set; }

		public ResolutionComponent ResolutionComponent { get; set; }

		public GameType GameType { get; private set; }

		protected Point VirtualResolution { get; set; }

		protected Point ScreenResolution { get; set; }

		protected bool Fullscreen { get; set; }

		protected bool? UseDeviceResolution { get; set; }

		protected bool Letterbox { get; set; }

		protected bool LoadContentWithLoadingScreen { get; set; } = true;

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

		public string ContentRootDirectory { get; protected set; }

		#endregion //Properties

		#region Methods

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
			ResolutionComponent = new ResolutionComponent(this, Graphics, VirtualResolution, ScreenResolution, Fullscreen, Letterbox, UseDeviceResolution);
			InitStyles();

			// Create the screen manager component.
			ScreenManager = new ScreenManager(this, GetMainMenuScreenStack);

			base.Initialize();
		}

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
		/// initialize the input to use for this game
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
