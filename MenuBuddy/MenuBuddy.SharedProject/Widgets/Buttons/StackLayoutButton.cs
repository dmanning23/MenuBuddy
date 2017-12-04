using System;
using System.Collections.Generic;
using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button thhat contains a relaitve layout
	/// </summary>
	public class StackLayoutButton : LayoutButton<StackLayout>, IStackLayout
	{
		#region Properties

		public StackAlignment Alignment
		{
			get
			{
				return (Layout as StackLayout).Alignment;
			}
			set
			{
				(Layout as StackLayout).Alignment = value;
			}
		}

		public List<IScreenItem> Items
		{
			get
			{
				return (Layout as StackLayout).Items;
			}
		}

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public StackLayoutButton()
		{
			Layout = new StackLayout();
		}

		public StackLayoutButton(StackLayoutButton inst) : base(inst)
		{
			Layout = new StackLayout(inst.Layout as StackLayout);
		}

		public event EventHandler<DragEventArgs> OnDrag;
		public event EventHandler<DropEventArgs> OnDrop;

		/// <summary>
		/// Get a deep copy of this item
		/// </summary>
		/// <returns></returns>
		public override IScreenItem DeepCopy()
		{
			return new StackLayoutButton(this);
		}

		#endregion //Initialization

		#region Methods

		protected override void CalculateRect()
		{
			//get the size of the rect
			var size = Size * Scale;

			//set the x component
			Vector2 pos = Position.ToVector2();
			switch (Horizontal)
			{
				case HorizontalAlignment.Center: { pos.X -= size.X / 2f; } break;
				case HorizontalAlignment.Right: { pos.X -= size.X; } break;
			}

			//set the y component
			switch (Vertical)
			{
				case VerticalAlignment.Center: { pos.Y -= size.Y / 2f; } break;
				case VerticalAlignment.Bottom: { pos.Y -= size.Y; } break;
			}

			_rect = new Rectangle((int)pos.X, (int)pos.Y, (int)size.X, (int)size.Y);

			//Set the position of the internal layout
			var relLayout = Layout as StackLayout;
			if (null != relLayout)
			{
				relLayout.Scale = Scale;
				relLayout.Vertical = VerticalAlignment.Top;
				relLayout.Horizontal = HorizontalAlignment.Left;
				relLayout.Position = pos.ToPoint();
			}

			_rect = relLayout.Rect;
		}

		public void InsertItem(IScreenItem item, IScreenItem prevItem)
		{
			(Layout as StackLayout).InsertItem(item, prevItem);
		}

		public void InsertItemBefore(IScreenItem item, IScreenItem nextItem)
		{
			(Layout as StackLayout).InsertItemBefore(item, nextItem);
		}

		public bool RemoveItems<T>() where T : IScreenItem
		{
			return (Layout as StackLayout).RemoveItems<T>();
		}

		public bool CheckDrag(DragEventArgs drag)
		{
			return (Layout as StackLayout).CheckDrag(drag);
		}

		public bool CheckDrop(DropEventArgs drop)
		{
			return (Layout as StackLayout).CheckDrop(drop);
		}

		#endregion
	}
}