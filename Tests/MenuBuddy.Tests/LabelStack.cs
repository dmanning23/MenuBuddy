using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FontBuddyLib;
using Microsoft.Xna.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class LabelStack
	{
		#region Fields

		private StackLayout _stack;

		private Mock<IFontBuddy> _font;
		private Mock<IScreen> _screen;
		private ILabel _label;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void LabelStack_Setup()
		{
			DefaultStyles.InitUnitTests();

			_stack = new StackLayout();

			var menuStyles = new StyleSheet();
			DefaultStyles.Instance().MainStyle = menuStyles;

			_font = new Mock<IFontBuddy>() { CallBase = true };
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = _font.Object;

			_screen = new Mock<IScreen>();

			_label = new Label("test");
		}

		#endregion //Setup

		#region Default

		[Test]
		public void LabelStack_NoItems()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Bottom,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			Assert.AreEqual(10f, _stack.Position.X);
			Assert.AreEqual(20f, _stack.Position.Y);
			Assert.AreEqual(10f, _stack.Rect.Left);
			Assert.AreEqual(20f, _stack.Rect.Bottom);
			Assert.AreEqual(0f, _stack.Rect.Width);
			Assert.AreEqual(0f, _stack.Rect.Height);
			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
			Assert.AreEqual(0, _stack.Items.Count);
		}

		#endregion //Default

		#region OneItem

		[Test]
		public void LabelStack_OneItem_Stack()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Bottom,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			Assert.AreEqual(10f, _stack.Position.X);
			Assert.AreEqual(20f, _stack.Position.Y);
			Assert.AreEqual(10f, _stack.Rect.Left);
			Assert.AreEqual(20f, _stack.Rect.Bottom);
			Assert.AreEqual(30f, _stack.Rect.Width);
			Assert.AreEqual(40f, _stack.Rect.Height);
			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
			Assert.AreEqual(1, _stack.Items.Count);
		}

		[Test]
		public void LabelStack_OneItem_Label1()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Bottom,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			Assert.AreEqual(10f, label1.Position.X);
			Assert.AreEqual(20f, label1.Position.Y);
			Assert.AreEqual(10f, label1.Rect.Left);
			Assert.AreEqual(20f, label1.Rect.Bottom);
			Assert.AreEqual(30f, label1.Rect.Width);
			Assert.AreEqual(40f, label1.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		#endregion //OneItem

		#region TwoItems_BottomAlignment

		[Test]
		public void LabelStack_TwoItems_BottomAlignment_Stack()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Bottom,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, _stack.Position.X);
			Assert.AreEqual(20f, _stack.Position.Y);
			Assert.AreEqual(10f, _stack.Rect.Left);
			Assert.AreEqual(20f, _stack.Rect.Bottom);
			Assert.AreEqual(30f, _stack.Rect.Width);
			Assert.AreEqual(80f, _stack.Rect.Height);
			Assert.AreEqual(StackAlignment.Bottom, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
			Assert.AreEqual(2, _stack.Items.Count);
		}

		[Test]
		public void LabelStack_TwoItems_BottomAlignment_Label1()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Bottom,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, label1.Position.X);
			Assert.AreEqual(20f, label1.Position.Y);
			Assert.AreEqual(10f, label1.Rect.Left);
			Assert.AreEqual(20f, label1.Rect.Bottom);
			Assert.AreEqual(30f, label1.Rect.Width);
			Assert.AreEqual(40f, label1.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		[Test]
		public void LabelStack_TwoItems_BottomAlignment_Label2()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Bottom,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, label2.Position.X);
			Assert.AreEqual(-20f, label2.Position.Y);
			Assert.AreEqual(10f, label2.Rect.Left);
			Assert.AreEqual(-20f, label2.Rect.Bottom);
			Assert.AreEqual(30f, label2.Rect.Width);
			Assert.AreEqual(40f, label2.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		#endregion //TwoItems_BottomAlignment

		#region TwoItems_TopAlignment

		[Test]
		public void LabelStack_TwoItems_TopAlignment_Stack()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, _stack.Position.X);
			Assert.AreEqual(20f, _stack.Position.Y);
			Assert.AreEqual(10f, _stack.Rect.Left);
			Assert.AreEqual(100f, _stack.Rect.Bottom);
			Assert.AreEqual(30f, _stack.Rect.Width);
			Assert.AreEqual(80f, _stack.Rect.Height);
			Assert.AreEqual(StackAlignment.Top, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _stack.Vertical);
			Assert.AreEqual(2, _stack.Items.Count);
		}

		[Test]
		public void LabelStack_TwoItems_TopAlignment_Label1()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, label1.Position.X);
			Assert.AreEqual(20f, label1.Position.Y);
			Assert.AreEqual(10f, label1.Rect.Left);
			Assert.AreEqual(60f, label1.Rect.Bottom);
			Assert.AreEqual(30f, label1.Rect.Width);
			Assert.AreEqual(40f, label1.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, label1.Vertical);
		}

		[Test]
		public void LabelStack_TwoItems_TopAlignment_Label2()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, label2.Position.X);
			Assert.AreEqual(60f, label2.Position.Y);
			Assert.AreEqual(10f, label2.Rect.Left);
			Assert.AreEqual(100f, label2.Rect.Bottom);
			Assert.AreEqual(30f, label2.Rect.Width);
			Assert.AreEqual(40f, label2.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, label1.Vertical);
		}

		#endregion //TwoItems_TopAlignment

		#region TwoItems_LeftAlignment

		[Test]
		public void LabelStack_TwoItems_LeftAlignment_Stack()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Left,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, _stack.Position.X);
			Assert.AreEqual(20f, _stack.Position.Y);
			Assert.AreEqual(10f, _stack.Rect.Left);
			Assert.AreEqual(70f, _stack.Rect.Right);
			Assert.AreEqual(20f, _stack.Rect.Bottom);
			Assert.AreEqual(60f, _stack.Rect.Width);
			Assert.AreEqual(40f, _stack.Rect.Height);
			Assert.AreEqual(StackAlignment.Left, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Left, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
			Assert.AreEqual(2, _stack.Items.Count);
		}

		[Test]
		public void LabelStack_TwoItems_LeftAlignment_Label1()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Left,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, label1.Position.X);
			Assert.AreEqual(20f, label1.Position.Y);
			Assert.AreEqual(10f, label1.Rect.Left);
			Assert.AreEqual(20f, label1.Rect.Bottom);
			Assert.AreEqual(30f, label1.Rect.Width);
			Assert.AreEqual(40f, label1.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		[Test]
		public void LabelStack_TwoItems_LeftAlignment_Label2()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Left,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(40f, label2.Position.X);
			Assert.AreEqual(20f, label2.Position.Y);
			Assert.AreEqual(40f, label2.Rect.Left);
			Assert.AreEqual(20f, label2.Rect.Bottom);
			Assert.AreEqual(30f, label2.Rect.Width);
			Assert.AreEqual(40f, label2.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		#endregion //TwoItems_LeftAlignment

		#region TwoItems_RightAlignment

		[Test]
		public void LabelStack_TwoItems_RightAlignment_Stack()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Right,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, _stack.Position.X);
			Assert.AreEqual(20f, _stack.Position.Y);
			Assert.AreEqual(-50f, _stack.Rect.Left);
			Assert.AreEqual(20f, _stack.Rect.Bottom);
			Assert.AreEqual(60f, _stack.Rect.Width);
			Assert.AreEqual(40f, _stack.Rect.Height);
			Assert.AreEqual(StackAlignment.Right, _stack.Alignment);
			Assert.AreEqual(HorizontalAlignment.Right, _stack.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, _stack.Vertical);
			Assert.AreEqual(2, _stack.Items.Count);
		}

		[Test]
		public void LabelStack_TwoItems_RightAlignment_Label1()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Right,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(10f, label1.Position.X);
			Assert.AreEqual(20f, label1.Position.Y);
			Assert.AreEqual(-20f, label1.Rect.Left);
			Assert.AreEqual(20f, label1.Rect.Bottom);
			Assert.AreEqual(30f, label1.Rect.Width);
			Assert.AreEqual(40f, label1.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		[Test]
		public void LabelStack_TwoItems_RightAlignment_Label2()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Right,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts")
			{
				Style = DefaultStyles.Instance().MainStyle,
			};
			_stack.AddItem(label2);

			Assert.AreEqual(-20f, label2.Position.X);
			Assert.AreEqual(20f, label2.Position.Y);
			Assert.AreEqual(-50f, label2.Rect.Left);
			Assert.AreEqual(20f, label2.Rect.Bottom);
			Assert.AreEqual(30f, label2.Rect.Width);
			Assert.AreEqual(40f, label2.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Right, label1.Horizontal);
			Assert.AreEqual(VerticalAlignment.Bottom, label1.Vertical);
		}

		#endregion //TwoItems_RightAlignment
	}
}
