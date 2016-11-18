using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a list of items that are stacked up
	/// The position of a stack layout is the top center
	/// </summary>
	public class StackLayout : Layout, IStackLayout
	{
		#region Fields

		private StackAlignment _alignment;

		#endregion //Fields

		#region Properties

		/// <summary>
		/// How to layout the items in this stack
		/// </summary>
		public StackAlignment Alignment
		{
			get
			{
				return _alignment;
			}
			set
			{
				_alignment = value;

				switch (_alignment)
				{
					case StackAlignment.Bottom: { Vertical = VerticalAlignment.Bottom; } break;
					case StackAlignment.Top: { Vertical = VerticalAlignment.Top; } break;
					case StackAlignment.Right: { Horizontal = HorizontalAlignment.Right; } break;
					case StackAlignment.Left: { Horizontal = HorizontalAlignment.Left; } break;
				}
			}
		}

		public override Point Position
		{
			get
			{
				return base.Position;
			}
			set
			{
				base.Position = value;

				//readd all the items
				var items = Items;
				Items = new List<IScreenItem>();
				foreach (var item in items)
				{
					AddItem(item);
				}
			}
		}

		public override HorizontalAlignment Horizontal
		{
			get
			{
				return base.Horizontal;
			}
			set
			{
				switch (Alignment)
				{
					case StackAlignment.Right:
						{
							base.Horizontal = HorizontalAlignment.Right;
						}
						break;
					case StackAlignment.Left:
						{
							base.Horizontal = HorizontalAlignment.Left;
						}
						break;
					default:
						{
							base.Horizontal = value;
						}
						break;
				}
				SetWidgetHorizontal(base.Horizontal);
            }
		}

		private void SetWidgetHorizontal(HorizontalAlignment alignment)
		{
			foreach (var item in Items)
			{
				item.Horizontal = alignment;
			}
		}

		public override VerticalAlignment Vertical
		{
			get
			{
				return base.Vertical;
			}
			set
			{
				switch (Alignment)
				{
					case StackAlignment.Bottom:
						{
							base.Vertical = VerticalAlignment.Bottom;
						}
						break;
					case StackAlignment.Top:
						{
							base.Vertical = VerticalAlignment.Top;
						}
						break;
					default:
						{
							base.Vertical = value;
						}
						break;
				}
				SetWidgetVertical(base.Vertical);
			}
		}

		private void SetWidgetVertical(VerticalAlignment alignment)
		{
			foreach (var item in Items)
			{
				item.Vertical = alignment;
			}
		}

		#endregion //Properties

		#region Initialization

		public StackLayout(StackAlignment alignment)
		{
			Alignment = alignment;
			Horizontal = HorizontalAlignment.Center;
		}

		/// <summary>
		/// Required to use this as a T argument in generic LayoutButton class
		/// </summary>
		public StackLayout() : this(StackAlignment.Top)
		{
		}

		public StackLayout(StackLayout inst) : base(inst)
		{
			_alignment = inst._alignment;
		}

		public override IScreenItem DeepCopy()
		{
			return new StackLayout(this);
		}

		#endregion //Initialization

		#region Methods

		public override void AddItem(IScreenItem item)
		{
			item.Horizontal = Horizontal;
			item.Vertical = Vertical;

			//Check if it is a scalable item
			var scalable = item as IScalable;
			if (null != scalable)
			{
				scalable.Scale = Scale;
			}

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

			Sort();
		}

		/// <summary>
		/// add an item after another item
		/// </summary>
		/// <param name="item"></param>
		/// <param name="prevItem"></param>
		public void InsertItem(IScreenItem item, IScreenItem prevItem)
		{
			//create a temp list to hold everything
			var tempItems = new List<IScreenItem>();

			//add all the items to the list
			foreach (var currentItem in Items)
			{
				tempItems.Add(currentItem);

				//check if this is the item to add after
				if (currentItem == prevItem)
				{
					tempItems.Add(item);
				}
			}

			//create a new layout list
			Items = new List<IScreenItem>();

			//add all the temp items to the layout list
			foreach (var tempItem in tempItems)
			{
				AddItem(tempItem);
			}
		}

		private void AddTop(IScreenItem item)
		{
			//get the current rect
			Rectangle rect = Rect;

			//set the position of the new item
			item.Position = new Point(Position.X, rect.Top);

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
			item.Position = new Point(rect.Left, Position.Y);

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
				AddItem(tempItem);
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