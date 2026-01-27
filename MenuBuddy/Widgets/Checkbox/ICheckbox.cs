using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a checkbox button that toggles between checked and unchecked states.
	/// </summary>
    public interface ICheckbox : IButton
	{
		/// <summary>
		/// Whether this checkbox is currently checked.
		/// </summary>
		bool IsChecked { get; set; }
    }
}
