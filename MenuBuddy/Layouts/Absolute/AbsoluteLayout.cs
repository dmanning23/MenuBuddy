using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is just a list of items that are on the screen.
	/// The user has to take care of placing them etc.
	/// </summary>
	public class AbsoluteLayout : Layout, IScalable
	{
		#region Properties

		/// <summary>
		/// The size of this layout.
		/// </summary>
		protected Vector2 _size;

		/// <summary>
		/// Whenever the rect changes on this layout, the old rect is stored here so can get a delta.
		/// </summary>
		private Rectangle PreviousRect
		{
			get; set;
		}

		/// <summary>
		/// The size of this layout.
		/// </summary>
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

		/// <summary>
		/// The position of this layout.
		/// </summary>
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

		/// <summary>
		/// The horizontal alignment of this layout.
		/// </summary>
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

		/// <summary>
		/// The vertical alignment of this layout.
		/// </summary>
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

		/// <summary>
		/// The scale of this layout.
		/// </summary>
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

		/// <summary>
		/// The bounding rectangle of this layout.
		/// </summary>
		public override Rectangle Rect
		{
			get
			{
				return CalculateRect();
			}
		}

		#endregion //Properties

		#region Init

		/// <summary>
		/// Create a new absolute layout.
		/// </summary>
		public AbsoluteLayout()
		{
		}

		/// <summary>
		/// Copy constructor
		/// </summary>
		/// <param name="inst">The instance to copy</param>
		public AbsoluteLayout(AbsoluteLayout inst) : base(inst)
		{
			_size = new Vector2(inst._size.X, inst._size.Y);
			PreviousRect = inst.PreviousRect;
		}

		/// <summary>
		/// Create a deep copy of this layout.
		/// </summary>
		/// <returns>The copied layout</returns>
		public override IScreenItem DeepCopy()
		{
			return new AbsoluteLayout(this);
		}

		#endregion //Init

		#region Methods

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

		/// <summary>
		/// Calculate the bounding rectangle of this layout based on its position, size, and alignment.
		/// </summary>
		/// <returns>The calculated rectangle</returns>
		protected virtual Rectangle CalculateRect()
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

		/// <summary>
		/// Update the positions of all the items in this layout.
		/// </summary>
		protected virtual void UpdateItems()
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
		protected virtual void UpdateItemPosition(IScreenItem item, Rectangle rect)
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