using MenuBuddy;

namespace MenuScreenTests
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	internal class EmptyMenuScreen : MenuScreen, IMainMenu
	{
		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public EmptyMenuScreen()
			: base("Empty Menu Screen")
		{
		}
	}
}