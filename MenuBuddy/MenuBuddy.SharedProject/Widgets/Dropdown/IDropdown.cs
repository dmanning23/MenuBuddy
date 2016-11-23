using InputHelper;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public interface IDropdown<T> : IWidget, IClickable
	{
		event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		T SelectedItem { get; set; }
		
		List<DropdownItem<T>> DropdownList
		{
			get;
		}
	}
}