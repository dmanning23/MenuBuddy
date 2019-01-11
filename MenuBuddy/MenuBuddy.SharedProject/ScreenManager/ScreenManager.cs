using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MenuBuddy
{
	/// <summary>
	/// The screen manager is a component which manages one or more GameScreen
	/// instances. It maintains a stack of screens, calls their Update and Draw
	/// methods at the appropriate times, and automatically routes input to the
	/// topmost active screen.
	/// </summary>
	public class ScreenManager : DrawableGameComponent, IScreenManager
	{
		#region Properties

		public DefaultGame DefaultGame => Game as DefaultGame;

		public ScreenStack ScreenStack
		{
			get; set;
		}

		/// <summary>
		/// Flag for whether or not this screen manager has been initialized
		/// </summary>
		private bool Initialized { get; set; }

		public IInputHandler Input { get; private set; }

		public SpriteBatch SpriteBatch { get; private set; }

		public Color ClearColor { get; set; }

		public DrawHelper DrawHelper { get; private set; }

		public ScreenStackDelegate MainMenuStack { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new screen manager component.
		/// </summary>
		public ScreenManager(Game game, ScreenStackDelegate mainMenuStack)
			: base(game)
		{
			Initialized = false;
			MainMenuStack = mainMenuStack;

			ScreenStack = new ScreenStack();

			ClearColor = new Color(0.0f, 0.1f, 0.2f);

			//get the touch service
			Input = game.Services.GetService<IInputHandler>();

			if (null == Input)
			{
				throw new Exception("Cannot initialize ScreenManager without first adding IInputHandler service");
			}

			game.Components.Add(this);
			game.Services.AddService(typeof(IScreenManager), this);

			//When using render targets, don't clear the screen!!!
			GraphicsDevice.PresentationParameters.RenderTargetUsage = RenderTargetUsage.PreserveContents;
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

			ScreenStack.LoadContent();
		}

		/// <summary>
		/// Unload your graphics content.
		/// </summary>
		protected override void UnloadContent()
		{
			ScreenStack.UnloadContent();

			DrawHelper?.Dispose();
			DrawHelper = null;
		}

		#endregion //Initialization

		#region Update and Draw

		public override void Update(GameTime gameTime)
		{
			ScreenStack.Update(gameTime, Input, !Game.IsActive);
		}

		/// <summary>
		/// Tells each screen to draw itself.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			try
			{
				ScreenStack.Draw(gameTime);
			}
			catch (Exception ex)
			{
				ErrorScreen(ex);
			}
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
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;

			// If we have a graphics device, tell the screen to load content.
			if (Initialized)
			{
				screen.LoadContent();
			}

			ScreenStack.AddScreen(screen);
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
					screen.ControllingPlayer = controllingPlayer;
					screen.ScreenManager = this;

					// If we have a graphics device, tell the screen to load content.
					if (Initialized)
					{
						screen.LoadContent();
					}
				}
			}

			ScreenStack.AddScreen(screens);
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
				screen.LoadContent();
			}

			ScreenStack.TopScreen = screen;
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

			ScreenStack.RemoveScreen(screen);

			//reset the times of all the rest of teh screens
			var widgetScreens = ScreenStack.FindScreens<WidgetScreen>();
			foreach (var curScreen in widgetScreens)
			{
				curScreen.ResetInputTimer();
			}
		}

		public void RemoveScreens<T>() where T:IScreen
		{
			var screens = ScreenStack.FindScreens<T>().ToList();
			foreach (var screen in screens)
			{
				RemoveScreen(screen);
			}
		}

		/// <summary>
		/// This method pops up a recoverable error screen.
		/// </summary>
		/// <param name="ex">the exception that occureed</param>
		public void ErrorScreen(Exception ex)
		{
			var screens = new List<IScreen>(MainMenuStack());
			screens.Add(new ErrorScreen(ex));
			LoadingScreen.Load(this, true, null, string.Empty, screens.ToArray());
		}

		public IScreen FindScreen(string screenName)
		{
			return ScreenStack.FindScreen(screenName);
		}

		public List<T> FindScreens<T>() where T : IScreen
		{
			return ScreenStack.FindScreens<T>().ToList();
		}

		/// <summary>
		/// Expose an array holding all the screens. We return a copy rather
		/// than the real master list, because screens should only ever be added
		/// or removed using the AddScreen and RemoveScreen methods.
		/// </summary>
		public IScreen[] GetScreens()
		{
			return ScreenStack.GetScreens();
		}

		public void PopToScreen<T>() where T : class, IScreen
		{
			ScreenStack.PopToScreen<T>();
		}

#endregion //Public Methods
	}
}