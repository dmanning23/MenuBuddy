using InputHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// The interface for a numedit control
	/// </summary>
    public interface INumEdit : IWidget, IClickable
	{
		event EventHandler<NumChangeEventArgs> OnNumberEdited;

		float Number { get; set; }

		string NumberText { get; set; }

		void SetNumber(float num);
	}
}
