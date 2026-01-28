using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class StackTests
	{
		#region Fields

		private StackLayout _stack;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			_stack = new StackLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void Stack_Default()
		{
			_stack.Position = new Point(10, 20);
			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(0);
			_stack.Rect.Height.ShouldBe(0);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_stack.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Layout_HasOutline()
		{
			_stack.HasOutline.ShouldBeFalse();
		}

		[Test]
		public void Layout_Copy_HasOutline()
		{
			_stack.HasOutline = true;
			var stack = new StackLayout(_stack);
			stack.HasOutline.ShouldBeTrue();
		}

		#endregion //Defaults

		#region OneItem

		[Test]
		public void Stack_AddItem_default()
		{
			_stack.Position = new Point(10, 20);

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
            _stack.AddItem(shim);

			_stack.Rect.X.ShouldBe(-5);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(40);
			shim.Rect.X.ShouldBe(-5);
			shim.Rect.Y.ShouldBe(20);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Center);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_AddItem_LeftHorizontal()
		{
			_stack.Position = new Point(10, 20);
			_stack.Horizontal = HorizontalAlignment.Left;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim);

			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(40);
			shim.Rect.X.ShouldBe(10);
			shim.Rect.Y.ShouldBe(20);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_AddItem_RightHorizontal()
		{
			_stack.Position = new Point(10, 20);
			_stack.Horizontal = HorizontalAlignment.Right;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim);

			_stack.Rect.X.ShouldBe(-20);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(40);
			shim.Rect.X.ShouldBe(-20);
			shim.Rect.Y.ShouldBe(20);
			shim.Horizontal.ShouldBe(HorizontalAlignment.Right);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_AddItem_CenterVertical()
		{
			_stack.Position = new Point(10, 20);
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Vertical = VerticalAlignment.Center;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim);

			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(40);
			shim.Rect.X.ShouldBe(10);
			shim.Rect.Y.ShouldBe(20);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_AddItem_BottomVertical()
		{
			_stack.Position = new Point(10, 20);
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Vertical = VerticalAlignment.Bottom;

			var shim = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim);

			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(40);
			shim.Rect.X.ShouldBe(10);
			shim.Rect.Y.ShouldBe(20);
			shim.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //OneItem

		#region TwoItem

		[Test]
		public void Stack_TwoItem_default()
		{
			_stack.Position = new Point(10, 20);

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_stack.AddItem(shim2);

			_stack.Rect.X.ShouldBe(-15);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(50);
			_stack.Rect.Height.ShouldBe(100);
			shim1.Rect.X.ShouldBe(-5);
			shim1.Rect.Y.ShouldBe(20);
			shim2.Rect.X.ShouldBe(-15);
			shim2.Rect.Y.ShouldBe(60);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Center);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_TwoItem_LeftHorizontal()
		{
			_stack.Position = new Point(10, 20);
			_stack.Horizontal = HorizontalAlignment.Left;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_stack.AddItem(shim2);

			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(50);
			_stack.Rect.Height.ShouldBe(100);
			shim1.Rect.X.ShouldBe(10);
			shim1.Rect.Y.ShouldBe(20);
			shim2.Rect.X.ShouldBe(10);
			shim2.Rect.Y.ShouldBe(60);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_TwoItem_RightHorizontal()
		{
			_stack.Position = new Point(10, 20);
			_stack.Horizontal = HorizontalAlignment.Right;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_stack.AddItem(shim2);

			_stack.Rect.X.ShouldBe(-40);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(50);
			_stack.Rect.Height.ShouldBe(100);
			shim1.Rect.X.ShouldBe(-20);
			shim1.Rect.Y.ShouldBe(20);
			shim2.Rect.X.ShouldBe(-40);
			shim2.Rect.Y.ShouldBe(60);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Right);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_TwoItem_CenterVertical()
		{
			_stack.Position = new Point(10, 20);
			_stack.Alignment = StackAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Vertical = VerticalAlignment.Center;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_stack.AddItem(shim2);

			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(50);
			_stack.Rect.Height.ShouldBe(100);
			shim1.Rect.X.ShouldBe(10);
			shim1.Rect.Y.ShouldBe(20);
			shim2.Rect.X.ShouldBe(10);
			shim2.Rect.Y.ShouldBe(60);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_TwoItem_BottomVertical()
		{
			_stack.Position = new Point(10, 20);
			_stack.Alignment = StackAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Vertical = VerticalAlignment.Bottom;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_stack.AddItem(shim2);

			_stack.Rect.X.ShouldBe(10);
			_stack.Rect.Y.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(50);
			_stack.Rect.Height.ShouldBe(100);
			shim1.Rect.X.ShouldBe(10);
			shim1.Rect.Y.ShouldBe(20);
			shim2.Rect.X.ShouldBe(10);
			shim2.Rect.Y.ShouldBe(60);
			shim1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			shim1.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Stack_TwoItem_Layers()
		{
			_stack.Position = new Point(10, 20);
			_stack.Alignment = StackAlignment.Top;
			_stack.Horizontal = HorizontalAlignment.Left;
			_stack.Vertical = VerticalAlignment.Bottom;

			var shim1 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(30, 40)
			};
			_stack.AddItem(shim1);
			var shim2 = new Shim()
			{
				Position = new Point(100, 200),
				Size = new Vector2(50, 60)
			};
			_stack.AddItem(shim2);
			
			shim1.Layer.ShouldBe(1);
			shim2.Layer.ShouldBe(2);
		}

		#endregion //OneItem
	}
}
