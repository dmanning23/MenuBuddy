using System.Collections.Generic;

namespace MenuBuddy
{
	public interface IDropdown<T> : IWidget, IClickable
	{
		List<DropdownItem<T>> DropdownList
		{
			get;
		}
	}
}