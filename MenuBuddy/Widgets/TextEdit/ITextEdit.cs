using InputHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// The interface for a textedit control
	/// </summary>
    public interface ITextEdit : IWidget, IClickable, IEditBox, ILabel
	{
		event EventHandler<TextChangedEventArgs> OnTextEdited;

		void SetText(string text);
	}
}
