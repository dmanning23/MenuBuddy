using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using ResolutionBuddy;
using Shouldly;

namespace MenuBuddy.Tests
{
	public class TestDropdownTarget
	{
		public string Text { get; set; }
		public TestDropdownTarget(string text) { Text = text; }
	}

	public class DropdownTests2
	{
		#region Fields

		private Dropdown<TestDropdownTarget> _drop;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			var resolution = new Mock<IResolution>();
			resolution.Setup(x => x.ScreenArea).Returns(new Rectangle(0, 0, 1280, 720));
			Resolution.Init(resolution.Object);

			var screen = new WidgetScreen("test screen");
			_drop = new Dropdown<TestDropdownTarget>(screen);
		}

		#endregion //Setup

		#region Tests
		
		[Test]
		public void select_item()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var target = new TestDropdownTarget("catpants");

			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = target;

			_drop.SelectedDropdownItem.ShouldNotBeNull();
			_drop.SelectedItem.ShouldNotBeNull();
			_drop.SelectedItem.Text.ShouldBe("catpants");
		}

		[Test]
		public void select_defaultitem()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var target = new TestDropdownTarget("catpants");

			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedDropdownItem.ShouldBeNull();
			_drop.SelectedItem.ShouldBeNull();
		}

		[Test]
		public void select_item2()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var target = new TestDropdownTarget("catpants");
			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target, _drop) {
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			var target2 = new TestDropdownTarget("buttnuts");
			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target2, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = target2;

			_drop.SelectedDropdownItem.ShouldNotBeNull();
			_drop.SelectedItem.ShouldNotBeNull();
			_drop.SelectedItem.Text.ShouldBe("buttnuts");
		}

		[Test]
		public void select_null()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var target = new TestDropdownTarget("catpants");
			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			var target2 = new TestDropdownTarget("buttnuts");
			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target2, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = target2;
			_drop.SelectedItem = null;

			_drop.SelectedDropdownItem.ShouldBeNull();
			_drop.SelectedItem.ShouldBeNull();
		}

		[Test]
		public void add_null()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var target = new TestDropdownTarget("catpants");
			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(null, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = null;

			_drop.SelectedDropdownItem.ShouldBeNull();
			_drop.SelectedItem.ShouldBeNull();
		}

		[Test]
		public void select_not_null()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var target = new TestDropdownTarget("catpants");
			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(target, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.AddDropdownItem(new DropdownItem<TestDropdownTarget>(null, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = target;

			_drop.SelectedDropdownItem.ShouldNotBeNull();
			_drop.SelectedItem.ShouldNotBeNull();
			_drop.SelectedItem.Text.ShouldBe("catpants");
		}

		#endregion //Tests
	}
}
