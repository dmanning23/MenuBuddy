using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OpenTK.Graphics.OpenGL;
using TouchScreenBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// The screen manager is a component which manages one or more GameScreen
	/// instances. It maintains a stack of screens, calls their Update and Draw
	/// methods at the appropriate times, and automatically routes input to the
	/// topmost active screen.
	/// </summary>
	public abstract class ScreenManager : DrawableGameComponent, IScreenManager
	{
		#region Properties

		#region Screen Properties

		/// <summary>
		/// A list of screens currently waiting to be updated
		/// </summary>
		private List<IScreen> ScreensToUpdate { get; set; }

		/// <summary>
		/// The list of screens that are currently in the game.
		/// </summary>
		/// <value>The screens.</value>
		public List<IScreen> Screens { get; private set; }

		/// <summary>
		/// This is a special screen that you always want to stay on top of all teh other screens.
		/// Useful for something like an Insert Coin screen
		/// </summary>
		public IScreen TopScreen { get; private set; }

		#endregion //Screen Properties

		/// <summary>
		/// Flag for whether or not this screen manager has been initialized
		/// </summary>
		private bool Initialized { get; set; }

		public IInputHelper Input { get; private set; }

		public SpriteBatch SpriteBatch { get; private set; }

		public Color ClearColor { get; set; }

		public StyleSheet Styles { get; set; }

		public DrawHelper DrawHelper { get; private set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new screen manager component.
		/// </summary>
		protected ScreenManager(Game game)
			: base(game)
		{
			Initialized = false;

			ScreensToUpdate = new List<IScreen>();
			Screens = new List<IScreen>();
			ClearColor = Color.Black;

			Styles = new StyleSheet();

			//get the touch service
			Input = game.Services.GetService(typeof(IInputHelper)) as IInputHelper;
			Debug.Assert(null != Input);

			game.Components.Add(this);
			game.Services.AddService(typeof(IScreenManager), this);
		}

		/// <summary>
		/// Called once by the monogame framework to initialize this thing
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();

			//allow the screen manager to load content from now on
			Initialized = true;
		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			DrawHelper = new DrawHelper(GraphicsDevice, SpriteBatch);

			// Tell each of the screens to load their content.
			foreach (var screen in Screens)
			{
				LoadScreenContent(screen);
			}

			if (null != TopScreen)
			{
				TopScreen.LoadContent();
			}
		}

		/// <summary>
		/// Setup the style and content of a screen
		/// </summary>
		/// <param name="screen"></param>
		private void LoadScreenContent(IScreen screen)
		{
			//set the style of teh screen
			screen.SetStyle(Styles);

			//load the screen content
			screen.LoadContent();
		}

		/// <summary>
		/// Unload your graphics content.
		/// </summary>
		protected override void UnloadContent()
		{
			// Tell each of the screens to unload their content.
			foreach (var screen in Screens)
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

		public override void Update(GameTime gameTime)
		{
#if !DEBUG
			try
			{
#endif
			// Make a copy of the master screen list, to avoid confusion if the process of updating one screen adds or removes others.
			ScreensToUpdate.Clear();

			//Update the top screen separate from all other screens.
			if (null != TopScreen)
			{
				TopScreen.Update(gameTime, false, false);
				Input.HandleInput(TopScreen);
			}

			for (int i = 0; i < Screens.Count; i++)
			{
				ScreensToUpdate.Add(Screens[i]);
			}

			bool otherScreenHasFocus = !Game.IsActive;
			bool coveredByOtherScreen = false;

			// Loop as long as there are screens waiting to be updated.
			while (ScreensToUpdate.Count > 0)
			{
				// Pop the topmost screen off the waiting list.
				var screen = ScreensToUpdate[ScreensToUpdate.Count - 1];
				ScreensToUpdate.RemoveAt(ScreensToUpdate.Count - 1);

				// Update the screen.
				screen.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

				if (screen.Transition.State == TransitionState.TransitionOn ||
					screen.Transition.State == TransitionState.Active)
				{
					// If this is the first active screen we came across,
					// give it a chance to handle input.
					if (!otherScreenHasFocus)
					{
						Input.HandleInput(screen);
						otherScreenHasFocus = true;
					}

					// If this is an active non-popup, inform any subsequent
					// screens that they are covered by it.
					if (screen.CoverOtherScreens)
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
				if (Screens[i].Transition.State == TransitionState.Hidden)
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
							  null, null, null, null,
							  Resolution.TransformationMatrix());
		}

		/// <summary>
		/// A simple way to start the spritebatch from a gamescreen
		/// </summary>
		public void SpriteBatchBegin(BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred)
		{
			SpriteBatch.Begin(sortMode,
							  blendState,
							  null, null, null, null,
							  Resolution.TransformationMatrix());
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
		public virtual void AddScreen(IScreen screen, PlayerIndex? controllingPlayer = null)
		{
#if !DEBUG
			try
			{
#endif
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;

			// If we have a graphics device, tell the screen to load content.
			if (Initialized)
			{
				LoadScreenContent(screen);
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
		public virtual void AddScreen(IScreen[] screens, PlayerIndex? controllingPlayer = null)
		{
			foreach (var screen in screens)
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
		public virtual void SetTopScreen(IScreen screen, PlayerIndex? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;

			// If we have a graphics device, tell the screen to load content.
			if (Initialized)
			{
				LoadScreenContent(screen);
			}

			TopScreen = screen;
		}

		/// <summary>
		/// Removes a screen from the screen manager. You should normally
		/// use GameScreen.ExitScreen instead of calling this directly, so
		/// the screen can gradually transition off rather than just being
		/// instantly removed.
		/// </summary>
		public virtual void RemoveScreen(IScreen screen)
		{
			// If we have a graphics device, tell the screen to unload content.
			if (Initialized)
			{
				screen.UnloadContent();
			}

			Screens.Remove(screen);
			ScreensToUpdate.Remove(screen);

			//reset the times of all the rest of teh screens
			var widgetScreens = Screens.OfType<WidgetScreen>();
			foreach (var curScreen in widgetScreens)
			{
				curScreen.ResetInputTimer();
			}
		}

		/// <summary>
		/// This method pops up a recoverable error screen.
		/// </summary>
		/// <param name="ex">the exception that occureed</param>
		public void ErrorScreen(Exception ex)
		{
			var screens = new List<IScreen>(GetMainMenuScreenStack());
			screens.Add(new ErrorScreen(ex));
			LoadingScreen.Load(this, false, null, screens.ToArray());
		}

		public IScreen FindScreen(string screenName)
		{
			return Screens.Find(m => m.ScreenName == screenName);
		}

		/// <summary>
		/// Expose an array holding all the screens. We return a copy rather
		/// than the real master list, because screens should only ever be added
		/// or removed using the AddScreen and RemoveScreen methods.
		/// </summary>
		public IScreen[] GetScreens()
		{
			return Screens.ToArray();
		}

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public abstract IScreen[] GetMainMenuScreenStack();

		#endregion //Public Methods
	}
}