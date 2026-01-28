using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class RelativeLayoutTests
	{
		#region Fields

		private RelativeLayout _layout;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			_layout = new RelativeLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void RelativeLayoutTests_Default()
		{
			_layout.Rect.X.ShouldBe(0);
			_layout.Rect.Y.ShouldBe(0);
			_layout.Rect.Width.ShouldBe(0);
			_layout.Rect.Height.ShouldBe(0);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Defaults

		#region Basic Changes

		[Test]
		public void RelativeLayoutTests_Pos()
		{
			_layout.Position = new Point(10, 20);
			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(0);
			_layout.Rect.Height.ShouldBe(0);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_Size()
		{
			_layout.Size = new Vector2(30f, 40f);
			_layout.Rect.X.ShouldBe(0);
			_layout.Rect.Y.ShouldBe(0);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Basic Changes

		#region Alignment Changes

		[Test]
		public void RelativeLayoutTests_TopLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_TopCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Center;
			_layout.Vertical = VerticalAlignment.Top;

			_layout.Rect.X.ShouldBe(-5);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_TopRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Right;
			_layout.Vertical = VerticalAlignment.Top;

			_layout.Rect.X.ShouldBe(-20);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_MiddleLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Center;

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(0);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Center);
		}

		[Test]
		public void RelativeLayoutTests_MiddleCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Center;
			_layout.Vertical = VerticalAlignment.Center;

			_layout.Rect.X.ShouldBe(-5);
			_layout.Rect.Y.ShouldBe(0);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_layout.Vertical.ShouldBe(VerticalAlignment.Center);
		}

		[Test]
		public void RelativeLayoutTests_MiddleRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Right;
			_layout.Vertical = VerticalAlignment.Center;

			_layout.Rect.X.ShouldBe(-20);
			_layout.Rect.Y.ShouldBe(0);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_layout.Vertical.ShouldBe(VerticalAlignment.Center);
		}

		[Test]
		public void RelativeLayoutTests_BottomLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Bottom;

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(-20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		[Test]
		public void RelativeLayoutTests_BottomCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Center;
			_layout.Vertical = VerticalAlignment.Bottom;

			_layout.Rect.X.ShouldBe(-5);
			_layout.Rect.Y.ShouldBe(-20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_layout.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		[Test]
		public void RelativeLayoutTests_BottomRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Right;
			_layout.Vertical = VerticalAlignment.Bottom;

			_layout.Rect.X.ShouldBe(-20);
			_layout.Rect.Y.ShouldBe(-20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_layout.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		#endregion //Alignment Changes

		#region Item Alignment Tests

		[Test]
		public void RelativeLayoutTests_Item_TopLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};
			_layout.AddItem(item);
			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(20);

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_Item_TopCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Top
			};
			_layout.AddItem(item);
			item.Rect.X.ShouldBe(25);
			item.Rect.Y.ShouldBe(20);
			item.Horizontal.ShouldBe(HorizontalAlignment.Center);
			item.Vertical.ShouldBe(VerticalAlignment.Top);

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_Item_TopRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Top
			};
			_layout.AddItem(item);
			item.Rect.X.ShouldBe(40);
			item.Rect.Y.ShouldBe(20);
			item.Horizontal.ShouldBe(HorizontalAlignment.Right);
			item.Vertical.ShouldBe(VerticalAlignment.Top);

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_Item_MiddleLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center
			};
			_layout.AddItem(item);
			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(40);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Center);

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_Item_BottomLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom
			};
			_layout.AddItem(item);
			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(60);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Bottom);

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Item Alignment Tests

		#region Change Layout Properties

		[Test]
		public void RelativeLayoutTests_Item_ChangePos()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};
			_layout.AddItem(item);

			_layout.Position = new Point(100, 200);
			item.Rect.X.ShouldBe(100);
			item.Rect.Y.ShouldBe(200);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Top);

			_layout.Rect.X.ShouldBe(100);
			_layout.Rect.Y.ShouldBe(200);
			_layout.Rect.Width.ShouldBe(30);
			_layout.Rect.Height.ShouldBe(40);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelativeLayoutTests_Item_ChangeSize()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Bottom
			};
			_layout.AddItem(item);

			_layout.Size = new Vector2(100f, 200f);
			item.Rect.X.ShouldBe(110);
			item.Rect.Y.ShouldBe(220);
			item.Horizontal.ShouldBe(HorizontalAlignment.Right);
			item.Vertical.ShouldBe(VerticalAlignment.Bottom);

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(100);
			_layout.Rect.Height.ShouldBe(200);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Change Layout Properties
	}
}
