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
			DefaultStyles.InitUnitTests();
			_button = new RelativeLayoutButton();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void RelButtonTests_Default()
		{
			Assert.AreEqual(0, _button.Rect.X);
			Assert.AreEqual(0, _button.Rect.Y);
			Assert.AreEqual(0, _button.Rect.Width);
			Assert.AreEqual(0, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		#endregion //Defaults

		#region Basic Changes

		[Test]
		public void RelButtonTests_Pos()
		{
			_button.Position = new Point(10, 20);
			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(0, _button.Rect.Width);
			Assert.AreEqual(0, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_Size()
		{
			_button.Size = new Vector2(30f, 40f);
			Assert.AreEqual(0, _button.Rect.X);
			Assert.AreEqual(0, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		#endregion //Basic Changes

		#region Layout positions

		[Test]
		public void RelButtonTests_LayoutPos()
		{
			_button.Position = new Point(10, 20);
			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(0, _button.Rect.Width);
			Assert.AreEqual(0, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);

			Assert.AreEqual(10, _button.Layout.Rect.X);
			Assert.AreEqual(20, _button.Layout.Rect.Y);
			Assert.AreEqual(0, _button.Layout.Rect.Width);
			Assert.AreEqual(0, _button.Layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Layout.Vertical);
		}

		[Test]
		public void RelButtonTests_LayoutSize()
		{
			_button.Size = new Vector2(30f, 40f);
			Assert.AreEqual(0, _button.Rect.X);
			Assert.AreEqual(0, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);

			Assert.AreEqual(0, _button.Layout.Rect.X);
			Assert.AreEqual(0, _button.Layout.Rect.Y);
			Assert.AreEqual(30f, _button.Layout.Rect.Width);
			Assert.AreEqual(40f, _button.Layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Layout.Vertical);
		}

		[Test]
		public void RelButtonTests_LayoutPadding()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Padding = new Vector2(1f, 2f);
			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);

			Assert.AreEqual(11, _button.Layout.Rect.X);
			Assert.AreEqual(22, _button.Layout.Rect.Y);
			Assert.AreEqual(28f, _button.Layout.Rect.Width);
			Assert.AreEqual(36f, _button.Layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Layout.Vertical);
		}

		[Test]
		public void RelButtonTests_LayoutScale()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Padding = new Vector2(1f, 2f);
			_button.Scale = 2f;
			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(60f, _button.Rect.Width);
			Assert.AreEqual(80f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);

			Assert.AreEqual(12, _button.Layout.Rect.X);
			Assert.AreEqual(24, _button.Layout.Rect.Y);
			Assert.AreEqual(56f, _button.Layout.Rect.Width);
			Assert.AreEqual(72f, _button.Layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Layout.Vertical);
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

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_TopCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Center;
			_button.Vertical = VerticalAlignment.Top;

			Assert.AreEqual(-5, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_TopRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Right;
			_button.Vertical = VerticalAlignment.Top;

			Assert.AreEqual(-20, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_MiddleLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Center;

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(0, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_MiddleCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Center;
			_button.Vertical = VerticalAlignment.Center;

			Assert.AreEqual(-5, _button.Rect.X);
			Assert.AreEqual(0, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_MiddleRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Right;
			_button.Vertical = VerticalAlignment.Center;

			Assert.AreEqual(-20, _button.Rect.X);
			Assert.AreEqual(0, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_BottomLeft()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Left;
			_button.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(-20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_BottomCenter()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Center;
			_button.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(-5, _button.Rect.X);
			Assert.AreEqual(-20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _button.Vertical);
		}

		[Test]
		public void RelButtonTests_BottomRight()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(30f, 40f);
			_button.Horizontal = HorizontalAlignment.Right;
			_button.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(-20, _button.Rect.X);
			Assert.AreEqual(-20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _button.Vertical);
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
			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
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
			Assert.AreEqual(25, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Center, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
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
			Assert.AreEqual(40, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Right, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
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
			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(40, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, item.Vertical);

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(30f, _button.Rect.Width);
			Assert.AreEqual(40f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
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
			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(60, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, item.Vertical);
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
			Assert.AreEqual(100, item.Rect.X);
			Assert.AreEqual(200, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);
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
			Assert.AreEqual(110, item.Rect.X);
			Assert.AreEqual(220, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Right, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, item.Vertical);

			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(100f, _button.Rect.Width);
			Assert.AreEqual(200f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		#endregion //Change Layout Properties
	}
}
