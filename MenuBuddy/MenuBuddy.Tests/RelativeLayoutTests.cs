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
			StyleSheet.InitUnitTests();
			_layout = new RelativeLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void RelativeLayoutTests_Default()
		{
			Assert.AreEqual(0, _layout.Rect.X);
			Assert.AreEqual(0, _layout.Rect.Y);
			Assert.AreEqual(0, _layout.Rect.Width);
			Assert.AreEqual(0, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		#endregion //Defaults

		#region Basic Changes

		[Test]
		public void RelativeLayoutTests_Pos()
		{
			_layout.Position = new Point(10, 20);
			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(0, _layout.Rect.Width);
			Assert.AreEqual(0, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_Size()
		{
			_layout.Size = new Vector2(30f, 40f);
			Assert.AreEqual(0, _layout.Rect.X);
			Assert.AreEqual(0, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_TopCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Center;
			_layout.Vertical = VerticalAlignment.Top;

			Assert.AreEqual(-5, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_TopRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Right;
			_layout.Vertical = VerticalAlignment.Top;

			Assert.AreEqual(-20, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_MiddleLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Center;

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(0, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_MiddleCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Center;
			_layout.Vertical = VerticalAlignment.Center;

			Assert.AreEqual(-5, _layout.Rect.X);
			Assert.AreEqual(0, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_MiddleRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Right;
			_layout.Vertical = VerticalAlignment.Center;

			Assert.AreEqual(-20, _layout.Rect.X);
			Assert.AreEqual(0, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_BottomLeft()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(-20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_BottomCenter()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Center;
			_layout.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(-5, _layout.Rect.X);
			Assert.AreEqual(-20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _layout.Vertical);
		}

		[Test]
		public void RelativeLayoutTests_BottomRight()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30f, 40f);
			_layout.Horizontal = HorizontalAlignment.Right;
			_layout.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(-20, _layout.Rect.X);
			Assert.AreEqual(-20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _layout.Vertical);
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
			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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
			Assert.AreEqual(25, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Center, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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
			Assert.AreEqual(40, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Right, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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
			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(40, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Center, item.Vertical);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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
			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(60, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, item.Vertical);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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
			Assert.AreEqual(100, item.Rect.X);
			Assert.AreEqual(200, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(30f, _layout.Rect.Width);
			Assert.AreEqual(40f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
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
			Assert.AreEqual(110, item.Rect.X);
			Assert.AreEqual(220, item.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Right, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, item.Vertical);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(100f, _layout.Rect.Width);
			Assert.AreEqual(200f, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		#endregion //Change Layout Properties
	}
}
