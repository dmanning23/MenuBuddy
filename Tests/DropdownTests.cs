using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using ResolutionBuddy;

namespace MenuBuddy.Tests
{
	public class DropdownTests
	{
		#region Fields

		private Dropdown<string> _drop;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			var resolution = new Mock<IResolution>();
			resolution.Setup(x => x.ScreenArea).Returns(new Rectangle(0, 0, 1280, 720));
			Resolution.Init(resolution.Object);

			var screen = new WidgetScreen("test screen");
			_drop = new Dropdown<string>(screen);
		}

		#endregion //Setup

		#region Default

		[Test]
		public void DropdownTests_Default()
		{
			_drop.Position = new Point(10, 20);
			_drop.Rect.X.ShouldBe(10);
			_drop.Rect.Y.ShouldBe(20);
			_drop.Rect.Width.ShouldBe(0);
			_drop.Rect.Height.ShouldBe(0);
			_drop.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_drop.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void DropdownTests_SetSize()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			_drop.Rect.X.ShouldBe(10);
			_drop.Rect.Y.ShouldBe(20);
			_drop.Rect.Width.ShouldBe(30);
			_drop.Rect.Height.ShouldBe(40);
			_drop.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_drop.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Default

		#region Tests

		[Test]
		public void DropdownTests_AddDropdownItem()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var dropitem = new DropdownItem<string>("catpants", _drop);
			dropitem.Vertical = VerticalAlignment.Top;
			dropitem.Horizontal = HorizontalAlignment.Left;
			dropitem.Size = new Vector2(30, 40);
			_drop.AddDropdownItem(dropitem);

			_drop.SelectedItem = "catpants";

			dropitem.Rect.X.ShouldBe(10);
			dropitem.Rect.Y.ShouldBe(20);
			dropitem.Rect.Width.ShouldBe(30);
			dropitem.Rect.Height.ShouldBe(40);
			dropitem.Horizontal.ShouldBe(HorizontalAlignment.Left);
			dropitem.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void DropdownTests_AddShim()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var dropitem = new DropdownItem<string>("catpants", _drop);
			dropitem.Vertical = VerticalAlignment.Top;
			dropitem.Horizontal = HorizontalAlignment.Left;
			dropitem.Size = new Vector2(30, 40);

			var item = new Shim()
			{
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Size = new Vector2(30, 40)
			};
			dropitem.AddItem(item);

			_drop.AddDropdownItem(dropitem);
			_drop.SelectedItem = "catpants";

			item.Rect.X.ShouldBe(10);
			item.Rect.Y.ShouldBe(20);
			item.Rect.Width.ShouldBe(30);
			item.Rect.Height.ShouldBe(40);
			item.Horizontal.ShouldBe(HorizontalAlignment.Left);
			item.Vertical.ShouldBe(VerticalAlignment.Top);
		}
		
		[Test]
		public void DropdownTests_Style()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var dropitem = new DropdownItem<string>("catpants", _drop);
			dropitem.Vertical = VerticalAlignment.Top;
			dropitem.Horizontal = HorizontalAlignment.Left;
			dropitem.Size = new Vector2(30, 40);
			_drop.AddDropdownItem(dropitem);

			_drop.SelectedItem = "catpants";

			dropitem.HasBackground.ShouldBe(false);
			dropitem.HasOutline.ShouldBe(false);
		}
		
		[Test]
		public void select_item()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			_drop.AddDropdownItem(new DropdownItem<string>("catpants", _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = "catpants";

			_drop.SelectedItem.ShouldBe("catpants");
		}

		[Test]
		public void select_defaultitem()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			_drop.AddDropdownItem(new DropdownItem<string>("catpants", _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			string.IsNullOrEmpty(_drop.SelectedItem).ShouldBeTrue();
		}

		[Test]
		public void select_item2()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			_drop.AddDropdownItem(new DropdownItem<string>("catpants", _drop) {
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.AddDropdownItem(new DropdownItem<string>("buttnuts", _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = "buttnuts";

			_drop.SelectedItem.ShouldBe("buttnuts");
		}

		[Test]
		public void select_null()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			_drop.AddDropdownItem(new DropdownItem<string>("catpants", _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.AddDropdownItem(new DropdownItem<string>("buttnuts", _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = null;

			string.IsNullOrEmpty(_drop.SelectedItem).ShouldBeTrue();
		}

		[Test]
		public void add_null()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			_drop.AddDropdownItem(new DropdownItem<string>("catpants", _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.AddDropdownItem(new DropdownItem<string>(null, _drop)
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Left,
				Size = new Vector2(30, 40)
			});

			_drop.SelectedItem = null;

			string.IsNullOrEmpty(_drop.SelectedItem).ShouldBeTrue();
		}

		#endregion //Tests
	}
}
