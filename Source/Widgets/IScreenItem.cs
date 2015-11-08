using GameTimer;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is an item that can be added to a page.
	/// Can be a label, stack panel, buttons, etc.
	/// </summary>
	public interface IScreenItem : IClickable
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
		/// Where to layer the item.
		/// low numbers go in the back, higher numbers in the front
		/// </summary>
		float Layer { get; set; }

		/// <summary>
		/// Highlight or don't highlight this screen item
		/// </summary>
		bool Highlight { set; }

		/// <summary>
		/// You can set this flag to prevent drawing this button when the screen is inactive.
		/// </summary>
		bool DrawWhenInactive { set; }

		/// <summary>
		/// horizontal alignment of this item
		/// </summary>
		HorizontalAlignment Horizontal { get; set; }

		/// <summary>
		/// vertical alignment of this item
		/// </summary>
		VerticalAlignment Vertical { get; set; }

		/// <summary>
		/// Updates the screen item.
		/// </summary>
		void Update(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws the background of this item
		/// </summary>
		/// <param name="screen">the screen this dude is part of</param>
		/// <param name="gameTime">the current gametime</param>
		void DrawBackground(IScreen screen, GameClock gameTime);

		/// <summary>
		/// Draws this screen item. This can be overridden to customize the appearance.
		/// </summary>
		/// <param name="screen">the screen this dude is part of</param>
		/// <param name="gameTime">the current gametime</param>
		void Draw(IScreen screen, GameClock gameTime);
	}
}