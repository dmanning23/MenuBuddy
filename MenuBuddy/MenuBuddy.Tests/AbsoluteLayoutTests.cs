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

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(0, _layout.Rect.Width);
			Assert.AreEqual(0, _layout.Rect.Height);
			Assert.AreEqual(110, shim.Rect.X);
			Assert.AreEqual(220, shim.Rect.Y);
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

			Assert.AreEqual(110, shim.Rect.X);
			Assert.AreEqual(220, shim.Rect.Y);
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

			Assert.AreEqual(110, shim.Rect.X);
			Assert.AreEqual(220, shim.Rect.Y);
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

			Assert.AreEqual(110, shim.Rect.X);
			Assert.AreEqual(220, shim.Rect.Y);
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

			Assert.AreEqual(110, shim.Rect.X);
			Assert.AreEqual(220, shim.Rect.Y);
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

			Assert.AreEqual(110, shim1.Rect.X);
			Assert.AreEqual(220, shim1.Rect.Y);
			Assert.AreEqual(110, shim2.Rect.X);
			Assert.AreEqual(220, shim2.Rect.Y);
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

			Assert.AreEqual(110, shim1.Rect.X);
			Assert.AreEqual(220, shim1.Rect.Y);
			Assert.AreEqual(110, shim2.Rect.X);
			Assert.AreEqual(220, shim2.Rect.Y);
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

			Assert.AreEqual(110, shim1.Rect.X);
			Assert.AreEqual(220, shim1.Rect.Y);
			Assert.AreEqual(110, shim2.Rect.X);
			Assert.AreEqual(220, shim2.Rect.Y);
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

			Assert.AreEqual(110, shim1.Rect.X);
			Assert.AreEqual(220, shim1.Rect.Y);
			Assert.AreEqual(110, shim2.Rect.X);
			Assert.AreEqual(220, shim2.Rect.Y);
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

			Assert.AreEqual(110, shim1.Rect.X);
			Assert.AreEqual(220, shim1.Rect.Y);
			Assert.AreEqual(110, shim2.Rect.X);
			Assert.AreEqual(220, shim2.Rect.Y);
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

			Assert.AreEqual(110, shim1.Rect.X);
			Assert.AreEqual(220, shim1.Rect.Y);
			Assert.AreEqual(160, shim2.Rect.X);
			Assert.AreEqual(260, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		#endregion //OneItem

		#region ChangeProperties

		[Test]
		public void AbsoluteLayoutTests_ChangeProperties_default()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(300f, 400f);
			_layout.Vertical = VerticalAlignment.Top;
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left
			};
			_layout.AddItem(shim);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(300, _layout.Rect.Width);
			Assert.AreEqual(400, _layout.Rect.Height);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);

			Assert.AreEqual(110, shim.Rect.X);
			Assert.AreEqual(220, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_ChangeProperties_pos()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(100f, 200f);
			_layout.Vertical = VerticalAlignment.Top;
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left
			};
			_layout.AddItem(shim);

			_layout.Position = new Point(1000, 2000);

			Assert.AreEqual(1000, _layout.Rect.X);
			Assert.AreEqual(2000, _layout.Rect.Y);
			Assert.AreEqual(100, _layout.Rect.Width);
			Assert.AreEqual(200, _layout.Rect.Height);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);

			Assert.AreEqual(1100, shim.Position.X);
			Assert.AreEqual(2200, shim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_ChangeProperties_hor()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(100f, 200f);
			_layout.Vertical = VerticalAlignment.Top;
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left
			};
			_layout.AddItem(shim);

			_layout.Horizontal = HorizontalAlignment.Right;

			Assert.AreEqual(-90, _layout.Rect.X);
			Assert.AreEqual(20, _layout.Rect.Y);
			Assert.AreEqual(100, _layout.Rect.Width);
			Assert.AreEqual(200, _layout.Rect.Height);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
			Assert.AreEqual(HorizontalAlignment.Right, _layout.Horizontal);

			Assert.AreEqual(10, shim.Position.X);
			Assert.AreEqual(220, shim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_ChangeProperties_vert()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(100f, 200f);
			_layout.Vertical = VerticalAlignment.Top;
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left
			};
			_layout.AddItem(shim);

			_layout.Vertical = VerticalAlignment.Bottom;

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(-180, _layout.Rect.Y);
			Assert.AreEqual(100, _layout.Rect.Width);
			Assert.AreEqual(200, _layout.Rect.Height);
			Assert.AreEqual(VerticalAlignment.Bottom, _layout.Vertical);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);

			Assert.AreEqual(110, shim.Position.X);
			Assert.AreEqual(20, shim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		[Test]
		public void AbsoluteLayoutTests_ChangeProperties_size()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(100f, 200f);
			_layout.Vertical = VerticalAlignment.Top;
			_layout.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40),
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left
			};
			_layout.AddItem(shim);

			_layout.Vertical = VerticalAlignment.Bottom;
			_layout.Size = new Vector2(100f, 400f);

			Assert.AreEqual(10, _layout.Rect.X);
			Assert.AreEqual(-380, _layout.Rect.Y);
			Assert.AreEqual(100, _layout.Rect.Width);
			Assert.AreEqual(400, _layout.Rect.Height);
			Assert.AreEqual(VerticalAlignment.Bottom, _layout.Vertical);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);

			Assert.AreEqual(110, shim.Position.X);
			Assert.AreEqual(-180, shim.Position.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
		}

		#endregion //ChangeProperties
	}
}
