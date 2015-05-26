using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the widget object.
	/// A widget is a screen item that is displayed on the page
	/// </summary>
	public interface IWidget : IScreenItem
	{
		/// <summary>
		/// The stylesheet of this item
		/// </summary>
		StyleSheet Style { get; }

		/// <summary>
		/// horizontal alignment of this item
		/// </summary>
		HorizontalAlignment Horizontal { get; set; }

		/// <summary>
		/// vertical alignment of this item
		/// </summary>
		VerticalAlignment Vertical { get; set; }

		/// <summary>
		/// Available load content method for child classes.
		/// </summary>
		/// <param name="screen"></param>
		void LoadContent(IScreen screen);

		/// <summary>
		/// How much to resize this widget.
		/// Default is 1.0
		/// </summary>
		float Scale { get; }

		/// <summary>
		/// How many pixels worth of padding to add around this widget.
		/// </summary>
		Vector2 Padding { get; }
	}
}