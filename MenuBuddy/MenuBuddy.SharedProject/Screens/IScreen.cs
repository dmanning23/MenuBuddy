using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;

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
		/// Gets the manager that this screen belongs to.
		/// </summary>
		ScreenManager ScreenManager { get; set; }

		ContentManager Content { get; }

		/// <summary>
		/// Gets or sets the name of this screen
		/// </summary>
		string ScreenName { get; set; }

		GameClock Time { get; }

		IScreenTransition Transition { get; }

		TransitionState TransitionState { get; }

		/// <summary>
		/// Whether or not screens underneath this one should tranisition off
		/// </summary>
		bool CoverOtherScreens { get; set; }

		/// <summary>
		/// Whether or not this screen should transition off when covered by other screens
		/// </summary>
		bool CoveredByOtherScreens { get; set; }

		PlayerIndex? ControllingPlayer { get; set; }

		/// <summary>
		/// Whether or not this is the active screen being displayed
		/// </summary>
		bool IsActive { get; }

		bool HasFocus { get; }

		bool IsExiting { get; }

		/// <summary>
		/// Set the layer of a screen to change it's location in the ScreenStack
		/// </summary>
		int Layer { get; set; }

		/// <summary>
		/// Used by the ScreenStack to sort screens. Don't touch!
		/// </summary>
		int SubLayer { get; set; }

		#endregion

		#region Methods

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		void LoadContent();

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