using System;
using Microsoft.Xna.Framework;
using MouseBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be highlighted or clicked on.
	/// </summary>
	public interface IClickable
	{
		event EventHandler<ClickEventArgs> OnClick;

		/// <summary>
		/// Check if something in this container is highlighted
		/// </summary>
		/// <param name="position">the position that was clicked</param>
		/// <returns>bool: true if this item was clicked in, false if not</returns>
		bool CheckClick(ClickEventArgs click);
	}
}