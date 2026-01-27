using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
    public interface ICheckbox : IButton
	{
		bool IsChecked { get; set; }
    }
}
