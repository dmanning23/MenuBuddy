using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A screen is a single layer that has update and draw logic, and which
	/// can be combined with other layers to build up a complex menu system.
	/// For instance the main menu, the options menu, the "are you sure you
	/// want to quit" message box, and the main game itself are all implemented
	/// as screens.
	/// </summary>
	public interface IScreen
	{
		#region Properties

		/// <summary>
		/// Gets or sets the screen manager that this screen belongs to.
		/// </summary>
		ScreenManager ScreenManager { get; set; }

		/// <summary>
		/// Gets the content manager used to load assets for this screen.
		/// </summary>
		ContentManager Content { get; }

		/// <summary>
		/// Gets or sets the name of this screen.
		/// </summary>
		string ScreenName { get; set; }

		/// <summary>
		/// Gets the game clock used for timing and animations.
		/// </summary>
		GameClock Time { get; }

		/// <summary>
		/// Gets the transition controller for this screen.
		/// </summary>
		IScreenTransition Transition { get; }

		/// <summary>
		/// Gets the current transition state of this screen.
		/// </summary>
		TransitionState TransitionState { get; }

		/// <summary>
		/// Gets or sets whether screens underneath this one should transition off.
		/// </summary>
		bool CoverOtherScreens { get; set; }

		/// <summary>
		/// Gets or sets whether this screen should transition off when covered by other screens.
		/// </summary>
		bool CoveredByOtherScreens { get; set; }

		/// <summary>
		/// Gets or sets the player index that controls this screen, or null for any player.
		/// </summary>
		int? ControllingPlayer { get; set; }

		/// <summary>
		/// Gets whether this screen is currently active and can receive input.
		/// </summary>
		bool IsActive { get; }

		/// <summary>
		/// Gets whether this screen currently has focus.
		/// </summary>
		bool HasFocus { get; }

		/// <summary>
		/// Gets whether this screen is in the process of exiting.
		/// </summary>
		bool IsExiting { get; }

		/// <summary>
		/// Gets or sets the layer of this screen in the screen stack. Higher layers are drawn on top.
		/// </summary>
		int Layer { get; set; }

		/// <summary>
		/// Gets or sets the sub-layer used by ScreenStack for sorting screens within the same layer.
		/// This is managed internally and should not be modified directly.
		/// </summary>
		int SubLayer { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		Task LoadContent();

		/// <summary>
		/// Unload content for the screen.
		/// </summary>
		void UnloadContent();

		/// <summary>
		/// Allows the screen to run logic, such as updating the transition position.
		/// Unlike HandleInput, this method is called regardless of whether the screen
		/// is active, hidden, or in the middle of a transition.
		/// </summary>
		void Update(GameTime gameTime, bool otherWindowHasFocus, bool covered);

		/// <summary>
		/// Updates the transition state of this screen.
		/// </summary>
		/// <param name="screenTransition">The screen transition to update.</param>
		/// <param name="gameTime">The current game time.</param>
		/// <returns>True if the transition is complete, false otherwise.</returns>
		bool UpdateTransition(IScreenTransition screenTransition, GameClock gameTime);

		/// <summary>
		/// This is called when the screen should draw itself.
		/// </summary>
		void Draw(GameTime gameTime);

		/// <summary>
		/// Tells the screen to go away. Unlike ScreenManager.RemoveScreen, which
		/// instantly kills the screen, this method respects the transition timings
		/// and will give the screen a chance to gradually transition off.
		/// </summary>
		void ExitScreen();

		/// <summary>
		/// Check if this screen wants to handle the back button
		/// </summary>
		/// <returns>True if this screen handled the back button, false if it doesn't want it</returns>
		bool OnBackButton();

		#endregion //Methods
	}
}