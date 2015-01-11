using BasicPrimitiveBuddy;
using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using TouchScreenBuddy;

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
		#region Properties

		#region Sound Effects

		/// <summary>
		/// name of sound effect to play when the menu selection changes
		/// </summary>
		private readonly string _strMenuChange;

		/// <summary>
		/// name of the sound effect when a menu item is selected
		/// </summary>
		private readonly string _strMenuSelect;

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

		#endregion //Sound Effects

		#region Fonts stuff

		/// <summary>
		/// name of the font to draw menu entries
		/// </summary>
		private readonly string _strMenuFont;

		/// <summary>
		/// name of the font for message boxes
		/// </summary>
		private readonly string _strMessageBoxFont;

		/// <summary>
		/// name of the font for the menu title
		/// </summary>
		private readonly string _strTitleFont;

		/// <summary>
		/// A font shared by all the screens, used to write menu text.
		/// This saves each screen having to bother loading their own local copy.
		/// </summary>
		public SpriteFont MenuFont { get; private set; }

		/// <summary>
		/// A font used to in message boxes.
		/// </summary>
		public SpriteFont MessageBoxFont { get; private set; }

		/// <summary>
		/// A bigger font, used to draw menu titles
		/// </summary>
		public SpriteFont TitleFont { get; private set; }

		#endregion //Fonts stuff

		#region Touch Menu stuff

		/// <summary>
		/// basic primitive for drawing the outline arounds touch buttons
		/// </summary>
		public XNABasicPrimitive Prim { get; set; }

		#if ANDROID && !OUYA
		private bool _touchMenus = true;
		#else
		private bool _touchMenus = false;
		#endif

		/// <summary>
		/// Whether or not this game uses touch menus (mouse or touch events)
		/// </summary>
		public bool TouchMenus 
		{ 
			get
			{
				return _touchMenus;
			}
			set
			{
				_touchMenus = value;
				InputState = (_touchMenus ? new InputMouseState() : new InputState());
			}
		}

		public Vector2 MousePos
		{
			get
			{
				return Resolution.ScreenToGameCoord(InputState.MousePos);
			}
		}

		/// <summary>
		/// the touch manager service component.
		/// warning: this dude might be null if the compoent isnt in this game
		/// </summary>
		public ITouchManager Touch { get; private set; }

		#endregion //Touch Menu stuff

		#region Menu Entry Colors

		/// <summary>
		/// The color to draw the text of a menu entry item when it is not selected
		/// </summary>
		/// <value>The color of the non selected.</value>
		public Color NonSelectedTextColor { get; set; }

		/// <summary>
		/// The color to draw the text of a menu entry item when it is not selected
		/// </summary>
		public Color SelectedTextColor { get; set; }

		/// <summary>
		/// The color to use for text shadow
		/// </summary>
		public Color TextShadowColor { get; set; }

		/// <summary>
		/// The color to draw the background of buttons
		/// </summary>
		public Color ButtonBackgroundColor { get; set; }

		/// <summary>
		/// The color to draw the borders around buttons
		/// </summary>
		public Color ButtonBorderColor { get; set; }

		#endregion Menu Entry Colors

		private readonly List<GameScreen> m_ScreensToUpdate = new List<GameScreen>();

		private bool m_IsInitialized;

		/// <summary>
		/// The list of screens that are currently in the game.
		/// </summary>
		/// <value>The screens.</value>
		public List<GameScreen> Screens { get; private set; }

		/// <summary>
		/// This is a special screen that you always want to stay on top of all teh other screens.
		/// Useful for something like an Insert Coin screen
		/// </summary>
		public GameScreen TopScreen { get; private set; }

		/// <summary>
		/// A default SpriteBatch shared by all the screens. 
		/// This saves each screen having to bother creating their own local instance.
		/// </summary>
		public SpriteBatch SpriteBatch { get; private set; }

		public InputState InputState { get; private set; }

		public Texture2D BlankTexture { get; private set; }

		/// <summary>
		/// The color to clear the backbuffer to
		/// </summary>
		/// <value>The color of the clear.</value>
		public Color ClearColor { get; set; }

		/// <summary>
		/// file to use for message box background
		/// </summary>
		public string MessageBoxBackgroundTextureName { get; set; }

		/// <summary>
		/// texture to use for message box backgrounds
		/// </summary>
		public Texture2D MessageBoxBackgroundTexture { get; set; }

		/// <summary>
		/// The filename of the gradient texture to use
		/// </summary>
		public string GradientTextureName { get; set; }

		/// <summary>
		/// The gradient that is drawn behind the message box, behind buttons.
		/// </summary>
		private Texture2D GradientTexture { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new screen manager component.
		/// </summary>
		protected ScreenManager(Game game,
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
			GradientTextureName = "gradient";
			MessageBoxBackgroundTextureName = "gradient";

			NonSelectedTextColor = Color.White;
			SelectedTextColor = Color.Red;
			ButtonBorderColor = Color.LightGray;
			ButtonBackgroundColor = new Color(0.0f, 0.0f, 0.2f);
			TextShadowColor = Color.Black;

			//get the touch service
			Touch = game.Services.GetService(typeof(ITouchManager)) as ITouchManager;
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
			// Load content belonging to the screen manager.
			ContentManager content = Game.Content;

			SpriteBatch = new SpriteBatch(GraphicsDevice);

			//init the basic primitive
			Prim = new XNABasicPrimitive(GraphicsDevice, SpriteBatch);
			Prim.Thickness = 5.0f;
			Prim.NumCircleSegments = 4;

			MessageBoxFont = content.Load<SpriteFont>(_strMessageBoxFont);
			MenuFont = content.Load<SpriteFont>(_strMenuFont);
			TitleFont = content.Load<SpriteFont>(_strTitleFont);

			MenuChange = content.Load<SoundEffect>(_strMenuChange);
			MenuSelect = content.Load<SoundEffect>(_strMenuSelect);

			//create a blank texture without loading some crap
			BlankTexture = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
			BlankTexture.SetData(new[] {Color.White});

			//load the textures
			GradientTexture = content.Load<Texture2D>(GradientTextureName);
			MessageBoxBackgroundTexture = content.Load<Texture2D>(MessageBoxBackgroundTextureName);

			// Tell each of the screens to load their content.
			foreach (GameScreen screen in Screens)
			{
				screen.LoadContent();
			}

			if (null != TopScreen)
			{
				TopScreen.LoadContent();
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

			if (null != TopScreen)
			{
				TopScreen.UnloadContent();
			}
		}

		#endregion //Initialization

		#region Update and Draw

		/// <summary>
		/// Allows each screen to run logic.
		/// </summary>
		public override void Update(GameTime gameTime)
		{
#if !DEBUG
			try
			{
#endif
				// Read the keyboard and gamepad.
				InputState.Update();

				// Make a copy of the master screen list, to avoid confusion if the process of updating one screen adds or removes others.
				m_ScreensToUpdate.Clear();

				//Update the top screen separate from all other screens.
				if (null != TopScreen)
				{
					TopScreen.Update(gameTime, false, false);
					TopScreen.HandleInput(InputState, gameTime);
				}

				for (int i = 0; i < Screens.Count; i++)
				{
					m_ScreensToUpdate.Add(Screens[i]);
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
						{
							coveredByOtherScreen = true;
						}
					}
				}
#if !DEBUG
			}
			catch (Exception ex)
			{
				ErrorScreen(ex);
			}
#endif
		}

		/// <summary>
		/// Tells each screen to draw itself.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
#if !DEBUG
			try
			{
#endif
				for (int i = 0; i < Screens.Count; i++)
				{
					if (Screens[i].ScreenState == EScreenState.Hidden)
					{
						continue;
					}

					Screens[i].Draw(gameTime);
				}

				//draw the top screen
				if (null != TopScreen)
				{
					TopScreen.Draw(gameTime);
				}

#if MOUSE
				if (TouchMenus)
				{
					var mouse = Mouse.GetState();
					var mousePos = new Vector2((float)mouse.X, (float)mouse.Y);

					SpriteBatch.Begin();
					//draw a circle around the mouse cursor
					Prim.NumCircleSegments = 4;
					Prim.Circle(mousePos, 6.0f, Color.Red);
					SpriteBatch.End();
				}
#endif

#if TOUCH
				if (TouchMenus)
				{
					SpriteBatch.Begin();
					//go though the points that are being touched
					TouchCollection touchCollection = TouchPanel.GetState();
					foreach (var touch in touchCollection)
					{
						if ((touch.State == TouchLocationState.Pressed) || (touch.State == TouchLocationState.Moved))
						{
							//draw a circle around each touch point
							Prim.Circle(touch.Position, 40.0f, new Color(1.0f, 1.0f, 1.0f, 0.25f));
						}
					}
					SpriteBatch.End();
				}
#endif

#if !DEBUG
			}
			catch (Exception ex)
			{
				ErrorScreen(ex);
			}
#endif
		}

		/// <summary>
		/// A simple way to start the spritebatch from a gamescreen
		/// </summary>
		public void SpriteBatchBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred)
		{
			SpriteBatch.Begin(sortMode,
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
		public virtual void AddScreen(GameScreen screen, PlayerIndex? controllingPlayer = null)
		{
#if !DEBUG
			try
			{
#endif
				screen.ControllingPlayer = controllingPlayer;
				screen.ScreenManager = this;
				screen.IsExiting = false;

				// If we have a graphics device, tell the screen to load content.
				if (m_IsInitialized)
				{
					screen.LoadContent();
				}

				Screens.Add(screen);
#if !DEBUG
			}
			catch (Exception ex)
			{
				ErrorScreen(ex);
			}
#endif
		}

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		public virtual void AddScreen(GameScreen[] screens, PlayerIndex? controllingPlayer = null)
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
		/// Set the top screen
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="controllingPlayer"></param>
		public virtual void SetTopScreen(GameScreen screen, PlayerIndex? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;
			screen.IsExiting = false;

			// If we have a graphics device, tell the screen to load content.
			if (m_IsInitialized)
			{
				screen.LoadContent();
			}

			TopScreen = screen;
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
		/// Helper draws a translucent black fullscreen sprite, used for fading
		/// screens in and out, and for darkening the background behind popups.
		/// </summary>
		public void FadeBackBufferToBlack(float fAlpha)
		{
			FilledRect(fAlpha, Resolution.ScreenArea);
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void FilledRect(float fAlpha, Rectangle rect)
		{
			SpriteBatch.Draw(BlankTexture, rect, new Color(0.0f, 0.0f, 0.0f, fAlpha));
		}

		/// <summary>
		/// Helper draws a translucent black sprite, used for fading specific areas
		/// </summary>
		public void DrawButtonBackground(byte alpha, Rectangle rect)
		{
			//get the color for the background & border
			Color backgroundColor = ButtonBackgroundColor;
			backgroundColor.A = alpha;
			Color borderColor = ButtonBorderColor;
			borderColor.A = alpha;

			//draw the filled background
			SpriteBatch.Draw(GradientTexture, rect, backgroundColor);

			//draw the button outline
			Prim.Rectangle(rect, borderColor);
		}

		/// <summary>
		/// This method pops up a recoverable error screen.
		/// </summary>
		/// <param name="ex">the exception that occureed</param>
		public void ErrorScreen(Exception ex)
		{
			var screens = new List<GameScreen>(GetMainMenuScreenStack());
			screens.Add(new ErrorScreen(ex));
			LoadingScreen.Load(this, false, null, screens.ToArray());
		}

		public GameScreen FindScreen(string screenName)
		{
			return Screens.Find(m => m.ScreenName == screenName);
		}

		#region screen stack methods

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
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public abstract GameScreen[] GetMainMenuScreenStack();

		#endregion //screen stack methods

		#endregion //Public Methods
	}
}