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

		private Mock<IScreen> _screen;

		private IFontBuddy _largeFont;
		private IFontBuddy _mediumFont;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void LabelStack_Setup()
		{
			_stack = new StackLayout();

			var font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(70f, 80f));
			_largeFont = font.Object;

			font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			_mediumFont = font.Object;

			_screen = new Mock<IScreen>();
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

			_stack.Position.X.ShouldBe(10);
			_stack.Position.Y.ShouldBe(20);
			_stack.Rect.Left.ShouldBe(10);
			_stack.Rect.Bottom.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(0);
			_stack.Rect.Height.ShouldBe(0);
			_stack.Alignment.ShouldBe(StackAlignment.Bottom);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_stack.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_stack.Items.Count.ShouldBe(0);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			_stack.Position.X.ShouldBe(10);
			_stack.Position.Y.ShouldBe(20);
			_stack.Rect.Left.ShouldBe(10);
			_stack.Rect.Bottom.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(40);
			_stack.Alignment.ShouldBe(StackAlignment.Bottom);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_stack.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_stack.Items.Count.ShouldBe(1);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			label1.Position.X.ShouldBe(10);
			label1.Position.Y.ShouldBe(20);
			label1.Rect.Left.ShouldBe(10);
			label1.Rect.Bottom.ShouldBe(20);
			label1.Rect.Width.ShouldBe(30);
			label1.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			_stack.Position.X.ShouldBe(10);
			_stack.Position.Y.ShouldBe(20);
			_stack.Rect.Left.ShouldBe(10);
			_stack.Rect.Bottom.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(80);
			_stack.Alignment.ShouldBe(StackAlignment.Bottom);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_stack.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_stack.Items.Count.ShouldBe(2);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label1.Position.X.ShouldBe(10);
			label1.Position.Y.ShouldBe(20);
			label1.Rect.Left.ShouldBe(10);
			label1.Rect.Bottom.ShouldBe(20);
			label1.Rect.Width.ShouldBe(30);
			label1.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label2.Position.X.ShouldBe(10);
			label2.Position.Y.ShouldBe(-20);
			label2.Rect.Left.ShouldBe(10);
			label2.Rect.Bottom.ShouldBe(-20);
			label2.Rect.Width.ShouldBe(30);
			label2.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			_stack.Position.X.ShouldBe(10);
			_stack.Position.Y.ShouldBe(20);
			_stack.Rect.Left.ShouldBe(10);
			_stack.Rect.Bottom.ShouldBe(100);
			_stack.Rect.Width.ShouldBe(30);
			_stack.Rect.Height.ShouldBe(80);
			_stack.Alignment.ShouldBe(StackAlignment.Top);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_stack.Vertical.ShouldBe(VerticalAlignment.Top);
			_stack.Items.Count.ShouldBe(2);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label1.Position.X.ShouldBe(10);
			label1.Position.Y.ShouldBe(20);
			label1.Rect.Left.ShouldBe(10);
			label1.Rect.Bottom.ShouldBe(60);
			label1.Rect.Width.ShouldBe(30);
			label1.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label2.Position.X.ShouldBe(10);
			label2.Position.Y.ShouldBe(60);
			label2.Rect.Left.ShouldBe(10);
			label2.Rect.Bottom.ShouldBe(100);
			label2.Rect.Width.ShouldBe(30);
			label2.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Top);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			_stack.Position.X.ShouldBe(10);
			_stack.Position.Y.ShouldBe(20);
			_stack.Rect.Left.ShouldBe(10);
			_stack.Rect.Right.ShouldBe(70);
			_stack.Rect.Bottom.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(60);
			_stack.Rect.Height.ShouldBe(40);
			_stack.Alignment.ShouldBe(StackAlignment.Left);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_stack.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_stack.Items.Count.ShouldBe(2);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label1.Position.X.ShouldBe(10);
			label1.Position.Y.ShouldBe(20);
			label1.Rect.Left.ShouldBe(10);
			label1.Rect.Bottom.ShouldBe(20);
			label1.Rect.Width.ShouldBe(30);
			label1.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label2.Position.X.ShouldBe(40);
			label2.Position.Y.ShouldBe(20);
			label2.Rect.Left.ShouldBe(40);
			label2.Rect.Bottom.ShouldBe(20);
			label2.Rect.Width.ShouldBe(30);
			label2.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Left);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			_stack.Position.X.ShouldBe(10);
			_stack.Position.Y.ShouldBe(20);
			_stack.Rect.Left.ShouldBe(-50);
			_stack.Rect.Bottom.ShouldBe(20);
			_stack.Rect.Width.ShouldBe(60);
			_stack.Rect.Height.ShouldBe(40);
			_stack.Alignment.ShouldBe(StackAlignment.Right);
			_stack.Horizontal.ShouldBe(HorizontalAlignment.Right);
			_stack.Vertical.ShouldBe(VerticalAlignment.Bottom);
			_stack.Items.Count.ShouldBe(2);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label1.Position.X.ShouldBe(10);
			label1.Position.Y.ShouldBe(20);
			label1.Rect.Left.ShouldBe(-20);
			label1.Rect.Bottom.ShouldBe(20);
			label1.Rect.Width.ShouldBe(30);
			label1.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Right);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
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

			var label1 = new Label("butt", _mediumFont);
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont);
			_stack.AddItem(label2);

			label2.Position.X.ShouldBe(-20);
			label2.Position.Y.ShouldBe(20);
			label2.Rect.Left.ShouldBe(-50);
			label2.Rect.Bottom.ShouldBe(20);
			label2.Rect.Width.ShouldBe(30);
			label2.Rect.Height.ShouldBe(40);
			label1.Horizontal.ShouldBe(HorizontalAlignment.Right);
			label1.Vertical.ShouldBe(VerticalAlignment.Bottom);
		}

		#endregion //TwoItems_RightAlignment

		#region Scale

		[Test]
		public void Label_Scale()
		{
			var label1 = new Label("butt", _mediumFont)
			{
				Scale = 0.5f
			};

			label1.Scale.ShouldBe(.5f);
		}

		[Test]
		public void Label1Stack_Scale()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Right,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt", _mediumFont)
			{
				Scale = 0.5f
			};
			_stack.AddItem(label1);

			label1.Scale = .5f;

			label1.Scale.ShouldBe(.5f);
		}

		[Test]
		public void LabelStack_Scale()
		{
			_stack = new StackLayout()
			{
				Alignment = StackAlignment.Right,
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Bottom,
				Position = new Point(10, 20)
			};

			var label1 = new Label("butt", _mediumFont)
			{
				Scale = 0.5f
			};
			_stack.AddItem(label1);

			var label2 = new Label("nuts", _mediumFont)
			{
				Scale = 0.25f
			};
			_stack.AddItem(label2);

			label1.Scale = .5f;
			label2.Scale = .25f;

			label1.Scale.ShouldBe(.5f);
			label2.Scale.ShouldBe(.25f);
		}

		#endregion //Scale
	}
}
