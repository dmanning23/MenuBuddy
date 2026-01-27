using InputHelper;
using System;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a dropdown widget that displays a selected item and opens a list of options when clicked.
	/// </summary>
	/// <typeparam name="T">The type of the items in the dropdown.</typeparam>
	public interface IDropdown<T> : ILayout, IClickable, ITransitionable, IBackgroundable
	{
		/// <summary>
		/// Raised when the selected item changes.
		/// </summary>
		event EventHandler<SelectionChangeEventArgs<T>> OnSelectedItemChange;

		/// <summary>
		/// Sets the selected item by its index in <see cref="DropdownItems"/>.
		/// </summary>
		int SelectedIndex { set; }

		/// <summary>
		/// The currently selected item value.
		/// </summary>
		T SelectedItem { get; set; }

		/// <summary>
		/// The currently selected dropdown item widget.
		/// </summary>
		IDropdownItem<T> SelectedDropdownItem { get; set; }

		/// <summary>
		/// The list of all available dropdown items.
		/// </summary>
		List<IDropdownItem<T>> DropdownItems { get; }

		/// <summary>
		/// Adds an item to the dropdown's list of available options.
		/// </summary>
		/// <param name="dropdownItem">The dropdown item to add.</param>
		void AddDropdownItem(IDropdownItem<T> dropdownItem);
	}
}