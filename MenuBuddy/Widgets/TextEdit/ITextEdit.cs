using InputHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a text input widget that allows the user to edit text via keyboard or dialog.
	/// </summary>
    public interface ITextEdit : IWidget, IClickable, IEditBox, ILabel
	{
		/// <summary>
		/// Raised when the user finishes editing text and the value has changed.
		/// </summary>
		event EventHandler<TextChangedEventArgs> OnTextEdited;

		/// <summary>
		/// Sets the text and fires the <see cref="OnTextEdited"/> event if the value has changed.
		/// </summary>
		/// <param name="text">The new text value.</param>
		void SetText(string text);
	}
}
