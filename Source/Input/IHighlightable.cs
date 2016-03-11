using System;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be highlighted.
	/// </summary>
	public interface IHighlightable
	{
		/// <summary>
		/// Highlight or don't highlight this screen item
		/// </summary>
		bool Highlight { set; }

		event EventHandler<HighlightEventArgs> OnHighlight;

		/// <summary>
		/// Check if an item in this container should be highlighted
		/// </summary>
		/// <param name="position">the location to check</param>
		bool CheckHighlight(HighlightEventArgs highlight);
	}
}