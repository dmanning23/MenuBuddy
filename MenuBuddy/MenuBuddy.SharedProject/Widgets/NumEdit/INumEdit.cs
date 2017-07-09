using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// The interface for a numedit control
	/// </summary>
	public interface INumEdit : IWidget, IClickable, IEditBox
	{
		event EventHandler<NumChangeEventArgs> OnNumberEdited;

		float Number { get; set; }

		string NumberText { get; set; }

		void SetNumber(float num);
	}
}
