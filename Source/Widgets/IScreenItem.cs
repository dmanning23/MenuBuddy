using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be added to a page.
	/// Can be a label, stack panel, buttons, etc.
	/// </summary>
	public interface IScreenItem
	{
		/// <summary>
		/// The screen realestate taken up by this item
		/// </summary>
		Rectangle Rect { get; }

		/// <summary>
		/// Set the position of this screen item
		/// </summary>
		Point Position { get; set; }

		/// <summary>
		/// Updates the screen item.
		/// </summary>
		void Update(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws the background of this item
		/// </summary>
		/// <param name="screen">the screen this dude is part of</param>
		/// <param name="gameTime">the current gametime</param>
		/// <param name="isSelected">whether or not this item is selected</param>
		void DrawBackground(IScreen screen, GameClock gameTime, bool isSelected);

		/// <summary>
		/// Draws this screen item. This can be overridden to customize the appearance.
		/// </summary>
		/// <param name="screen">the screen this dude is part of</param>
		/// <param name="gameTime">the current gametime</param>
		/// <param name="isSelected">whether or not this item is selected</param>
		void Draw(IScreen screen, GameClock gameTime, bool isSelected);
	}
}