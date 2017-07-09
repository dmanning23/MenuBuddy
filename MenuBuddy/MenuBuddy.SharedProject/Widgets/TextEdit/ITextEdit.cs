using InputHelper;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	/// <summary>
	/// The interface for a textedit control
	/// </summary>
    public interface ITextEdit : IWidget, IClickable, IEditBox
	{
		event EventHandler<TextChangedEventArgs> OnTextEdited;

		string Text { get; set; }

		void SetText(string text);
	}
}
