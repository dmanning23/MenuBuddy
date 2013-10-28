using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Collections.Generic;

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

		private List<GameScreen> m_ScreensToUpdate = new List<GameScreen>();

		private bool m_IsInitialized;

		//Font names
		private string _strMessageBoxFont;
		private string _strMenuFont;
		private string _strTitleFont;

		//Sound effect names
		private string _strMenuChange;
		private string _strMenuSelect;

		#endregion //Member Variables

		#region Properties

		/// <summary>
		/// The list of screens that are currently in the game.
		/// </summary>
		/// <value>The screens.</value>
		public List<GameScreen> Screens { get; private set; }

		/// <summary>
		/// A default SpriteBatch shared by all the screens. 
		/// This saves each screen having to bother creating their own local instance.
		/// </summary>
		public SpriteBatch SpriteBatch { get; private set; }

		/// <summary>
		/// A font used to in message boxes.
		/// </summary>
		public SpriteFont MessageBoxFont { get; private set; }

		/// <summary>
		/// A font shared by all the screens, used to write menu text.
		/// This saves each screen having to bother loading their own local copy.
		/// </summary>
		public SpriteFont MenuFont { get; private set; }

		/// <summary>
		/// A bigger font, used to draw menu titles
		/// </summary>
		public SpriteFont TitleFont { get; private set; }

		public InputState InputState { get; private set; }

		public Texture2D BlankTexture { get; private set; }

		/// <summary>
		/// sound effect for when the menu selection changes
		/// </summary>
		/// <value>The menu change.</value>
		public SoundEffect MenuChange { get; private set; }

		/// <summary>
		/// sound effect for when a menu item is selected
		/// </summary>
		/// <value>The menu select.</value>
		public SoundEffect MenuSelect { get; private set; }

		/// <summary>
		/// content manager used to load music files
		/// </summary>
		public ContentManager m_MusicContent;

		/// <summary>
		/// The color to clear the backbuffer to
		/// </summary>
		/// <value>The color of the clear.</value>
		public Color ClearColor { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new screen manager component.
		/// </summary>
		public ScreenManager(Game game, 
		                     string strTitleFont, 
		                     string strMenuFont, 
		                     string strMessageBoxFont,
		                    string strMenuChange,
		                    string strMenuSelect) : base(game)
		{
			Screens = new List<GameScreen>();
			InputState = new InputState();
			ClearColor = Color.Black;
			_strMenuFont = strMenuFont;
			_strTitleFont = strTitleFont;
			_strMessageBoxFont = strMessageBoxFont;
			_strMenuChange = strMenuChange;
			_strMenuSelect = strMenuSelect;
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

			MessageBoxFont = content.Load<SpriteFont>(_strMessageBoxFont);
			MenuFont = content.Load<SpriteFont>(_strMenuFont);
			TitleFont = content.Load<SpriteFont>(_strTitleFont);

			MenuChange = content.Load<SoundEffect>(_strMenuChange);
			MenuSelect = content.Load<SoundEffect>(_strMenuSelect);

			//create a blank texture without loading some crap
			BlankTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			BlankTexture.SetData<Color>(new Color[] { Color.White });

			// Tell each of the screens to load their content.
			foreach (GameScreen screen in Screens)
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
			foreach (GameScreen screen in Screens)
			{
				screen.UnloadContent();
			}

			m_MusicContent.Unload();
		}

		#endregion //Initialization

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

			foreach (GameScreen screen in Screens)
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
		/// Tells each screen to draw itself.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			foreach (GameScreen screen in Screens)
			{
				if (screen.ScreenState == EScreenState.Hidden)
				{
					continue;
				}

				screen.Draw(gameTime);
			}

//			//draw the titlesafe area
//			SpriteBatchStart();
//			BasicPrimitive rect = new BasicPrimitive(GraphicsDevi		ce);
//			rect.Rectangle(Resolution.TitleSafeArea, Color.Red, SpriteBatch);
//			SpriteBatch.End();
		}

		/// <summary>
		/// A simple way to start the spritebatch from a gamescreen
		/// </summary>
		public void SpriteBatchBegin()
		{
			SpriteBatch.Begin(SpriteSortMode.Deferred, 
			                  BlendState.NonPremultiplied,
			                  null, null, null, null, Resolution.TransformationMatrix());
		}

		/// <summary>
		/// a simple way to end a spritebatch from a gamescreen
		/// </summary>
		public void SpriteBatchEnd()
		{
			SpriteBatch.End();
		}

		#endregion //Update and Draw

		#region Public Methods

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		public virtual void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;
			screen.IsExiting = false;

			// If we have a graphics device, tell the screen to load content.
			if (m_IsInitialized)
			{
				screen.LoadContent();
			}

			Screens.Add(screen);
		}

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		public virtual void AddScreen(GameScreen [] screens, PlayerIndex? controllingPlayer)
		{
			foreach (GameScreen screen in screens)
			{
				if (screen != null)
				{
					AddScreen(screen, controllingPlayer);
				}
			}
		}

		/// <summary>
		/// Removes a screen from the screen manager. You should normally
		/// use GameScreen.ExitScreen instead of calling this directly, so
		/// the screen can gradually transition off rather than just being
		/// instantly removed.
		/// </summary>
		public virtual void RemoveScreen(GameScreen screen)
		{
			// If we have a graphics device, tell the screen to unload content.
			if (m_IsInitialized)
			{
				screen.UnloadContent();
			}

			Screens.Remove(screen);
			m_ScreensToUpdate.Remove(screen);

			//reset the times of all the rest of teh screens
			foreach (GameScreen curScreen in Screens)
			{
				curScreen.ResetInputTimer();
			}
		}

		/// <summary>
		/// Expose an array holding all the screens. We return a copy rather
		/// than the real master list, because screens should only ever be added
		/// or removed using the AddScreen and RemoveScreen methods.
		/// </summary>
		public GameScreen[] GetScreens()
		{
			return Screens.ToArray();
		}

		/// <summary>
		/// Helper draws a translucent black fullscreen sprite, used for fading
		/// screens in and out, and for darkening the background behind popups.
		/// </summary>
		public void FadeBackBufferToBlack(float fAlpha)
		{
			SpriteBatch.Draw(BlankTexture,
							 Resolution.ScreenArea,
							 new Color(0.0f, 0.0f, 0.0f, fAlpha));
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

		/// <summary>
		/// This method pops up a recoverable error screen.
		/// </summary>
		/// <param name="ex">the exception that occureed</param>
		public void ErrorScreen(Exception ex)
		{
			List<GameScreen> screens = new List<GameScreen>(GetMainMenuScreenStack());
			screens.Add(new ErrorScreen(ex));
			LoadingScreen.Load(this, false, null, screens.ToArray());
		}

		#region screen stack methods

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public abstract GameScreen[] GetMainMenuScreenStack();

		#endregion //screen stack methods

		#endregion //Public Methods
	}
}