using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a layout that places items based on the alignmentof the item
	/// Set the rectangle of this layout first, then add all the items
	/// </summary>
	public class RelativeLayout : Layout, IScalable
	{
		#region Fields

		private Vector2 _size;

		#endregion //Fields

		#region Properties

		public Vector2 Size
		{
			get
			{
				return _size;
			}
			set
			{
				_size = value;
				UpdateItems();
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
				UpdateItems();
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
				base.Horizontal = value;
				UpdateItems();
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
				base.Vertical = value;
				UpdateItems();
			}
		}

		public override Rectangle Rect
		{
			get
			{
				return CalculateRect();
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		public RelativeLayout()
		{
			_size = Vector2.Zero;
		}

		/// <summary>
		/// Set the position of the item as store it
		/// </summary>
		/// <param name="item"></param>
		public override void AddItem(IScreenItem item)
		{
			SetItemPosition(item, CalculateRect());

			//store the new item
			Items.Add(item);
			Sort();
		}

		private Rectangle CalculateRect()
		{
			var pos = Position;

			switch (Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X -= (int)(Size.X / 2f); } break;
				case HorizontalAlignment.Right: { pos.X -= (int)Size.X; } break;
			}

			switch (Vertical)
			{
				case VerticalAlignment.Center: { pos.Y -= (int)(Size.Y / 2f); } break;
				case VerticalAlignment.Bottom: { pos.Y -= (int)Size.Y; } break;
			}

			return new Rectangle(pos.X, pos.Y, (int)Size.X, (int)Size.Y);
		}

		private void UpdateItems()
		{
			//Grab the rect for this layout
			var rect = CalculateRect();

			//update the positions of all the current widgets
			foreach (var item in Items)
			{
				SetItemPosition(item, rect);
			}
		}

		/// <summary>
		/// Set the position of a widget to be relative to this layout
		/// </summary>
		/// <param name="item"></param>
		private void SetItemPosition(IScreenItem item, Rectangle rect)
		{
			//point to set the location of the item
			Point pos = Point.Zero;

			switch (item.Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X = Rect.Center.X; } break;
				case HorizontalAlignment.Left: { pos.X = Rect.Left; } break;
				case HorizontalAlignment.Right: { pos.X = Rect.Right; } break;
			}

			switch (item.Vertical)
			{
				case VerticalAlignment.Center: { pos.Y = Rect.Center.Y; } break;
				case VerticalAlignment.Top: { pos.Y = Rect.Top; } break;
				case VerticalAlignment.Bottom: { pos.Y = Rect.Bottom; } break;
			}

			//set the position of the new item
			item.Position = pos;
		}

		#endregion //Methods
	}
}