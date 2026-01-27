using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a numeric input widget that allows the user to edit a number via a numpad.
	/// </summary>
	public interface INumEdit : IWidget, IClickable, IEditBox
	{
		/// <summary>
		/// Raised when the user finishes editing and the number value has changed.
		/// </summary>
		event EventHandler<NumChangeEventArgs> OnNumberEdited;

		/// <summary>
		/// The current numeric value.
		/// </summary>
		float Number { get; set; }

		/// <summary>
		/// The text representation of the current number, used during editing (may contain partial input like a trailing decimal).
		/// </summary>
		string NumberText { get; set; }

		/// <summary>
		/// Sets the number and fires the <see cref="OnNumberEdited"/> event if the value has changed and is within bounds.
		/// </summary>
		/// <param name="num">The new number value.</param>
		void SetNumber(float num);
	}
}
