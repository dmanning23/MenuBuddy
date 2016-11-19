using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Event arguments for when the selected item changes in a dropdown list
	/// </summary>
	/// <typeparam name="T"></typeparam>
    public class DropDownEventArgs<T> : EventArgs
    {
		public T SelectedItem { get; set; }

		public DropDownEventArgs(T selectedItem)
		{
			SelectedItem = selectedItem;
		}
    }
}
