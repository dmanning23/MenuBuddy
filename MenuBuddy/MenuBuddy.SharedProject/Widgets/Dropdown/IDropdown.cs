using InputHelper;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public interface IDropdown<T> : ILayout, IClickable, ITransitionable
	{
		event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		int SelectedIndex { set; }

		T SelectedItem { get; set; }

		DropdownItem<T> SelectedDropdownItem { get; set; }

		List<DropdownItem<T>> DropdownItems { get; }

		void AddDropdownItem(DropdownItem<T> dropdownItem);
	}
}