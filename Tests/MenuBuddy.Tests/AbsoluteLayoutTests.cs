using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class AbsoluteLayoutTests
	{
		#region Fields

		private AbsoluteLayout _layout;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			DefaultStyles.InitUnitTests();
			_layout = new AbsoluteLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void AbsoluteLayoutTests_Default()
		{
			_layout.Position = new Point(10, 20);
			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(0, _layout.Rect.Width);
			Assert.AreEqual(0, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		#endregion //Defaults

		#region OneItem

		[Test]
		public void AbsoluteLayoutTests_AddItem_default()
		{
			_layout.Position = new Point(10, 20);

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
            _layout.AddItem(shim);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(30, _layout.Rect.Width);
			Assert.AreEqual(40, _layout.Rect.Height);
			Assert.AreEqual(100, shim.Rect.X);
			Assert.AreEqual(200, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_AddItem_LeftHorizontal()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(30, _layout.Rect.Width);
			Assert.AreEqual(40, _layout.Rect.Height);
			Assert.AreEqual(100, shim.Rect.X);
			Assert.AreEqual(200, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_AddItem_RightHorizontal()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Right;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(30, _layout.Rect.Width);
			Assert.AreEqual(40, _layout.Rect.Height);
			Assert.AreEqual(100, shim.Rect.X);
			Assert.AreEqual(200, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_AddItem_CenterVertical()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Center;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(30, _layout.Rect.Width);
			Assert.AreEqual(40, _layout.Rect.Height);
			Assert.AreEqual(100, shim.Rect.X);
			Assert.AreEqual(200, shim.Rect.Y);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_AddItem_BottomVertical()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Bottom;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(30, _layout.Rect.Width);
			Assert.AreEqual(40, _layout.Rect.Height);
			Assert.AreEqual(100, shim.Rect.X);
			Assert.AreEqual(200, shim.Rect.Y);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		#endregion //OneItem

		#region TwoItem

		[Test]
		public void AbsoluteLayoutTests_TwoItem_default()
		{
			_layout.Position = new Point(10, 20);

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_layout.AddItem(shim2);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(50, _layout.Rect.Width);
			Assert.AreEqual(60, _layout.Rect.Height);
			Assert.AreEqual(100, shim1.Rect.X);
			Assert.AreEqual(200, shim1.Rect.Y);
			Assert.AreEqual(100, shim2.Rect.X);
			Assert.AreEqual(200, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_TwoItem_LeftHorizontal()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_layout.AddItem(shim2);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(50, _layout.Rect.Width);
			Assert.AreEqual(60, _layout.Rect.Height);
			Assert.AreEqual(100, shim1.Rect.X);
			Assert.AreEqual(200, shim1.Rect.Y);
			Assert.AreEqual(100, shim2.Rect.X);
			Assert.AreEqual(200, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_TwoItem_RightHorizontal()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Right;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_layout.AddItem(shim2);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(50, _layout.Rect.Width);
			Assert.AreEqual(60, _layout.Rect.Height);
			Assert.AreEqual(100, shim1.Rect.X);
			Assert.AreEqual(200, shim1.Rect.Y);
			Assert.AreEqual(100, shim2.Rect.X);
			Assert.AreEqual(200, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_TwoItem_CenterVertical()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Center;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_layout.AddItem(shim2);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(50, _layout.Rect.Width);
			Assert.AreEqual(60, _layout.Rect.Height);
			Assert.AreEqual(100, shim1.Rect.X);
			Assert.AreEqual(200, shim1.Rect.Y);
			Assert.AreEqual(100, shim2.Rect.X);
			Assert.AreEqual(200, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_TwoItem_BottomVertical()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_layout.AddItem(shim2);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(50, _layout.Rect.Width);
			Assert.AreEqual(60, _layout.Rect.Height);
			Assert.AreEqual(100, shim1.Rect.X);
			Assert.AreEqual(200, shim1.Rect.Y);
			Assert.AreEqual(100, shim2.Rect.X);
			Assert.AreEqual(200, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_TwoItem_FarAway()
		{
			_layout.Position = new Point(10, 20);
			_layout.Horizontal = HorizontalAlignment.Left;
			_layout.Vertical = VerticalAlignment.Top;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_layout.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(150, 240),
				Size = new Vector2(50, 60)
			};
			_layout.AddItem(shim2);

			Assert.AreEqual(100, _layout.Rect.X);
			Assert.AreEqual(200, _layout.Rect.Y);
			Assert.AreEqual(100, _layout.Rect.Width);
			Assert.AreEqual(100, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		#endregion //OneItem
	}
}
