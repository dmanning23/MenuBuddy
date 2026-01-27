using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Base interface for all items that can be added to a screen, such as labels, buttons, and layout panels.
	/// </summary>
	public interface IScreenItem : IHasContent
	{
		/// <summary>
		/// A name for this item, used for debugging purposes.
		/// </summary>
		string Name { get; set; }

		/// <summary>
		/// The bounding rectangle occupied by this item on screen.
		/// </summary>
		Rectangle Rect { get; }

		/// <summary>
		/// The position of this screen item, used as the anchor point for alignment.
		/// </summary>
		Point Position { get; set; }

		/// <summary>
		/// The draw layer of this item. Lower values are drawn further back; higher values are drawn in front.
		/// </summary>
		float Layer { get; set; }

		/// <summary>
		/// Whether to draw this item when its parent screen is inactive. Set to <c>false</c> to hide it during inactive states.
		/// </summary>
		bool DrawWhenInactive { set; }

		/// <summary>
		/// The horizontal alignment of this item relative to its position.
		/// </summary>
		HorizontalAlignment Horizontal { get; set; }

		/// <summary>
		/// The vertical alignment of this item relative to its position.
		/// </summary>
		VerticalAlignment Vertical { get; set; }

		/// <summary>
		/// Creates a deep copy of this screen item.
		/// </summary>
		/// <returns>A new <see cref="IScreenItem"/> that is a copy of this instance.</returns>
		IScreenItem DeepCopy();

		/// <summary>
		/// Updates this screen item for the current frame.
		/// </summary>
		/// <param name="screen">The screen this item belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		void Update(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws the background layer of this item (e.g., background fill and outline).
		/// </summary>
		/// <param name="screen">The screen this item belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		void DrawBackground(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws this screen item. Override to customize the appearance.
		/// </summary>
		/// <param name="screen">The screen this item belongs to.</param>
		/// <param name="gameTime">The current game time.</param>
		void Draw(IScreen screen, GameClock gameTime);
	}
}