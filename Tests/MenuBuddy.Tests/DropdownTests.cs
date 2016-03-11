using Microsoft.Xna.Framework;
using NUnit.Framework;

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
			DefaultStyles.InitUnitTests();
			_drop = new Dropdown<string>();
		}

		#endregion //Setup

		#region Default

		[Test]
		public void DropdownTests_Default()
		{
			_drop.Position = new Point(10, 20);
			Assert.AreEqual(10, _drop.Rect.X);
			Assert.AreEqual(20, _drop.Rect.Y);
			Assert.AreEqual(0, _drop.Rect.Width);
			Assert.AreEqual(0, _drop.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _drop.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _drop.Vertical);
		}

		[Test]
		public void DropdownTests_SetSize()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _drop.Rect.X);
			Assert.AreEqual(20, _drop.Rect.Y);
			Assert.AreEqual(30, _drop.Rect.Width);
			Assert.AreEqual(40, _drop.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _drop.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _drop.Vertical);
		}

		#endregion //Default

		#region AddItem

		[Test]
		public void DropdownTests_AddDropdownItem()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var dropitem = new DropdownItem<string>("catpants", _drop);
			dropitem.Vertical = VerticalAlignment.Top;
			dropitem.Horizontal = HorizontalAlignment.Left;
			dropitem.Size = new Vector2(30, 40);
			_drop.DropdownList.Add(dropitem);

			_drop.SelectedItem = "catpants";

			Assert.AreEqual(10, dropitem.Rect.X);
			Assert.AreEqual(20, dropitem.Rect.Y);
			Assert.AreEqual(30, dropitem.Rect.Width);
			Assert.AreEqual(40, dropitem.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, dropitem.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, dropitem.Vertical);
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

			_drop.DropdownList.Add(dropitem);
			_drop.SelectedItem = "catpants";

			Assert.AreEqual(10, item.Rect.X);
			Assert.AreEqual(20, item.Rect.Y);
			Assert.AreEqual(30, item.Rect.Width);
			Assert.AreEqual(40, item.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, item.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, item.Vertical);
		}

		#endregion //AddItem

		#region AddItem

		[Test]
		public void DropdownTests_Style()
		{
			_drop.Position = new Point(10, 20);
			_drop.Size = new Vector2(30, 40);

			var dropitem = new DropdownItem<string>("catpants", _drop);
			dropitem.Vertical = VerticalAlignment.Top;
			dropitem.Horizontal = HorizontalAlignment.Left;
			dropitem.Size = new Vector2(30, 40);
			_drop.DropdownList.Add(dropitem);

			_drop.SelectedItem = "catpants";

			Assert.AreEqual(false, dropitem.Style.HasBackground);
			Assert.AreEqual(false, dropitem.Style.HasOutline);
		}

		#endregion //AddItem
	}
}
