using System;
using Microsoft.Xna.Framework;
using MouseBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can respond to drag-drop operations
	/// </summary>
	public interface IDraggable
	{
		event EventHandler<DragEventArgs> OnDrag;

		/// <summary>
		/// Check if something in this container is being dragged
		/// </summary>
		/// <param name="drag">the drag operation being performed</param>
		/// <returns>bool: true if this item was clicked in, false if not</returns>
		bool CheckDrag(DragEventArgs drag);
	}
}