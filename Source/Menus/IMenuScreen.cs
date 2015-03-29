using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a stack of menu options. 
	/// The user can move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public interface IMenuScreen : IScreen
	{
		/// <summary>
		/// Select the previous menu entry
		/// </summary>
		void MenuUp();

		/// <summary>
		/// Select the next menu entry
		/// </summary>
		void MenuDown();

		/// <summary>
		/// User hit left on the current entry
		/// </summary>
		void MenuLeft();

		/// <summary>
		/// User hit right on the current entry
		/// </summary>
		void MenuRight();

		/// <summary>
		/// User hit the "menu select" button.
		/// </summary>
		/// <param name="playerIndex"></param>
		void OnSelect(PlayerIndex? playerIndex);

		/// <summary>
		/// Handler for when the user has cancelled the menu.
		/// </summary>
		void OnCancel(PlayerIndex? playerIndex);
	}
}