using MenuBuddy;

namespace MenuScreenTests
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	internal class EmptyMenuStackScreen : MenuScreen, IMainMenu
	{
		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public EmptyMenuStackScreen()
			: base("Empty Menu Stack Screen")
		{
		}
	}
}