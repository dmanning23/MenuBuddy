using System;
using System.Diagnostics;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using HadoukInput;

namespace MenuBuddy
{
	/// <summary>
	/// The screen manager is a component which manages one or more GameScreen
	/// instances. It maintains a stack of screens, calls their Update and Draw
	/// methods at the appropriate times, and automatically routes input to the
	/// topmost active screen.
	/// </summary>
	public abstract class ScreenManager : DrawableGameComponent
	{
		#region Member Variables

		private List<GameScreen> m_Screens = new List<GameScreen>();

		private List<GameScreen> m_ScreensToUpdate = new List<GameScreen>();

		private bool m_IsInitialized;

		#endregion //Member Variables

		#region Properties

		/// <summary>
		/// A default SpriteBatch shared by all the screens. 
		/// This saves each screen having to bother creating their own local instance.
		/// </summary>
		public SpriteBatch SpriteBatch { get; private set; }

		/// <summary>
		/// A default font shared by all the screens. 
		/// This saves each screen having to bother loading their own local copy.
		/// </summary>
		public SpriteFont Font { get; private set; }

		/// <summary>
		/// A bigger font, used to draw menu titles
		/// </summary>
		public SpriteFont MenuTitleFont { get; private set; }

		public InputState InputState { get; private set; }

		public Texture2D BlankTexture { get; private set; }

		//TODO: create some delegates for playing sounds

		//TODO: create a few methods for playing default sounds like "menu change" "menu select"

		/// <summary>
		/// content manager used to load music files
		/// </summary>
		public ContentManager m_MusicContent;

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new screen manager component.
		/// </summary>
		public ScreenManager(Game game) : base(game)
		{
			InputState = new InputState();
		}

		/// <summary>
		/// Initializes the screen manager component.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();
			m_IsInitialized = true;
		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent()
		{
			//create the music content manager
			m_MusicContent = new ContentManager(Game.Services, "Content");

			// Load content belonging to the screen manager.
			ContentManager content = Game.Content;

			this.SpriteBatch = new SpriteBatch(GraphicsDevice);

			//TODO: take out this hard coded reference to default font
			Font = content.Load<SpriteFont>("ArialBlack48");

			//TODO: take out this hard coded reference to menu font
			MenuTitleFont = content.Load<SpriteFont>("ArialBlack72");

			//create a blank texture without loading some crap
			BlankTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			BlankTexture.SetData<Color>(new Color[] { Color.White });

			// Tell each of the screens to load their content.
			foreach (GameScreen screen in m_Screens)
			{
				screen.LoadContent();
			}
		}

		/// <summary>
		/// Unload your graphics content.
		/// </summary>
		protected override void UnloadContent()
		{
			// Tell each of the screens to unload their content.
			foreach (GameScreen screen in m_Screens)
			{
				screen.UnloadContent();
			}

			m_MusicContent.Unload();
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows each screen to run logic.
		/// </summary>
		public override void Update(GameTime gameTime)
		{
			// Read the keyboard and gamepad.
			InputState.Update();

			// Make a copy of the master screen list, to avoid confusion if
			// the process of updating one screen adds or removes others.
			m_ScreensToUpdate.Clear();

			foreach (GameScreen screen in m_Screens)
			{
				m_ScreensToUpdate.Add(screen);
			}

			bool otherScreenHasFocus = !Game.IsActive;
			bool coveredByOtherScreen = false;

			// Loop as long as there are screens waiting to be updated.
			while (m_ScreensToUpdate.Count > 0)
			{
				// Pop the topmost screen off the waiting list.
				GameScreen screen = m_ScreensToUpdate[m_ScreensToUpdate.Count - 1];

				m_ScreensToUpdate.RemoveAt(m_ScreensToUpdate.Count - 1);

				// Update the screen.
				screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

				if (screen.ScreenState == EScreenState.TransitionOn ||
					screen.ScreenState == EScreenState.Active)
				{
					// If this is the first active screen we came across,
					// give it a chance to handle input.
					if (!otherScreenHasFocus)
					{
						screen.HandleInput(InputState, gameTime);

						otherScreenHasFocus = true;
					}

					// If this is an active non-popup, inform any subsequent
					// screens that they are covered by it.
					if (!screen.IsPopup)
						coveredByOtherScreen = true;
				}
			}
		}

		/// <summary>
		/// Prints a list of all the screens, for debugging.
		/// </summary>
		void TraceScreens()
		{
			List<string> screenNames = new List<string>();

			foreach (GameScreen screen in m_Screens)
				screenNames.Add(screen.GetType().Name);

			//Trace.WriteLine(string.Join(", ", screenNames.ToArray()));
		}

		/// <summary>
		/// Tells each screen to draw itself.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			foreach (GameScreen screen in m_Screens)
			{
				if (screen.ScreenState == EScreenState.Hidden)
					continue;

				screen.Draw(gameTime);
			}
		}

		#endregion

		#region Public Methods

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		public void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;
			screen.IsExiting = false;

			// If we have a graphics device, tell the screen to load content.
			if (m_IsInitialized)
			{
				screen.LoadContent();
			}

			m_Screens.Add(screen);
		}

		/// <summary>
		/// Removes a screen from the screen manager. You should normally
		/// use GameScreen.ExitScreen instead of calling this directly, so
		/// the screen can gradually transition off rather than just being
		/// instantly removed.
		/// </summary>
		public void RemoveScreen(GameScreen screen)
		{
			// If we have a graphics device, tell the screen to unload content.
			if (m_IsInitialized)
			{
				screen.UnloadContent();
			}

			m_Screens.Remove(screen);
			m_ScreensToUpdate.Remove(screen);

			//reset the times of all the rest of teh screens
			for (int i = 0; i < m_Screens.Count; i++)
			{
				m_Screens[i].TimeSinceInput = 0.0;
				m_Screens[i].PrevTimeSinceInput = 0.0;
			}
		}

		/// <summary>
		/// Expose an array holding all the screens. We return a copy rather
		/// than the real master list, because screens should only ever be added
		/// or removed using the AddScreen and RemoveScreen methods.
		/// </summary>
		public GameScreen[] GetScreens()
		{
			return m_Screens.ToArray();
		}

		/// <summary>
		/// Helper draws a translucent black fullscreen sprite, used for fading
		/// screens in and out, and for darkening the background behind popups.
		/// </summary>
		public void FadeBackBufferToBlack(float fAlpha)
		{
			Viewport viewport = GraphicsDevice.Viewport;

			SpriteBatch.Begin();

			SpriteBatch.Draw(BlankTexture,
							 new Rectangle(0, 0, viewport.Width, viewport.Height),
							 new Color(0.0f, 0.0f, 0.0f, fAlpha));

			SpriteBatch.End();
		}

		public void PlayMusic(string strMusic)
		{
			//TODO: first stop the music
			//SPFLib.CAudioManager.StopMusic();

			//unload the music
			m_MusicContent.Unload();
			
			//TODO: start playing the new music
			//SPFLib.CAudioManager.PlayMusic(strMusic, m_MusicContent);
		}

		#region screen stack methods

		/// <summary>
		/// Get the set of screens needed to start a game
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		/// <param name="iNumPlayers">the number of players in the game.</param>
		public abstract GameScreen[] GetGameplayScreenStack(int iNumPlayers);

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public abstract GameScreen[] GetMainMenuScreenStack();

		/// <summary>
		/// Get the set of screens needed to display the network busy screen
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public abstract GameScreen[] GetNetworkBusyScreenStack();

		#endregion //screen stack methods

		#endregion
	}
}