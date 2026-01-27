using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Event arguments for when the selected item changes in a dropdown list
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public class SelectionChangeEventArgs<T> : EventArgs
    {
		public T SelectedItem { get; set; }

		public SelectionChangeEventArgs(T selectedItem)
		{
			SelectedItem = selectedItem;
		}
    }
}
