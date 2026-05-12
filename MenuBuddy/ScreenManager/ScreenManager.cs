using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A <see cref="DrawableGameComponent"/> that owns the screen stack and drives the
	/// entire UI lifecycle. It maintains the ordered stack of <see cref="IScreen"/> instances,
	/// calls their <c>Update</c> and <c>Draw</c> methods each frame, routes input to the
	/// topmost active screen, and handles adding, removing, and transitioning screens.
	/// </summary>
	public class ScreenManager : DrawableGameComponent, IScreenManager
	{
		#region Properties

		/// <summary>
		/// Convenience cast of <see cref="GameComponent.Game"/> to <see cref="MenuBuddy.DefaultGame"/>.
		/// </summary>
		public DefaultGame DefaultGame => Game as DefaultGame;

		/// <summary>
		/// The ordered stack of screens managed by this component.
		/// Higher-layer screens are drawn on top and receive input first.
		/// </summary>
		public ScreenStack ScreenStack
		{
			get; set;
		}

		/// <summary>
		/// True once <see cref="Initialize"/> has been called by the MonoGame framework,
		/// indicating the graphics device is ready and screens can load content.
		/// </summary>
		private bool Initialized { get; set; }

		/// <summary>
		/// The input handler that translates raw device input into highlight, click, drag, and drop events.
		/// Must be registered as a game service before the <see cref="ScreenManager"/> is constructed.
		/// </summary>
		public IInputHandler Input { get; private set; }

		/// <summary>
		/// The shared <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> all screens use to draw.
		/// Screens should call <see cref="SpriteBatchBegin(SpriteSortMode)"/> and <see cref="SpriteBatchEnd"/> rather than managing it directly.
		/// </summary>
		public SpriteBatch SpriteBatch { get; private set; }

		/// <summary>
		/// The color used to clear the back buffer each frame before any screens are drawn.
		/// Defaults to <see cref="StyleSheet.ClearColor"/>.
		/// </summary>
		public Color ClearColor { get; set; }

		/// <summary>
		/// Utility for common drawing operations such as fading the background behind modal screens.
		/// </summary>
		public DrawHelper DrawHelper { get; private set; }

		/// <summary>
		/// Factory delegate that returns the set of screens making up the main menu.
		/// Used by <see cref="ErrorScreen(Exception)"/> to rebuild the screen stack after an error.
		/// </summary>
		public ScreenStackDelegate MainMenuStack { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new screen manager component, registers it as a game service, and validates
		/// that an <see cref="IInputHandler"/> service is already present.
		/// </summary>
		/// <param name="game">The MonoGame <see cref="Game"/> instance that owns this component.</param>
		/// <param name="mainMenuStack">
		/// Factory delegate used to rebuild the main menu screen stack, e.g. when recovering from an error.
		/// </param>
		/// <exception cref="Exception">Thrown if <see cref="IInputHandler"/> has not been registered as a game service.</exception>
		public ScreenManager(Game game, ScreenStackDelegate mainMenuStack)
			: base(game)
		{
			Initialized = false;
			MainMenuStack = mainMenuStack;

			ScreenStack = new ScreenStack();

			ClearColor = StyleSheet.ClearColor;

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
		/// Called once by the MonoGame framework after the graphics device is created.
		/// Sets the <see cref="Initialized"/> flag so subsequent <see cref="AddScreen(IScreen, int?)"/> calls
		/// will immediately trigger content loading.
		/// </summary>
		public override void Initialize()
		{
			base.Initialize();

			//allow the screen manager to load content from now on
			Initialized = true;
		}

		/// <summary>
		/// Creates the shared <see cref="SpriteBatch"/> and <see cref="DrawHelper"/>,
		/// then kicks off async content loading for any screens already on the stack.
		/// </summary>
		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			DrawHelper = new DrawHelper(this);

			Task.Run(() => ScreenStack.LoadContent()).ConfigureAwait(false);
		}

		/// <summary>
		/// Unloads and disposes all screens on the stack and releases the <see cref="DrawHelper"/>.
		/// </summary>
		protected override void UnloadContent()
		{
			ScreenStack?.UnloadContent();
			ScreenStack = null;

			DrawHelper?.Dispose();
			DrawHelper = null;
		}

		#endregion //Initialization

		#region Update and Draw

		/// <summary>
		/// Updates all screens on the stack, passing current input state and whether the game window has OS focus.
		/// </summary>
		/// <param name="gameTime">Snapshot of the current game timing.</param>
		public override void Update(GameTime gameTime)
		{
			ScreenStack.Update(gameTime, Input, !Game.IsActive);
		}

		/// <summary>
		/// Tells each screen to draw itself.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			ScreenStack.Draw(gameTime);
		}

		/// <summary>
		/// Begins the shared <see cref="SpriteBatch"/> with <see cref="BlendState.NonPremultiplied"/> and
		/// the current resolution transformation matrix. Screens should call this rather than managing
		/// the sprite batch directly.
		/// </summary>
		/// <param name="sortMode">The sprite sort mode. Defaults to <see cref="SpriteSortMode.Deferred"/>.</param>
		public void SpriteBatchBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred)
		{
			SpriteBatch.Begin(sortMode,
							  BlendState.NonPremultiplied,
							  null, null, null, null,
							  Resolution.TransformationMatrix());
		}

		/// <summary>
		/// Begins the shared <see cref="SpriteBatch"/> with a custom blend state and
		/// the current resolution transformation matrix.
		/// </summary>
		/// <param name="blendState">The blend state to use.</param>
		/// <param name="sortMode">The sprite sort mode. Defaults to <see cref="SpriteSortMode.Deferred"/>.</param>
		public void SpriteBatchBegin(BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred)
		{
			SpriteBatch.Begin(sortMode,
							  blendState,
							  null, null, null, null,
							  Resolution.TransformationMatrix());
		}

		/// <summary>
		/// Ends the current <see cref="SpriteBatch"/> draw operation and flushes buffered draw calls.
		/// </summary>
		public void SpriteBatchEnd()
		{
			SpriteBatch.End();
		}

		#endregion //Update and Draw

		#region Public Methods

		/// <summary>
		/// Adds a screen to the stack. Assigns the controlling player and screen manager,
		/// loads the screen's content if the manager is already initialized, then pushes it onto the stack.
		/// </summary>
		/// <param name="screen">The screen to add.</param>
		/// <param name="controllingPlayer">The player index that owns this screen, or null to accept input from any player.</param>
		public virtual async Task AddScreen(IScreen screen, int? controllingPlayer = null)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;

			// If we have a graphics device, tell the screen to load content.
			if (Initialized)
			{
				await screen.LoadContent();
			}

			ScreenStack.AddScreen(screen);
		}

		/// <summary>
		/// Adds multiple screens to the stack in one operation. Each screen has its content loaded
		/// before the whole batch is pushed, so all screens are ready when they first receive input.
		/// </summary>
		/// <param name="screens">The screens to add. Null entries are skipped.</param>
		/// <param name="controllingPlayer">The player index that owns these screens, or null to accept input from any player.</param>
		public virtual async Task AddScreen(IScreen[] screens, int? controllingPlayer = null)
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
						await screen.LoadContent();
					}
				}
			}

			ScreenStack.AddScreen(screens);
		}

		/// <summary>
		/// Replaces the topmost screen on the stack with the given screen.
		/// Content is loaded before the swap so there is no visible gap.
		/// </summary>
		/// <param name="screen">The screen to place at the top of the stack.</param>
		/// <param name="controllingPlayer">The player index that owns this screen, or null to accept input from any player.</param>
		public virtual async Task SetTopScreen(IScreen screen, int? controllingPlayer)
		{
			screen.ControllingPlayer = controllingPlayer;
			screen.ScreenManager = this;

			// If we have a graphics device, tell the screen to load content.
			if (Initialized)
			{
				await screen.LoadContent();
			}

			ScreenStack.TopScreen = screen;
		}

		/// <summary>
		/// Immediately removes a screen from the stack and unloads its content.
		/// Prefer <see cref="IScreen.ExitScreen"/> for graceful removal with a transition animation.
		/// Also resets the attract-mode timer on all remaining widget screens.
		/// </summary>
		/// <param name="screen">The screen to remove.</param>
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

		/// <summary>
		/// Removes all screens of type <typeparamref name="T"/> from the stack.
		/// </summary>
		/// <typeparam name="T">The screen type to remove.</typeparam>
		public void RemoveScreens<T>() where T : IScreen
		{
			var screens = ScreenStack.FindScreens<T>().ToList();
			foreach (var screen in screens)
			{
				RemoveScreen(screen);
			}
		}

		/// <summary>
		/// Clears the entire screen stack and rebuilds it with the main menu screens plus an
		/// <see cref="MenuBuddy.ErrorScreen"/> showing the exception details. This lets the
		/// player recover without restarting the game.
		/// </summary>
		/// <param name="ex">The exception that was thrown.</param>
		public async Task ErrorScreen(Exception ex)
		{
			var screens = new List<IScreen>(MainMenuStack());
			screens.Add(new ErrorScreen(ex));
			ClearScreens();
			await LoadingScreen.Load(this, null, string.Empty, screens.ToArray());
		}

		/// <summary>
		/// Finds the first screen on the stack whose <see cref="IScreen.ScreenName"/> matches <paramref name="screenName"/>.
		/// Returns null if no match is found.
		/// </summary>
		/// <param name="screenName">The name to search for.</param>
		public IScreen FindScreen(string screenName)
		{
			return ScreenStack.FindScreen(screenName);
		}

		/// <summary>
		/// Returns all screens on the stack that are assignable to <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The screen type to search for.</typeparam>
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

		/// <summary>
		/// Removes all screens above the first screen of type <typeparamref name="T"/>, making it the topmost screen.
		/// </summary>
		/// <typeparam name="T">The screen type to pop back to.</typeparam>
		public void PopToScreen<T>() where T : class, IScreen
		{
			ScreenStack.PopToScreen<T>();
		}

		/// <summary>
		/// Moves the first screen of type <typeparamref name="T"/> to the top of the stack without removing other screens.
		/// </summary>
		/// <typeparam name="T">The screen type to bring to the top.</typeparam>
		public void BringToTop<T>() where T : IScreen
		{
			ScreenStack.BringToTop<T>();
		}

		/// <summary>
		/// Signals every screen on the stack to begin its exit transition.
		/// Screens are removed gradually as their transitions complete, not instantly.
		/// </summary>
		public void ClearScreens()
		{
			// Tell all the current screens to transition off.
			foreach (var screen in GetScreens())
			{
				screen.ExitScreen();
			}
		}

		/// <summary>
		/// Dispatches the back button event to the screen stack.
		/// Screens are tried from top to bottom; the first one to return true consumes the event.
		/// </summary>
		/// <returns>True if any screen handled the back button; false if it was ignored.</returns>
		public bool OnBackButton()
		{
			return ScreenStack.OnBackButton();
		}

		#endregion //Public Methods
	}
}