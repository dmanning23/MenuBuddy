using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for containers that hold and manage a collection of <see cref="IScreenItem"/> children.
	/// </summary>
	public interface IScreenItemContainer
	{
		/// <summary>
		/// Adds a screen item to this container.
		/// </summary>
		/// <param name="item">The screen item to add.</param>
		void AddItem(IScreenItem item);

		/// <summary>
		/// Removes a screen item from this container.
		/// </summary>
		/// <param name="item">The screen item to remove.</param>
		/// <returns><c>true</c> if the item was found and removed; otherwise, <c>false</c>.</returns>
		bool RemoveItem(IScreenItem item);
	}
}