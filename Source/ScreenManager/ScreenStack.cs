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
	/// This class updates and draws a stack of screens
	/// </summary>
	public class ScreenStack
	{
		#region Properties

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
		public IScreen TopScreen { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructs a new screen manager component.
		/// </summary>
		public ScreenStack()
		{
			ScreensToUpdate = new List<IScreen>();
			Screens = new List<IScreen>();
		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		public void LoadContent()
		{
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
			

			//load the screen content
			screen.LoadContent();
		}

		/// <summary>
		/// Unload your graphics content.
		/// </summary>
		public void UnloadContent()
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

		public void Update(GameTime gameTime, IInputHelper input, bool otherWindowHasFocus)
		{
			//Make a copy of the master screen list, to avoid confusion if the process of updating one screen adds or removes others.
			ScreensToUpdate.Clear();

			//Update the top screen separate from all other screens.
			if (null != TopScreen)
			{
				TopScreen.Update(gameTime, false, false);
				input.HandleInput(TopScreen);
			}

			for (int i = 0; i < Screens.Count; i++)
			{
				ScreensToUpdate.Add(Screens[i]);
			}

			bool coveredByOtherScreen = false;

			// Loop as long as there are screens waiting to be updated.
			while (ScreensToUpdate.Count > 0)
			{
				// Pop the topmost screen off the waiting list.
				var screen = ScreensToUpdate[ScreensToUpdate.Count - 1];
				ScreensToUpdate.RemoveAt(ScreensToUpdate.Count - 1);

				// Update the screen.
				screen.Update(gameTime, otherWindowHasFocus, coveredByOtherScreen);

				//If the screen is active, let it check the input
				if (screen.IsActive)
				{
					input.HandleInput(screen);

					//If this is a covering screen, let other screens know they are covered.
					if (screen.CoverOtherScreens)
					{
						coveredByOtherScreen = true;
					}
				}
			}
		}

		/// <summary>
		/// Tells each screen to draw itself.
		/// </summary>
		public void Draw(GameTime gameTime)
		{
			for (int i = 0; i < Screens.Count; i++)
			{
				var screen = Screens[i];

				//don't add hidden screens
				if (screen.TransitionState == TransitionState.Hidden)
				{
					continue;
				}

				screen.Draw(gameTime);
			}

			//draw the top screen
			if (null != TopScreen)
			{
				TopScreen.Draw(gameTime);
			}
		}

		/// <summary>
		/// Removes a screen from the screen stack
		/// </summary>
		public virtual void RemoveScreen(IScreen screen)
		{
			Screens.Remove(screen);
			ScreensToUpdate.Remove(screen);
		}

		#endregion //Methods
	}
}