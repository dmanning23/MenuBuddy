using System;

namespace MenuBuddy
{
	/// <summary>
	/// Event arguments for text change events, containing the updated text value.
	/// </summary>
	public class TextChangedEventArgs : EventArgs
	{
		/// <summary>
		/// The updated text value.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Initializes a new <see cref="TextChangedEventArgs"/> with the specified text.
		/// </summary>
		/// <param name="text">The updated text value.</param>
		public TextChangedEventArgs(string text)
		{
			Text = text;
		}
	}
}
