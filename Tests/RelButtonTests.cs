using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class RelButtonTests
	{
		#region Fields

		private RelativeLayoutButton _button;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			_button = new RelativeLayoutButton();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void RelButtonTests_Default()
		{
			_button.Rect.X.ShouldBe(0);
			_button.Rect.Y.ShouldBe(0);
			_button.Rect.Width.ShouldBe(0);
			_button.Rect.Height.ShouldBe(0);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Defaults

		#region Basic Changes

		[Test]
		public void RelButtonTests_Pos()
		{
			_button.Position = new Point(10, 20);
			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(0);
			_button.Rect.Height.ShouldBe(0);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_Size()
		{
			_button.Size = new Vector2(30f, 40f);
			_button.Rect.X.ShouldBe(0);
			_button.Rect.Y.ShouldBe(0);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Basic Changes

		#region Layout positions

		[Test]
		public void RelButtonTests_LayoutPos()
		{
			_button.Position = new Point(10, 20);
			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(0);
			_button.Rect.Height.ShouldBe(0);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);

			_button.Layout.Rect.X.ShouldBe(10);
			_button.Layout.Rect.Y.ShouldBe(20);
			_button.Layout.Rect.Width.ShouldBe(0);
			_button.Layout.Rect.Height.ShouldBe(0);
			_button.Layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_LayoutSize()
		{
			_button.Size = new Vector2(30f, 40f);
			_button.Rect.X.ShouldBe(0);
			_button.Rect.Y.ShouldBe(0);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);

			_button.Layout.Rect.X.ShouldBe(0);
			_button.Layout.Rect.Y.ShouldBe(0);
			_button.Layout.Rect.Width.ShouldBe(30);
			_button.Layout.Rect.Height.ShouldBe(40);
			_button.Layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		

		[Test]
		public void RelButtonTests_LayoutScale()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Scale = 2f;
			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(60);
			_button.Rect.Height.ShouldBe(80);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);

			_button.Layout.Rect.X.ShouldBe(10);
			_button.Layout.Rect.Y.ShouldBe(20);
			_button.Layout.Rect.Width.ShouldBe(60);
			_button.Layout.Rect.Height.ShouldBe(80);
			_button.Layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Layout positions

		#region Alignment Changes

		[Test]
		public void RelButtonTests_TopLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_TopCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Center;
			_button.Vertical = VerticalAlignment.Top;

			_button.Rect.X.ShouldBe(-5);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_TopRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Right;
			_button.Vertical = VerticalAlignment.Top;

			_button.Rect.X.ShouldBe(-20);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_MiddleLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Center;

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(0);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Center);
		}

		[Test]
		public void RelButtonTests_MiddleCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Center;
			_button.Vertical = VerticalAlignment.Center;

			_button.Rect.X.ShouldBe(-5);
			_button.Rect.Y.ShouldBe(0);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_button.Vertical.ShouldBe(VerticalAlignment.Center);
		}

		[Test]
		public void RelButtonTests_MiddleRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Right;
			_button.Vertical = VerticalAlignment.Center;

			_button.Rect.X.ShouldBe(-20);
			_button.Rect.Y.ShouldBe(0);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_button.Vertical.ShouldBe(VerticalAlignment.Center);
		}

		[Test]
		public void RelButtonTests_BottomLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Bottom;

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(-20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		[Test]
		public void RelButtonTests_BottomCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Center;
			_button.Vertical = VerticalAlignment.Bottom;

			_button.Rect.X.ShouldBe(-5);
			_button.Rect.Y.ShouldBe(-20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_button.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		[Test]
		public void RelButtonTests_BottomRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Right;
			_button.Vertical = VerticalAlignment.Bottom;

			_button.Rect.X.ShouldBe(-20);
			_button.Rect.Y.ShouldBe(-20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_button.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		#endregion //Alignment Changes

		#region Item Alignment Tests

		[Test]
		public void RelButtonTests_Item_TopLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};
			_button.AddItem(item);
			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(20);

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_Item_TopCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Top
			};
			_button.AddItem(item);
			item.Rect.X.ShouldBe(25);
			item.Rect.Y.ShouldBe(20);
			item.Horizontal.ShouldBe(HorizontalAlignment.Center);
			item.Vertical.ShouldBe(VerticalAlignment.Top);

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_Item_TopRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Top
			};
			_button.AddItem(item);
			item.Rect.X.ShouldBe(40);
			item.Rect.Y.ShouldBe(20);
			item.Horizontal.ShouldBe(HorizontalAlignment.Right);
			item.Vertical.ShouldBe(VerticalAlignment.Top);

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_Item_MiddleLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Center
			};
			_button.AddItem(item);
			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(40);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Center);

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(30);
			_button.Rect.Height.ShouldBe(40);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_Item_BottomLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom
			};
			_button.AddItem(item);
			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(60);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		#endregion //Item Alignment Tests

		#region Change Layout Properties

		[Test]
		public void RelButtonTests_Item_ChangePos()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			};
			_button.AddItem(item);

			_button.Position = new Point(100, 200);
			item.Rect.X.ShouldBe(100);
			item.Rect.Y.ShouldBe(200);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void RelButtonTests_Item_ChangeSize()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Top;

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Right,
				Vertical = VerticalAlignment.Bottom
			};
			_button.AddItem(item);

			_button.Size = new Vector2(100f, 200f);
			item.Rect.X.ShouldBe(110);
			item.Rect.Y.ShouldBe(220);
			item.Horizontal.ShouldBe(HorizontalAlignment.Right);
			item.Vertical.ShouldBe(VerticalAlignment.Bottom);

			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(100);
			_button.Rect.Height.ShouldBe(200);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Change Layout Properties
	}
}
