using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item tyhat can be added to a page.
	/// Can be a label, stack panel, buttons, etc.
	/// </summary>
	public interface IScreenItem
	{
		/// <summary>
		/// The screen realestate taken up by this widget
		/// </summary>
		Rectangle Rect { get; }

		/// <summary>
		/// Set the position of this screen item
		/// </summary>
		Point Position { get; set; }

		/// <summary>
		/// Updates the menu entry.
		/// </summary>
		void Update(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws the backlground of this item
		/// </summary>
		void DrawBackground(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws the menu entry. This can be overridden to customize the appearance.
		/// </summary>
		void Draw(IScreen screen, GameClock gameTime);
	}
}