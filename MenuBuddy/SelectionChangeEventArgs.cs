using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Event arguments for when the selected item changes in a dropdown or similar selection control.
	/// </summary>
	/// <typeparam name="T">The type of the selected item.</typeparam>
	public class SelectionChangeEventArgs<T> : EventArgs
	{
		/// <summary>
		/// Gets or sets the newly selected item.
		/// </summary>
		public T SelectedItem { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="SelectionChangeEventArgs{T}"/> class.
		/// </summary>
		/// <param name="selectedItem">The newly selected item.</param>
		public SelectionChangeEventArgs(T selectedItem)
		{
			SelectedItem = selectedItem;
		}
	}
}
