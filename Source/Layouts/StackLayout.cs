using System.Collections.Generic;
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
			Sort();
		}

		private void AddBottom(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(Position.X, rect.Bottom);

			//store the new item
			Items.Add(item);
			Sort();
		}

		private void AddLeft(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(rect.Left - item.Rect.Width, Position.Y);

			//store the new item
			Items.Add(item);
			Sort();
		}

		private void AddRight(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(rect.Right, Position.Y);

			//store the new item
			Items.Add(item);
			Sort();
		}

		/// <summary>
		/// Add an item to the start position of the stack lacyout
		/// </summary>
		/// <param name="item"></param>
		public void AddItemAtBeginning(IScreenItem item)
		{
			//create a temp list to hold everything
			var tempItems = new List<IScreenItem>();

			//add the item to the new list
			tempItems.Add(item);

			//add the rest of the items
			tempItems.AddRange(Items);

			//create a new layout list
			Items = new List<IScreenItem>();

			//add all the temp items to the layout list
			foreach (var tempItem in tempItems)
			{
				Items.Add(tempItem);
			}
		}

		public override bool RemoveItem(IScreenItem item)
		{
			//Store the list of items in a temp variable
			var tempItems = Items;

			//Remove the specified item
			var removed = tempItems.Remove(item);

			if (removed)
			{
				//Create a new list
				Items = new List<IScreenItem>();

				//Add all the items back in the list
				foreach (var tempItem in tempItems)
				{
					AddItem(tempItem);
				}
			}

			return removed;
		}

		#endregion //Methods
	}
}