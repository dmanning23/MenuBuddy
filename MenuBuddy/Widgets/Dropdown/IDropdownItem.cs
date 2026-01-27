using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a single selectable item in a dropdown list.
	/// </summary>
	/// <typeparam name="T">The type of the data item this widget represents.</typeparam>
	public interface IDropdownItem<T> : IButton
	{
		/// <summary>
		/// The data item that this dropdown entry represents.
		/// </summary>
		T Item
		{
			get; set;
		}
	}
}
