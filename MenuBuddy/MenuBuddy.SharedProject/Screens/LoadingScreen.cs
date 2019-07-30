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
		#region Fields

		private IScreen[] ScreensToLoad { get; set; }

		BackgroundWorker _backgroundThread;

		private string LoadSoundEffect { get; set; }

		public string Font { get; set; }

		#endregion

		#region Initialization

		/// <summary>
		/// The constructor is private: loading screens should be activated via the static Load method instead.
		/// </summary>
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
		/// Activates the loading screen.
		/// </summary>
		/// <param name="screenManager">The screenmanager.</param>
		/// <param name="loadingIsSlow">If true, the loading screen will be displayed, otherwise will just pop up screensToLoad</param>
		/// <param name="controllingPlayer">The player that loaded the screen. Just pass null!!!</param>
		/// <param name="loadSoundEffect">Play the transition sound here instead of in the screen that initiated the load.</param>
		/// <param name="screensToLoad">Params list of all the screens we want to load.</param>
		public static Task Load(ScreenManager screenManager,
								params IScreen[] screensToLoad)
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(null, screensToLoad);
			return screenManager.AddScreen(loadingScreen, null);
		}

		public static Task Load(ScreenManager screenManager,
								PlayerIndex controllingPlayer,
								string loadSoundEffect,
								params IScreen[] screensToLoad)
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(loadSoundEffect, screensToLoad);
			return screenManager.AddScreen(loadingScreen, controllingPlayer);
		}

		public static Task Load(ScreenManager screenManager,
								string loadSoundEffect,
								params IScreen[] screensToLoad)
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(loadSoundEffect, screensToLoad);
			return screenManager.AddScreen(loadingScreen, null);
		}

		public static Task Load(ScreenManager screenManager,
								string loadSoundEffect,
								string fontResource,
								params IScreen[] screensToLoad)
		{
			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(loadSoundEffect, screensToLoad)
			{
				Font = fontResource,
			};
			return screenManager.AddScreen(loadingScreen, null);
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			var layout = new RelativeLayout
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Position = Resolution.TitleSafeArea.Center,
			};

			//create the message widget
			var width = 0f;
			var msg = new PaddedLayout(0, 0, 24, 0, new Label("Loading...", Content, FontSize.Medium, Font)
			{
				Highlightable = false
			})
			{
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

			// Start up the background thread, which will update the network session and draw the animation while we are loading.
			_backgroundThread = new BackgroundWorker();
			_backgroundThread.WorkerSupportsCancellation = true;
			_backgroundThread.DoWork += new DoWorkEventHandler(BackgroundWorkerThread);
			_backgroundThread.RunWorkerCompleted += new RunWorkerCompletedEventHandler(CleanUp);
			_backgroundThread.RunWorkerAsync();
		}

		#endregion //Initialization

		#region Update and Draw

		/// <summary>
		/// Draws the loading screen.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			// The gameplay screen takes a while to load, so we display a loading
			// message while that is going on, but the menus load very quickly, and
			// it would look silly if we flashed this up for just a fraction of a
			// second while returning from the game to the menus. This parameter
			// tells us how long the loading is going to take, so we know whether
			// to bother drawing the message.
			ScreenManager.SpriteBatchBegin();
			FadeBackground();
			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Update and Draw

		#region Background Thread

		/// <summary>
		/// Worker thread draws the loading animation and updates the network
		/// session while the load is taking place.
		/// </summary>
		void BackgroundWorkerThread(object sender, DoWorkEventArgs e)
		{
			ScreenManager.AddScreen(ScreensToLoad, ControllingPlayer).Wait();
		}

		void CleanUp(object sender, RunWorkerCompletedEventArgs e)
		{
			//clean up all the memory from those other screens
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