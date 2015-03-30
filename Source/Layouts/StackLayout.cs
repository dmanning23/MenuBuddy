using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a list of items that are stacked up
	/// The position of a stack layout is the top center
	/// </summary>
	public class StackLayout : Layout
	{
		#region Methods

		public StackLayout()
		{
		}

		public override void AddItem(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//get the item rect
			var itemRect = item.Rect;

			//set the position of the new item
			item.Position = new Point(Position.X, rect.Bottom);

			//store the new item
			Items.Add(item);
		}

		#endregion //Methods
	}
}