using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is a thing on the screen that can contain a collection of other things
	/// </summary>
	public interface IScreenItemContainer : IScreenItem
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

		/// <summary>
		/// Get all the widgets currently displayed in this layout
		/// </summary>
		IEnumerable<IButton> Buttons { get; }
	}
}