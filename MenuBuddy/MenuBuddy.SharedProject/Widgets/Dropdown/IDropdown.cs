using InputHelper;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	public interface IDropdown<T> : IWidget, IClickable
	{
		event EventHandler<DropDownEventArgs<T>> OnSelectedItemChange;

		List<DropdownItem<T>> DropdownList
		{
			get;
		}
	}
}