using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a thing on the screen that can contain a collection of other things
	/// </summary>
	public interface IScreenItemContainer
	{
		/// <summary>
		/// Add a screen item to this screen.
		/// </summary>
		/// <param name="item"></param>
		void AddItem(IScreenItem item);

		/// <summary>
		/// Remove an item from this screen.
		/// </summary>
		/// <param name="item">The item to remove</param>
		/// <returns>true if the item was removed</returns>
		bool RemoveItem(IScreenItem item);
	}
}