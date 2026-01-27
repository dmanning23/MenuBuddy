using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for a text display widget that can render styled text on screen.
	/// </summary>
	public interface ILabel : IWidget, IClickable
	{
		/// <summary>
		/// The text content displayed by this label.
		/// </summary>
		string Text { get; set; }

		/// <summary>
		/// The font used to render this label's text.
		/// </summary>
		IFontBuddy Font { get; set; }

		/// <summary>
		/// The font size category (large, medium, or small) for this label.
		/// </summary>
		FontSize FontSize { get; }

		/// <summary>
		/// An optional override color for the text shadow. If <c>null</c>, the style sheet default is used.
		/// </summary>
		Color? ShadowColor { get; set; }

		/// <summary>
		/// An optional override color for the text. If <c>null</c>, the style sheet default is used.
		/// </summary>
		Color? TextColor { get; set; }

		/// <summary>
		/// Whether the text should be masked with asterisks, as for password input.
		/// </summary>
		bool IsPassword { get; set; }

		/// <summary>
		/// Scales the label to fit within the specified width.
		/// </summary>
		/// <param name="rowWidth">The maximum width in pixels to fit within.</param>
		void ScaleToFit(int rowWidth);

		/// <summary>
		/// Shrinks the label's scale only if the text exceeds the specified width.
		/// </summary>
		/// <param name="rowWidth">The maximum width in pixels to fit within.</param>
		void ShrinkToFit(int rowWidth);
	}
}