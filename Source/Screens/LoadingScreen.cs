using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Threading;

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
	public class LoadingScreen : Screen
	{
		#region Fields

		private const string Message = " Loading...";

		private bool LoadingIsSlow { get; set; }

		private IScreen[] ScreensToLoad { get; set; }

		private bool OtherScreensAreGone { get; set; }

		/// <summary>
		/// Gets or sets the hour glass texture we gonna
		/// </summary>
		/// <value>The hour glass.</value>
		private Texture2D HourGlass { get; set; }

		Thread _backgroundThread;

		#endregion

		#region Initialization

		/// <summary>
		/// The constructor is private: loading screens should
		/// be activated via the static Load method instead.
		/// </summary>
		private LoadingScreen(ScreenManager screenManager, bool loadingIsSlow, IScreen[] screensToLoad)
		{
			LoadingIsSlow = loadingIsSlow;
			ScreensToLoad = screensToLoad;

			Transition.OnTime = TimeSpan.FromSeconds(0.5);

			if (loadingIsSlow)
			{
				//load the hourglass
				HourGlass = screenManager.Game.Content.Load<Texture2D>("hourglass");
			}
		}

		/// <summary>
		/// Activates the loading screen.
		/// </summary>
		public static void Load(ScreenManager screenManager,
								bool loadingIsSlow,
								PlayerIndex? controllingPlayer,
								params IScreen[] screensToLoad)
		{
			// Tell all the current screens to transition off.
			foreach (var screen in screenManager.GetScreens())
			{
				screen.ExitScreen();
			}

			// Create and activate the loading screen.
			var loadingScreen = new LoadingScreen(screenManager,
												  loadingIsSlow,
												  screensToLoad);

			screenManager.AddScreen(loadingScreen, controllingPlayer);
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Updates the loading screen.
		/// </summary>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			// If all the previous screens have finished transitioning off, it is time to actually perform the load.
			if (OtherScreensAreGone)
			{
				// Start up the background thread, which will update the network
				// session and draw the animation while we are loading.
				if (_backgroundThread == null)
				{
					_backgroundThread = new Thread(BackgroundWorkerThread);
					_backgroundThread.Start();
				}

				if (!_backgroundThread.IsAlive)
				{
					//clean up all the memory from those other screens
					GC.Collect();

					ScreenManager.RemoveScreen(this);

					// Once the load has finished, we use ResetElapsedTime to tell
					// the  game timing mechanism that we have just finished a very
					// long frame, and that it should not try to catch up.
					ScreenManager.Game.ResetElapsedTime();
				}
			}
		}

		/// <summary>
		/// Draws the loading screen.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			// If we are the only active screen, that means all the previous screens
			// must have finished transitioning off. We check for this in the Draw
			// method, rather than in Update, because it isn't enough just for the
			// screens to be gone: in order for the transition to look good we must
			// have actually drawn a frame without them before we perform the load.
			if ((Transition.State == TransitionState.Active) &&
				(ScreenManager.GetScreens().Length == 1))
			{
				OtherScreensAreGone = true;
			}

			// The gameplay screen takes a while to load, so we display a loading
			// message while that is going on, but the menus load very quickly, and
			// it would look silly if we flashed this up for just a fraction of a
			// second while returning from the game to the menus. This parameter
			// tells us how long the loading is going to take, so we know whether
			// to bother drawing the message.
			if (LoadingIsSlow)
			{
				//Get the text position
				var textPosition = new Vector2(
					Resolution.TitleSafeArea.Center.X + 64,
					Resolution.TitleSafeArea.Center.Y);
				Vector2 fontSize = Style.SelectedFont.Font.MeasureString(Message);
				textPosition.Y -= fontSize.Y;

				ScreenManager.SpriteBatchBegin();

				//Draw on a black backgrounf
				FadeBackground();

				// Draw the text.
				Color colorFore = Transition.AlphaColor(Color.White);
				Color colorBack = Transition.AlphaColor(Color.Black);
				Style.SelectedFont.Write(Message, textPosition, Justify.Center, 1.0f, colorFore, ScreenManager.SpriteBatch, Time);

				//get the size of the text
				Vector2 textSize = Style.SelectedFont.Font.MeasureString(Message);

				//get the hourglass position
				var hourglassPos = new Rectangle();
				hourglassPos.Width = 64;
				hourglassPos.Height = 64;
				hourglassPos.X = (int)(textPosition.X - (textSize.X * 0.5f) - HourGlass.Width);
				hourglassPos.Y = (int)(textPosition.Y + 36);

				//draw the hourglass
				ScreenManager.SpriteBatch.Draw(HourGlass, hourglassPos, colorFore);

				ScreenManager.SpriteBatchEnd();
			}
		}

		#endregion

		#region Background Thread

		/// <summary>
		/// Worker thread draws the loading animation and updates the network
		/// session while the load is taking place.
		/// </summary>
		void BackgroundWorkerThread()
		{
			foreach (var screen in ScreensToLoad)
			{
				if (screen != null)
				{
					ScreenManager.AddScreen(screen, ControllingPlayer);
				}
			}
		}

		#endregion
	}
}