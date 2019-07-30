using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// The screen manager is a component which manages one or more GameScreen
	/// instances. It maintains a stack of screens, calls their Update and Draw
	/// methods at the appropriate times, and automatically routes input to the
	/// topmost active screen.
	/// </summary>
	public interface IScreenManager : IDrawable, IGameComponent, IUpdateable
	{
		#region Properties

		DefaultGame DefaultGame { get; }

		/// <summary>
		/// the input helper being used in this game
		/// </summary>
		IInputHandler Input { get; }

		/// <summary>
		/// A default SpriteBatch shared by all the screens. 
		/// This saves each screen having to bother creating their own local instance.
		/// </summary>
		SpriteBatch SpriteBatch { get; }

		/// <summary>
		/// The color to clear the backbuffer to
		/// </summary>
		/// <value>The color of the clear.</value>
		Color ClearColor { set; }

		/// <summary>
		/// The object used by screen items to help render things like button backgrounds, etc.
		/// </summary>
		DrawHelper DrawHelper { get; }

		/// <summary>
		/// Delegate method to get the stack of screens used for the main menu.
		/// </summary>
		ScreenStackDelegate MainMenuStack { get; }

		#endregion //Properties

		#region Update and Draw

		/// <summary>
		/// A simple way to start the spritebatch from a gamescreen
		/// </summary>
		void SpriteBatchBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred);

		/// <summary>
		/// A simple way to start the spritebatch from a gamescreen
		/// </summary>
		void SpriteBatchBegin(BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred);

		/// <summary>
		/// a simple way to end a spritebatch from a gamescreen
		/// </summary>
		void SpriteBatchEnd();

		#endregion //Update and Draw

		#region Public Methods

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		Task AddScreen(IScreen screen, PlayerIndex? controllingPlayer = null);

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		Task AddScreen(IScreen[] screens, PlayerIndex? controllingPlayer = null);

		/// <summary>
		/// Set the top screen
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="controllingPlayer"></param>
		Task SetTopScreen(IScreen screen, PlayerIndex? controllingPlayer);

		/// <summary>
		/// Removes a screen from the screen manager. You should normally
		/// use GameScreen.ExitScreen instead of calling this directly, so
		/// the screen can gradually transition off rather than just being
		/// instantly removed.
		/// </summary>
		void RemoveScreen(IScreen screen);

		void RemoveScreens<T>() where T:IScreen;

		/// <summary>
		/// This method pops up a recoverable error screen.
		/// </summary>
		/// <param name="ex">the exception that occureed</param>
		void ErrorScreen(Exception ex);

		/// <summary>
		/// Find a screen by name.
		/// </summary>
		/// <param name="screenName"></param>
		/// <returns></returns>
		IScreen FindScreen(string screenName);

		/// <summary>
		/// Find all the screens of a certain type
		/// </summary>
		/// <param name="screenType"></param>
		/// <returns></returns>
		List<T> FindScreens<T>() where T : IScreen;

		void BringToTop<T>() where T : IScreen;

		/// <summary>
		/// Clear the entire screenstack
		/// </summary>
		void ClearScreens();

		/// <summary>
		/// Called when the user hits the "back" button on Android or escape key on win
		/// </summary>
		/// <returns>True if any screen wants to handle the back button, false to send the app to the background</returns>
		bool OnBackButton();

		#endregion //Public Methods
	}
}