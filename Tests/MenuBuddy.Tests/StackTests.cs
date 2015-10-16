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
			DefaultStyles.InitUnitTests();
			_stack = new StackLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void Stack_Default()
		{
			_stack.Position = new Point(10, 20);
			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(0, _stack.Rect.Width);
			Assert.AreEqual(0, _stack.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
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

			Assert.AreEqual(-5, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(30, _stack.Rect.Width);
			Assert.AreEqual(40, _stack.Rect.Height);
			Assert.AreEqual(-5, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Center, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
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

			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(30, _stack.Rect.Width);
			Assert.AreEqual(40, _stack.Rect.Height);
			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
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

			Assert.AreEqual(-20, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(30, _stack.Rect.Width);
			Assert.AreEqual(40, _stack.Rect.Height);
			Assert.AreEqual(-20, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Right, shim.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
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

			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(30, _stack.Rect.Width);
			Assert.AreEqual(40, _stack.Rect.Height);
			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
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

			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(30, _stack.Rect.Width);
			Assert.AreEqual(40, _stack.Rect.Height);
			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(VerticalAlignment.Top, shim.Vertical);
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

			Assert.AreEqual(-15, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(50, _stack.Rect.Width);
			Assert.AreEqual(100, _stack.Rect.Height);
			Assert.AreEqual(-5, shim1.Rect.X);
			Assert.AreEqual(20, shim1.Rect.Y);
			Assert.AreEqual(-15, shim2.Rect.X);
			Assert.AreEqual(60, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Center, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
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

			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(50, _stack.Rect.Width);
			Assert.AreEqual(100, _stack.Rect.Height);
			Assert.AreEqual(10, shim1.Rect.X);
			Assert.AreEqual(20, shim1.Rect.Y);
			Assert.AreEqual(10, shim2.Rect.X);
			Assert.AreEqual(60, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
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

			Assert.AreEqual(-40, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(50, _stack.Rect.Width);
			Assert.AreEqual(100, _stack.Rect.Height);
			Assert.AreEqual(-20, shim1.Rect.X);
			Assert.AreEqual(20, shim1.Rect.Y);
			Assert.AreEqual(-40, shim2.Rect.X);
			Assert.AreEqual(60, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Right, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
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

			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(50, _stack.Rect.Width);
			Assert.AreEqual(100, _stack.Rect.Height);
			Assert.AreEqual(10, shim1.Rect.X);
			Assert.AreEqual(20, shim1.Rect.Y);
			Assert.AreEqual(10, shim2.Rect.X);
			Assert.AreEqual(60, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
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

			Assert.AreEqual(10, _stack.Rect.X);
			Assert.AreEqual(20, _stack.Rect.Y);
			Assert.AreEqual(50, _stack.Rect.Width);
			Assert.AreEqual(100, _stack.Rect.Height);
			Assert.AreEqual(10, shim1.Rect.X);
			Assert.AreEqual(20, shim1.Rect.Y);
			Assert.AreEqual(10, shim2.Rect.X);
			Assert.AreEqual(60, shim2.Rect.Y);
			Assert.AreEqual(HorizontalAlignment.Left, shim1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, shim1.Vertical);
		}

		#endregion //OneItem
	}
}
