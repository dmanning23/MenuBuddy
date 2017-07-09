using System;

namespace MenuBuddy
{
	/// <summary>
	/// The event argumnets that get fired off when the text is manually edited in a TextEdit using the keyboard
	/// </summary>
	public class TextChangedEventArgs : EventArgs
	{
		public string Text { get; set; }

		public TextChangedEventArgs(string text)
		{
			Text = text;
		}
	}
}
