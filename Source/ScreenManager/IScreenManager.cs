using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

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

		/// <summary>
		/// the input helper being used in this game
		/// </summary>
		IInputHelper Input { get; }

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
		/// This is a set of the styles that will be used for this game.
		/// </summary>
		IDefaultStyles Styles { get; }

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
		void AddScreen(IScreen screen, PlayerIndex? controllingPlayer = null);

		/// <summary>
		/// Adds a new screen to the screen manager.
		/// </summary>
		void AddScreen(IScreen[] screens, PlayerIndex? controllingPlayer = null);

		/// <summary>
		/// Set the top screen
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="controllingPlayer"></param>
		void SetTopScreen(IScreen screen, PlayerIndex? controllingPlayer);

		/// <summary>
		/// Removes a screen from the screen manager. You should normally
		/// use GameScreen.ExitScreen instead of calling this directly, so
		/// the screen can gradually transition off rather than just being
		/// instantly removed.
		/// </summary>
		void RemoveScreen(IScreen screen);

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

		#endregion //Public Methods
	}
}