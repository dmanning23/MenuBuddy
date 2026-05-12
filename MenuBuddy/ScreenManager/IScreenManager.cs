using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Contract for the component that owns the screen stack and drives the entire UI lifecycle.
	/// It maintains the ordered stack of <see cref="IScreen"/> instances, calls their
	/// <c>Update</c> and <c>Draw</c> methods each frame, routes input to the topmost active
	/// screen, and handles adding, removing, and transitioning screens.
	/// </summary>
	public interface IScreenManager : IDrawable, IGameComponent, IUpdateable
	{
		#region Properties

		/// <summary>
		/// The game instance cast to <see cref="MenuBuddy.DefaultGame"/>.
		/// </summary>
		DefaultGame DefaultGame { get; }

		/// <summary>
		/// The input handler that translates raw device input into highlight, click, drag, and drop events.
		/// </summary>
		IInputHandler Input { get; }

		/// <summary>
		/// The shared <see cref="Microsoft.Xna.Framework.Graphics.SpriteBatch"/> all screens use to draw.
		/// Screens should call <see cref="SpriteBatchBegin(SpriteSortMode)"/> and <see cref="SpriteBatchEnd"/>
		/// rather than managing it directly.
		/// </summary>
		SpriteBatch SpriteBatch { get; }

		/// <summary>
		/// The color used to clear the back buffer each frame before any screens are drawn.
		/// </summary>
		Color ClearColor { set; }

		/// <summary>
		/// Utility for common drawing operations such as fading the background behind modal screens.
		/// </summary>
		DrawHelper DrawHelper { get; }

		/// <summary>
		/// Factory delegate that returns the set of screens making up the main menu.
		/// Used by <see cref="ErrorScreen(Exception)"/> to rebuild the screen stack after an error.
		/// </summary>
		ScreenStackDelegate MainMenuStack { get; }

		#endregion //Properties

		#region Update and Draw

		/// <summary>
		/// Begins the shared <see cref="SpriteBatch"/> with <see cref="BlendState.NonPremultiplied"/> and
		/// the current resolution transformation matrix.
		/// </summary>
		/// <param name="sortMode">The sprite sort mode. Defaults to <see cref="SpriteSortMode.Deferred"/>.</param>
		void SpriteBatchBegin(SpriteSortMode sortMode = SpriteSortMode.Deferred);

		/// <summary>
		/// Begins the shared <see cref="SpriteBatch"/> with a custom blend state and
		/// the current resolution transformation matrix.
		/// </summary>
		/// <param name="blendState">The blend state to use.</param>
		/// <param name="sortMode">The sprite sort mode. Defaults to <see cref="SpriteSortMode.Deferred"/>.</param>
		void SpriteBatchBegin(BlendState blendState, SpriteSortMode sortMode = SpriteSortMode.Deferred);

		/// <summary>
		/// Ends the current <see cref="SpriteBatch"/> draw operation and flushes buffered draw calls.
		/// </summary>
		void SpriteBatchEnd();

		#endregion //Update and Draw

		#region Public Methods

		/// <summary>
		/// Adds a screen to the stack. Assigns the controlling player and screen manager,
		/// loads the screen's content if the manager is already initialized, then pushes it onto the stack.
		/// </summary>
		/// <param name="screen">The screen to add.</param>
		/// <param name="controllingPlayer">The player index that owns this screen, or null to accept input from any player.</param>
		Task AddScreen(IScreen screen, int? controllingPlayer = null);

		/// <summary>
		/// Adds multiple screens to the stack in one operation. Each screen has its content loaded
		/// before the whole batch is pushed. Null entries are skipped.
		/// </summary>
		/// <param name="screens">The screens to add.</param>
		/// <param name="controllingPlayer">The player index that owns these screens, or null to accept input from any player.</param>
		Task AddScreen(IScreen[] screens, int? controllingPlayer = null);

		/// <summary>
		/// Replaces the topmost screen on the stack with the given screen.
		/// Content is loaded before the swap so there is no visible gap.
		/// </summary>
		/// <param name="screen">The screen to place at the top of the stack.</param>
		/// <param name="controllingPlayer">The player index that owns this screen, or null to accept input from any player.</param>
		Task SetTopScreen(IScreen screen, int? controllingPlayer);

		/// <summary>
		/// Immediately removes a screen from the stack and unloads its content.
		/// Prefer <see cref="IScreen.ExitScreen"/> for graceful removal with a transition animation.
		/// </summary>
		/// <param name="screen">The screen to remove.</param>
		void RemoveScreen(IScreen screen);

		/// <summary>
		/// Removes all screens of type <typeparamref name="T"/> from the stack.
		/// </summary>
		/// <typeparam name="T">The screen type to remove.</typeparam>
		void RemoveScreens<T>() where T : IScreen;

		/// <summary>
		/// Clears the entire screen stack and rebuilds it with the main menu screens plus an
		/// <see cref="MenuBuddy.ErrorScreen"/> showing the exception details.
		/// </summary>
		/// <param name="ex">The exception that was thrown.</param>
		Task ErrorScreen(Exception ex);

		/// <summary>
		/// Finds the first screen on the stack whose <see cref="IScreen.ScreenName"/> matches <paramref name="screenName"/>.
		/// Returns null if no match is found.
		/// </summary>
		/// <param name="screenName">The name to search for.</param>
		/// <returns>The matching screen, or null.</returns>
		IScreen FindScreen(string screenName);

		/// <summary>
		/// Returns all screens on the stack that are assignable to <typeparamref name="T"/>.
		/// </summary>
		/// <typeparam name="T">The screen type to search for.</typeparam>
		/// <returns>A list of matching screens, which may be empty.</returns>
		List<T> FindScreens<T>() where T : IScreen;

		/// <summary>
		/// Moves the first screen of type <typeparamref name="T"/> to the top of the stack without removing other screens.
		/// </summary>
		/// <typeparam name="T">The screen type to bring to the top.</typeparam>
		void BringToTop<T>() where T : IScreen;

		/// <summary>
		/// Signals every screen on the stack to begin its exit transition.
		/// Screens are removed gradually as their transitions complete, not instantly.
		/// </summary>
		void ClearScreens();

		/// <summary>
		/// Dispatches the back button event to the screen stack.
		/// Screens are tried from top to bottom; the first one to return true consumes the event.
		/// </summary>
		/// <returns>True if any screen handled the back button; false if it was ignored.</returns>
		bool OnBackButton();

		#endregion //Public Methods
	}
}
