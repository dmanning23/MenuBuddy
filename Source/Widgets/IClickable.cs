using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be highlighted or clicked on.
	/// </summary>
	public interface IClickable
	{
		/// <summary>
		/// Check if an item in this container should be highlighted
		/// </summary>
		/// <param name="position">the location to check</param>
		void CheckHighlight(Vector2 position);

		/// <summary>
		/// Check if something in this container is highlighted
		/// </summary>
		/// <param name="position">the position that was clicked</param>
		/// <returns>bool: true if this item was clicked in, false if not</returns>
		bool CheckClick(Vector2 position);
	}
}