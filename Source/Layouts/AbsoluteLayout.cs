
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is just a list of items that are on the screen.
	/// The user has to take care of placing them etc.
	/// </summary>
	public class AbsoluteLayout : Layout, IScalable
	{
		#region Fields

		private Vector2 _size;

		/// <summary>
		/// Whenever the rect changes on this layout, the old rect is stored here so can get a delta.
		/// </summary>
		private Rectangle PreviousRect
		{
			get; set;
		}

		#endregion //Fields

		#region Properties

		public virtual Vector2 Size
		{
			get
			{
				return _size;
			}
			set
			{
				SetPrevRect();
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
				SetPrevRect();
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
				if (base.Horizontal != value)
				{
					SetPrevRect();
					base.Horizontal = value;
					UpdateItems();
				}
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
				if (base.Vertical != value)
				{
					SetPrevRect();
					base.Vertical = value;
					UpdateItems();
				}
			}
		}

		public override float Scale
		{
			get
			{
				return base.Scale;
			}
			set
			{
				base.Scale = value;
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

		public AbsoluteLayout()
		{
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

		private void SetPrevRect()
		{
			PreviousRect = CalculateRect();
		}

		protected Rectangle CalculateRect()
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
				UpdateItemPosition(item, rect);
			}
		}

		/// <summary>
		/// Set the position of a widget to be relative to this layout
		/// </summary>
		/// <param name="item"></param>
		private void UpdateItemPosition(IScreenItem item, Rectangle rect)
		{
			//Get the delta position
			var delta = PreviousRect.Location - rect.Location;

			//add the delt to the current position
			item.Position -= delta;
		}

		/// <summary>
		/// Set the position of a widget to be relative to this layout
		/// </summary>
		/// <param name="item"></param>
		protected virtual void SetItemPosition(IScreenItem item, Rectangle rect)
		{
			//add the position of the layout to the item
			item.Position += rect.Location;
		}

		#endregion //Methods
	}
}