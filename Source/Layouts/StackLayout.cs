using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a list of items that are stacked up
	/// The position of a stack layout is the top center
	/// </summary>
	public class StackLayout : Layout
	{
		#region Properties

		/// <summary>
		/// How to layout the items in this stack
		/// </summary>
		public StackAlignment Alignment { get; set; }

		#endregion //Properties

		#region Methods

		public StackLayout(StackAlignment alignment = StackAlignment.Top)
		{
			Alignment = alignment;
		}

		public override void AddItem(IScreenItem item)
		{
			switch (Alignment)
			{
				case StackAlignment.Top:
				{
					AddBottom(item);
				}
				break;
				case StackAlignment.Bottom:
				{
					AddTop(item);
				}
				break;
				case StackAlignment.Left:
				{
					AddRight(item);
				}
				break;
				case StackAlignment.Right:
				{
					AddLeft(item);
				}
				break;
			}
		}

		private void AddTop(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(Position.X, rect.Top - item.Rect.Height);

			//store the new item
			Items.Add(item);
		}

		private void AddBottom(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(Position.X, rect.Bottom);

			//store the new item
			Items.Add(item);
		}

		private void AddLeft(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(rect.Left - item.Rect.Width, Position.Y);

			//store the new item
			Items.Add(item);
		}

		private void AddRight(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(rect.Right, Position.Y);

			//store the new item
			Items.Add(item);
		}

		#endregion //Methods
	}
}