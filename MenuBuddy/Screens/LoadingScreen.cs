using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.ComponentModel;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// The loading screen coordinates transitions between the menu system and the
	/// game itself. Normally one screen will transition off at the same time as
	/// the next screen is transitioning on, but for larger transitions that can
	/// take a longer time to load their data, we want the menu system to be entirely
	/// gone before we start loading the game. This is done as follows:
	/// 
	/// - Tell all the existing screens to transition off.
	/// - Activate a loading screen, which will transition on at the same time.
	/// - The loading screen watches the state of the previous screens.
	/// - When it sees they have finished transitioning off, it activates the real
	///   next screen, which may take a long time to load its data. The loading
	///   screen will be the only thing displayed while this load is taking place.
	/// </summary>
	public class LoadingScreen : WidgetScreen
	{
		#region Properties

		/// <summary>
		/// The screens to load and activate after the loading screen is displayed.
		/// </summary>
		private IScreen[] ScreensToLoad { get; set; }

		/// <summary>
		/// Background worker thread that loads screen content off the main thread on non-desktop platforms.
		/// </summary>
		BackgroundWorker _backgroundThread;

		/// <summary>
		/// Optional sound effect resource name to play while loading begins.
		/// </summary>
		private string LoadSoundEffect { get; set; }

		/// <summary>
		/// Optional font resource name for the loading message label. Defaults to <see cref="StyleSheet.MediumFontResource"/>.
		/// </summary>
		public string Font { get; set; }

		/// <summary>
		/// The text displayed on screen while loading. Defaults to "Loading...".
		/// </summary>
		public string Message { get; set; }

#if DESKTOP
		/// <summary>
		/// Short delay before loading begins on desktop, giving the loading screen one frame to render before the main thread blocks.
		/// </summary>
		CountdownTimer timer = new CountdownTimer();
#endif

#endregion //Properties

		#region Methods

		/// <summary>
		/// Private — use the static <see cref="Load(ScreenManager, IScreen[], string)"/> overloads to create and push a loading screen.
		/// </summary>
		/// <param name="loadSoundEffect">Sound effect resource name to play at load start, or null for silence.</param>
		/// <param name="screensToLoad">The screens to add once previous screens have finished transitioning off.</param>
		private LoadingScreen(string loadSoundEffect, IScreen[] screensToLoad)
			: base("Loading")
		{
			ScreensToLoad = screensToLoad;
			LoadSoundEffect = loadSoundEffect;

			CoveredByOtherScreens = false;
			CoverOtherScreens = true;

			Transition.OnTime = 0.5f;
		}

		/// <summary>
		/// Activates the loading screen without a sound effect or controlling player.
		/// </summary>
		/// <param name="screenManager">The screen manager to push the loading screen onto.</param>
		/// <param name="screensToLoad">The screens to load and display after the loading screen exits.</param>
		/// <param name="message">The loading message to display. Defaults to "Loading...".</param>
		public static Task Load(ScreenManager screenManager,
								IScreen[] screensToLoad,
								string message = "Loading...")
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(null, screensToLoad)
			{
				Message = message
			};
			return screenManager.AddScreen(loadingScreen, null);
		}

		/// <summary>
		/// Activates the loading screen for a specific controlling player with a transition sound.
		/// </summary>
		/// <param name="screenManager">The screen manager.</param>
		/// <param name="controllingPlayer">The player index that initiated the load.</param>
		/// <param name="loadSoundEffect">Sound effect resource name to play at load start.</param>
		/// <param name="screensToLoad">The screens to load and display after the loading screen exits.</param>
		/// <param name="message">The loading message to display.</param>
		public static Task Load(ScreenManager screenManager,
								int controllingPlayer,
								string loadSoundEffect,
								IScreen[] screensToLoad,
								string message = "Loading...")
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(loadSoundEffect, screensToLoad)
			{
				Message = message
			};
			return screenManager.AddScreen(loadingScreen, controllingPlayer);
		}

		/// <summary>
		/// Activates the loading screen with a transition sound.
		/// </summary>
		/// <param name="screenManager">The screen manager.</param>
		/// <param name="loadSoundEffect">Sound effect resource name to play at load start.</param>
		/// <param name="screensToLoad">The screens to load and display after the loading screen exits.</param>
		/// <param name="message">The loading message to display.</param>
		public static Task Load(ScreenManager screenManager,
								string loadSoundEffect,
								IScreen[] screensToLoad,
								string message = "Loading...")
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(loadSoundEffect, screensToLoad)
			{
				Message = message
			};
			return screenManager.AddScreen(loadingScreen, null);
		}

		/// <summary>
		/// Activates the loading screen with a transition sound and a custom font for the message label.
		/// </summary>
		/// <param name="screenManager">The screen manager.</param>
		/// <param name="loadSoundEffect">Sound effect resource name to play at load start.</param>
		/// <param name="fontResource">Font resource name to use for the loading message.</param>
		/// <param name="screensToLoad">The screens to load and display after the loading screen exits.</param>
		/// <param name="message">The loading message to display.</param>
		public static Task Load(ScreenManager screenManager,
								string loadSoundEffect,
								string fontResource,
								IScreen[] screensToLoad,
								string message = "Loading...")
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(loadSoundEffect, screensToLoad)
			{
				Font = fontResource,
				Message = message
			};
			return screenManager.AddScreen(loadingScreen, null);
		}

		/// <summary>
		/// Builds the loading UI: a centered label with an optional hourglass image, plays the load sound if set,
		/// then kicks off content loading. On desktop, starts a short timer so the screen can render once before
		/// the main thread blocks; on other platforms, spins up a <see cref="BackgroundWorker"/> instead.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			var layout = new RelativeLayout
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Position = Resolution.TitleSafeArea.Center,
			};

			if (string.IsNullOrEmpty(Font))
			{
				Font = StyleSheet.MediumFontResource;
			}

			//create the message widget
			var width = 0f;
			var msg = new Label(Message, Content, FontSize.Medium, Font)
			{
				Highlightable = false,
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Center,
			};
			width += msg.Rect.Width;

			Texture2D hourglassTex = null;
			try
			{
				hourglassTex = Content.Load<Texture2D>(StyleSheet.LoadingScreenHourglassImageResource);
			}
			catch (Exception)
			{
				//No hourglass texture :P
			}

			if (null != hourglassTex)
			{
				//create the hourglass widget
				var hourglass = new Image(hourglassTex)
				{
					Horizontal = HorizontalAlignment.Left,
					Vertical = VerticalAlignment.Center,
					Scale = 1.5f,
					Highlightable = false,
				};
				layout.AddItem(hourglass);
				width += hourglass.Rect.Width;

				//add a little shim in between the widgets
				width += 32f;
			}

			layout.AddItem(msg);
			layout.Size = new Vector2(width, 64f);
			AddItem(layout);

			//play the "loading" sound effect
			if (!string.IsNullOrEmpty(LoadSoundEffect))
			{
				var sound = Content.Load<SoundEffect>(LoadSoundEffect);
				sound.Play();
			}

#if DESKTOP
			timer.Start(1f);
#else
			// Start up the background thread, which will update the network session and draw the animation while we are loading.
			_backgroundThread = new BackgroundWorker();
			_backgroundThread.WorkerSupportsCancellation = true;
			_backgroundThread.DoWork += new DoWorkEventHandler(BackgroundWorkerThread);
			_backgroundThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CleanUp);
			_backgroundThread.RunWorkerAsync();
#endif
		}

#if DESKTOP
		/// <summary>
		/// Desktop-only update. Waits one second after the screen becomes active before triggering
		/// the load synchronously on the main thread, ensuring the loading UI is visible first.
		/// </summary>
		/// <param name="gameTime">Snapshot of the current game timing.</param>
		/// <param name="otherScreenHasFocus">True when the application window does not have OS focus.</param>
		/// <param name="coveredByOtherScreen">True when another screen is stacked on top of this one.</param>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			timer.Update(gameTime);

			//If this is Desktop and a second has elapsed, load all the screen content
			if (IsActive && !timer.HasTimeRemaining)
			{
				BackgroundWorkerThread(this, new DoWorkEventArgs(null));
				CleanUp(this, new RunWorkerCompletedEventArgs(null, null, false));
			}
		}
#endif

		/// <summary>
		/// Draws the loading screen. Fades the background behind the loading UI,
		/// then delegates to the base class to draw the message label and hourglass.
		/// </summary>
		/// <param name="gameTime">Snapshot of the current game timing.</param>
		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatchBegin();
			FadeBackground();
			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Update and Draw

		#region Background Thread

		/// <summary>
		/// Background worker entry point. Adds <see cref="ScreensToLoad"/> to the screen manager
		/// on a separate thread so the loading UI stays responsive.
		/// </summary>
		void BackgroundWorkerThread(object sender, DoWorkEventArgs e)
		{
			ScreenManager.AddScreen(ScreensToLoad, ControllingPlayer).Wait();
		}

		/// <summary>
		/// Called when the background worker completes. Forces garbage collection, exits the loading screen,
		/// and resets the game's elapsed time so the engine doesn't try to catch up.
		/// </summary>
		void CleanUp(object sender, RunWorkerCompletedEventArgs e)
		{
			//clean up all the memory from those other screens
			GC.Collect();
			GC.WaitForPendingFinalizers();
			GC.Collect();

			ExitScreen();

			// Once the load has finished, we use ResetElapsedTime to tell
			// the  game timing mechanism that we have just finished a very
			// long frame, and that it should not try to catch up.
			ScreenManager.Game.ResetElapsedTime();
		}

		#endregion //Background Thread
	}
}