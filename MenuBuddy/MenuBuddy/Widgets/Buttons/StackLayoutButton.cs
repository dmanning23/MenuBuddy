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
				return Layout.Items;
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
			var layout = Layout as StackLayout;
			if (null != layout)
			{
				layout.Scale = Scale;
				layout.Vertical = VerticalAlignment.Top;
				layout.Horizontal = HorizontalAlignment.Left;
				layout.Position = pos.ToPoint();
			}

			_rect = layout.Rect;
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
			return Layout.RemoveItems<T>();
		}

		public void Clear()
		{
			Layout.Clear();
		}

		public bool CheckDrag(DragEventArgs drag)
		{
			return Layout.CheckDrag(drag);
		}

		public bool CheckDrop(DropEventArgs drop)
		{
			return Layout.CheckDrop(drop);
		}

		#endregion
	}
}