using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace MenuBuddy
{
	public class PaddedLayout : AbsoluteLayout
	{
		#region Fields

		protected int _left;
		protected int _right;
		protected int _top;
		protected int _bottom;
		protected IScreenItem _item;

		#endregion //Fields

		#region Properties

		public int Left
		{
			get
			{
				return _left;
			}
			set
			{
				_left = value;
				UpdateItems();
			}
		}

		public int Right
		{
			get
			{
				return _right;
			}
			set
			{
				_right = value;
				UpdateItems();
			}
		}

		public int Top
		{
			get
			{
				return _top;
			}
			set
			{
				_top = value;
				UpdateItems();
			}
		}

		public int Bottom
		{
			get
			{
				return _bottom;
			}
			set
			{
				_bottom = value;
				UpdateItems();
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

				//rescale the child item
				var scalable = _item as IScalable;
				if (null != scalable)
				{
					scalable.Scale = value;
				}
				UpdateItems();
			}
		}

		#endregion //Properties

		#region Methods

		public PaddedLayout(int left, int right, int top, int bottom, IScreenItem item)
		{
			base.AddItem(item);
			_left = left;
			_right = right;
			_top = top;
			_bottom = bottom;
			_item = item;

			_item.Horizontal = HorizontalAlignment.Left;
			_item.Vertical = VerticalAlignment.Top;
			UpdateItems();
		}

		public override void AddItem(IScreenItem item)
		{
			throw new Exception("PaddedLayout doesn't support the AddItem method.");
		}

		protected override void UpdateItemPosition(IScreenItem item, Rectangle rect)
		{
			var scaleLeft = (Scale * Left);
			var scaleRight = (Scale * Right);
			var scaleTop = (Scale * Top);
			var scaleBottom = (Scale * Bottom);

			if (null != _item)
			{
				//update the layout size
				_size = new Vector2(_item.Rect.Size.X + scaleLeft + scaleRight,
				_item.Rect.Size.Y + scaleTop + scaleBottom);

				//update the item position
				_item.Position = new Point(Rect.Left + (int)scaleLeft, Rect.Top + (int)scaleTop);
			}
		}

		#endregion //Methods
	}
}
