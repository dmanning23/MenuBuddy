using InputHelper;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public interface IDropdown<T> : ILayout, IClickable, ITransitionable, IBackgroundable
	{
		event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		int SelectedIndex { set; }

		T SelectedItem { get; set; }

		IDropdownItem<T> SelectedDropdownItem { get; set; }

		List<IDropdownItem<T>> DropdownItems { get; }

		void AddDropdownItem(IDropdownItem<T> dropdownItem);
	}
}