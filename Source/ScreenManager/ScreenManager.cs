using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;
using MouseBuddy;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using TouchScreenBuddy;

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

		public ScreenStack ScreenStack
		{
			get; set;
		}

		/// <summary>
		/// Flag for whether or not this screen manager has been initialized
		/// </summary>
		private bool Initialized { get; set; }

		public IInputHelper Input { get; private set; }

		public SpriteBatch SpriteBatch { get; private set; }

		public Color ClearColor { get; set; }

		public DrawHelper DrawHelper { get; private set; }

		public ScreenStackDelegate MainMenuStack { get; set; }

#if DEBUG
		private IMouseManager MouseManager { get; set; }
		private ITouchManager TouchManager { get; set; }
#endif

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
			Input = game.Services.GetService(typeof(IInputHelper)) as IInputHelper;
			Debug.Assert(null != Input);

			game.Components.Add(this);
			game.Services.AddService(typeof(IScreenManager), this);

#if DEBUG
			MouseManager = game.Services.GetService(typeof(IMouseManager)) as IMouseManager;
			TouchManager = game.Services.GetService(typeof(ITouchManager)) as ITouchManager;
#endif

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
		}

		#endregion //Initialization

		#region Update and Draw

		public override void Update(GameTime gameTime)
		{
			//try
			//{

			ScreenStack.Update(gameTime, Input, !Game.IsActive);

			//}
			//catch (Exception ex)
			//{
			//	ErrorScreen(ex);
			//}
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
			ScreenStack.Draw(gameTime);

#if !DEBUG
			}
			catch (Exception ex)
			{
				ErrorScreen(ex);
			}
#endif

#if DEBUG
			//draw a circle around the mouse cursor
			if (null != MouseManager)
			{
				var mouse = Mouse.GetState();
				var mousePos = new Vector2(mouse.X, mouse.Y);

				SpriteBatch.Begin();

				DrawHelper.Prim.NumCircleSegments = 4;
				DrawHelper.Prim.Circle(mousePos, 6.0f, Color.Red);

				SpriteBatch.End();
			}

			//draw a circle around each touch point
			if (null != TouchManager)
			{
				SpriteBatch.Begin();

				//go though the points that are being touched
				TouchCollection touchCollection = TouchPanel.GetState();
				foreach (var touch in touchCollection)
				{
					if ((touch.State == TouchLocationState.Pressed) || (touch.State == TouchLocationState.Moved))
					{
						DrawHelper.Prim.Circle(touch.Position, 40.0f, new Color(1.0f, 1.0f, 1.0f, 0.25f));
					}
				}

				SpriteBatch.End();
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
					screen.LoadContent();
				}

				ScreenStack.Screens.Add(screen);
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
#if !DEBUG
			try
			{
#endif
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
#if !DEBUG
			}
			catch (Exception ex)
			{
				ErrorScreen(ex);
			}
#endif

			ScreenStack.Screens.AddRange(screens);
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
			var widgetScreens = ScreenStack.Screens.OfType<WidgetScreen>();
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
			var screens = new List<IScreen>(MainMenuStack());
			screens.Add(new ErrorScreen(ex));
			LoadingScreen.Load(this, true, null, screens.ToArray());
		}

		public IScreen FindScreen(string screenName)
		{
			return ScreenStack.Screens.Find(m => m.ScreenName == screenName);
		}

		/// <summary>
		/// Expose an array holding all the screens. We return a copy rather
		/// than the real master list, because screens should only ever be added
		/// or removed using the AddScreen and RemoveScreen methods.
		/// </summary>
		public IScreen[] GetScreens()
		{
			return ScreenStack.Screens.ToArray();
		}

		#endregion //Public Methods
	}
}