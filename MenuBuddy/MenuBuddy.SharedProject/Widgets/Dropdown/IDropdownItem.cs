using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	public interface IDropdownItem<T> : IButton
	{
		/// <summary>
		/// The item this dropdownitem is representing in the gui
		/// </summary>
		T Item
		{
			get; set;
		}
	}
}
