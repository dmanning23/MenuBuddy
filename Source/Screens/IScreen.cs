using HadoukInput;
using Microsoft.Xna.Framework;

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

		/// <summary>
		/// Gets or sets the name of this screen
		/// </summary>
		string ScreenName { get; set; }

		/// <summary>
		/// The default style of this screen.
		/// </summary>
		StyleSheet Style { get; }

		Transition Transition { get; }

		/// <summary>
		/// Normally when one screen is brought up over the top of another,
		/// the first screen will transition off to make room for the new one.
		/// This property indicates whether screens underneath it need to bother transitioning off.
		/// </summary>
		bool CoverOtherScreens { get; set; }

		PlayerIndex? ControllingPlayer { get; set; }

		/// <summary>
		/// Whether or not this is the active screen being displayed
		/// </summary>
		bool IsActive { get; }

		#endregion

		#region Initialization

		/// <summary>
		/// Set the style of this screen.
		/// Called before loadcontent and cascades the style from teh screenmanager
		/// </summary>
		void SetStyle(StyleSheet styles);

		/// <summary>
		/// Load graphics content for the screen.
		/// </summary>
		void LoadContent();

		/// <summary>
		/// Unload content for the screen.
		/// </summary>
		void UnloadContent();

		#endregion

		#region Update and Draw

		/// <summary>
		/// Allows the screen to run logic, such as updating the transition position.
		/// Unlike HandleInput, this method is called regardless of whether the screen
		/// is active, hidden, or in the middle of a transition.
		/// </summary>
		void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen);

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

		#endregion
	}
}