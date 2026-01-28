using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class AbsoluteLayoutTests
	{
        #region Fields

#pragma warning disable NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method
        private AbsoluteLayout _layout;
#pragma warning restore NUnit1032 // An IDisposable field/property should be Disposed in a TearDown method

        #endregion //Fields

        #region Setup

        [SetUp]
		public void Setup()
		{
			_layout = new AbsoluteLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void AbsoluteLayoutTests_Default()
		{
			_layout.Position = new Point(10, 20);
			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(0);
			_layout.Rect.Height.ShouldBe(0);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
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

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(0);
			_layout.Rect.Height.ShouldBe(0);
			shim.Rect.X.ShouldBe(110);
			shim.Rect.Y.ShouldBe(220);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim.Rect.X.ShouldBe(110);
			shim.Rect.Y.ShouldBe(220);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim.Rect.X.ShouldBe(110);
			shim.Rect.Y.ShouldBe(220);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim.Rect.X.ShouldBe(110);
			shim.Rect.Y.ShouldBe(220);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim.Rect.X.ShouldBe(110);
			shim.Rect.Y.ShouldBe(220);
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

			shim1.Rect.X.ShouldBe(110);
			shim1.Rect.Y.ShouldBe(220);
			shim2.Rect.X.ShouldBe(110);
			shim2.Rect.Y.ShouldBe(220);
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

			shim1.Rect.X.ShouldBe(110);
			shim1.Rect.Y.ShouldBe(220);
			shim2.Rect.X.ShouldBe(110);
			shim2.Rect.Y.ShouldBe(220);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim1.Rect.X.ShouldBe(110);
			shim1.Rect.Y.ShouldBe(220);
			shim2.Rect.X.ShouldBe(110);
			shim2.Rect.Y.ShouldBe(220);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim1.Rect.X.ShouldBe(110);
			shim1.Rect.Y.ShouldBe(220);
			shim2.Rect.X.ShouldBe(110);
			shim2.Rect.Y.ShouldBe(220);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim1.Rect.X.ShouldBe(110);
			shim1.Rect.Y.ShouldBe(220);
			shim2.Rect.X.ShouldBe(110);
			shim2.Rect.Y.ShouldBe(220);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			shim1.Rect.X.ShouldBe(110);
			shim1.Rect.Y.ShouldBe(220);
			shim2.Rect.X.ShouldBe(160);
			shim2.Rect.Y.ShouldBe(260);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(300);
			_layout.Rect.Height.ShouldBe(400);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);

			shim.Rect.X.ShouldBe(110);
			shim.Rect.Y.ShouldBe(220);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			_layout.Rect.X.ShouldBe(1000);
			_layout.Rect.Y.ShouldBe(2000);
			_layout.Rect.Width.ShouldBe(100);
			_layout.Rect.Height.ShouldBe(200);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);

			shim.Position.X.ShouldBe(1100);
			shim.Position.Y.ShouldBe(2200);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			_layout.Rect.X.ShouldBe(-90);
			_layout.Rect.Y.ShouldBe(20);
			_layout.Rect.Width.ShouldBe(100);
			_layout.Rect.Height.ShouldBe(200);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Right);

			shim.Position.X.ShouldBe(10);
			shim.Position.Y.ShouldBe(220);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(-180);
			_layout.Rect.Width.ShouldBe(100);
			_layout.Rect.Height.ShouldBe(200);
			_layout.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);

			shim.Position.X.ShouldBe(110);
			shim.Position.Y.ShouldBe(20);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
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

			_layout.Rect.X.ShouldBe(10);
			_layout.Rect.Y.ShouldBe(-380);
			_layout.Rect.Width.ShouldBe(100);
			_layout.Rect.Height.ShouldBe(400);
			_layout.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);

			shim.Position.X.ShouldBe(110);
			shim.Position.Y.ShouldBe(-180);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //ChangeProperties
	}
}
