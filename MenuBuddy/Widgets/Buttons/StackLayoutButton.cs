using System;
using System.Collections.Generic;
using InputHelper;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// A button that uses a <see cref="StackLayout"/> to arrange its child items sequentially.
	/// </summary>
	public class StackLayoutButton : LayoutButton<StackLayout>, IStackLayout
	{
		#region Properties

		/// <summary>
		/// The stack alignment direction for the inner layout.
		/// </summary>
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

		/// <summary>
		/// The list of child screen items in the inner layout.
		/// </summary>
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
		/// Initializes a new <see cref="StackLayoutButton"/> with a default stack layout.
		/// </summary>
		public StackLayoutButton()
		{
			Layout = new StackLayout();
		}

		/// <summary>
		/// Initializes a new <see cref="StackLayoutButton"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The button to copy from.</param>
		public StackLayoutButton(StackLayoutButton inst) : base(inst)
		{
			Layout = new StackLayout(inst.Layout as StackLayout);
		}

		/// <summary>
		/// Raised when a drag operation occurs on this button.
		/// </summary>
		public event EventHandler<DragEventArgs> OnDrag;

		/// <summary>
		/// Raised when a drop operation occurs on this button.
		/// </summary>
		public event EventHandler<DropEventArgs> OnDrop;

		/// <summary>
		/// Creates a deep copy of this button.
		/// </summary>
		/// <returns>A new <see cref="StackLayoutButton"/> that is a copy of this instance.</returns>
		public override IScreenItem DeepCopy()
		{
			return new StackLayoutButton(this);
		}

		#endregion //Initialization

		#region Methods

		/// <inheritdoc/>
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

		/// <summary>
		/// Inserts a screen item into the stack layout after the specified item.
		/// </summary>
		/// <param name="item">The screen item to insert.</param>
		/// <param name="prevItem">The item after which to insert the new item.</param>
		public void InsertItem(IScreenItem item, IScreenItem prevItem)
		{
			(Layout as StackLayout).InsertItem(item, prevItem);
		}

		/// <summary>
		/// Inserts a screen item into the stack layout before the specified item.
		/// </summary>
		/// <param name="item">The screen item to insert.</param>
		/// <param name="nextItem">The item before which to insert the new item.</param>
		public void InsertItemBefore(IScreenItem item, IScreenItem nextItem)
		{
			(Layout as StackLayout).InsertItemBefore(item, nextItem);
		}

		/// <summary>
		/// Removes all items of the specified type from the layout.
		/// </summary>
		/// <typeparam name="T">The type of items to remove.</typeparam>
		/// <returns><c>true</c> if any items were removed; otherwise, <c>false</c>.</returns>
		public bool RemoveItems<T>() where T : IScreenItem
		{
			return Layout.RemoveItems<T>();
		}

		/// <summary>
		/// Removes all items from the layout.
		/// </summary>
		public void Clear()
		{
			Layout.Clear();
		}

		/// <summary>
		/// Forwards a drag check to the inner layout.
		/// </summary>
		/// <param name="drag">The drag event arguments.</param>
		/// <returns><c>true</c> if the drag was handled; otherwise, <c>false</c>.</returns>
		public bool CheckDrag(DragEventArgs drag)
		{
			return Layout.CheckDrag(drag);
		}

		/// <summary>
		/// Forwards a drop check to the inner layout.
		/// </summary>
		/// <param name="drop">The drop event arguments.</param>
		/// <returns><c>true</c> if the drop was handled; otherwise, <c>false</c>.</returns>
		public bool CheckDrop(DropEventArgs drop)
		{
			return Layout.CheckDrop(drop);
		}

		#endregion
	}
}