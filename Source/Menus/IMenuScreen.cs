using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a stack of menu options. 
	/// The user can move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public interface IMenuScreen : IWidgetScreen
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
    }
}