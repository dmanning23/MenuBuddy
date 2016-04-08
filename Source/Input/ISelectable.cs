using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be highlighted or clicked on.
	/// It is only used for message boxes
	/// </summary>
	public interface ISelectable
	{
		event EventHandler<SelectedEventArgs> OnSelect;

		void Selected(object obj, PlayerIndex player);
	}
}