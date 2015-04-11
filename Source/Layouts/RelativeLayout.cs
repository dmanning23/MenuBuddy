using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a layout that places items based on the alignmentof the item
	/// Set the rectangle of this layout first, then add all the items
	/// </summary>
	public class RelativeLayout : Layout
	{
		#region Fields

		/// <summary>
		/// This layout type stores its own rect
		/// </summary>
		private Rectangle _rect;

		#endregion //Fields

		#region Properties

		public override Rectangle Rect
		{
			get { return _rect; }
		}

		public Rectangle Rectangle
		{
			set
			{
				//Grab the rect for this layout
				_rect = value;

				//update the positions of all the current widgets
				foreach (var item in Items)
				{
					SetWidgetPosition(item);
				}
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public RelativeLayout()
		{
			_rect = Rectangle.Empty;
		}

		/// <summary>
		/// Set the position of the item as store it
		/// </summary>
		/// <param name="item"></param>
		public override void AddItem(IScreenItem item)
		{
			SetWidgetPosition(item);

			//store the new item
			Items.Add(item);
		}

		/// <summary>
		/// Set the position of a widget to be relative to this layout
		/// </summary>
		/// <param name="widget"></param>
		private void SetWidgetPosition(IScreenItem item)
		{
			//Check if the item is a widget
			IWidget widget = item as IWidget;
			if (null != widget)
			{
				//point to set the location of the item
				Point pos = Point.Zero;

				switch (widget.Horizontal)
				{
					case HorizontalAlignment.Center: { pos.X = Rect.Center.X; } break;
					case HorizontalAlignment.Left: { pos.X = Rect.Left; } break;
					case HorizontalAlignment.Right: { pos.X = Rect.Right; } break;
				}

				switch (widget.Vertical)
				{
					case VerticalAlignment.Center: { pos.Y = Rect.Center.Y; } break;
					case VerticalAlignment.Top: { pos.Y = Rect.Top; } break;
					case VerticalAlignment.Bottom: { pos.Y = Rect.Bottom; } break;
				}

				//set the position of the new item
				item.Position = pos;
			}
		}

		#endregion //Methods
	}
}