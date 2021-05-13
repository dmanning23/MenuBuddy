using InputHelper;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class TreeTests
	{
		#region Fields

		private Tree<string> _tree;

		#endregion //Fields

		#region Setup

		private TreeItem<string> AddItem()
		{
			var cat = new TreeItem<string>("test", _tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			
			_tree.AddItem(cat);

			return cat;
		}

		private TreeItem<string> AddSubItem(TreeItem<string> item)
		{
			var sub = new TreeItem<string>("test", _tree, item);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			item.ChildItems.Add(sub);

			return sub;
		}

		[SetUp]
		public async Task Setup()
		{
			var screen = new WidgetScreen("Test");
			_tree = new Tree<string>(screen);
			await _tree.LoadContent(screen);
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void Tree_Default()
		{
			Assert.AreEqual(0, _tree.Rect.X);
			Assert.AreEqual(0, _tree.Rect.Y);
			Assert.AreEqual(0, _tree.Rect.Width);
			Assert.AreEqual(0, _tree.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _tree.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _tree.Vertical);
		}

		[Test]
		public void Tree_Default2()
		{
			_tree.Position = new Point(10, 20);
			Assert.AreEqual(10, _tree.Rect.X);
			Assert.AreEqual(20, _tree.Rect.Y);
			Assert.AreEqual(0, _tree.Rect.Width);
			Assert.AreEqual(0, _tree.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _tree.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _tree.Vertical);
		}

		#endregion //Defaults

		#region unexpanded tests

		[Test]
		public void unexpanded_oneitem()
		{
			//create the top item
			var cat = AddItem();

			Assert.AreEqual(cat.Rect.Width, _tree.TotalRect.Right);
			Assert.AreEqual(cat.Rect.Height, _tree.TotalRect.Bottom);
			Assert.AreEqual(cat.Rect.Width, _tree.TotalRect.Width);
			Assert.AreEqual(cat.Rect.Height, _tree.TotalRect.Height);
		}

		[Test]
		public void unexpanded_twoitem()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var expectedWidth = Math.Max(cat.Rect.Width, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(expectedWidth, _tree.TotalRect.Right);
		}

		[Test]
		public void unexpanded_twoitem1()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var expectedWidth = cat.Rect.Width + cat1.Rect.Width;
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
		}

		[Test]
		public void unexpanded_twoitem2()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var expectedWidth = Math.Max(cat.Rect.Width, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
		}

		[Test]
		public void unexpanded_twoitem3()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var expectedWidth = cat.Rect.Width + cat1.Rect.Width;
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		#endregion //unexpanded tests

		#region expand tests

		[Test]
		public void expand_one()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			Assert.AreEqual(expectedWidth, _tree.TotalRect.Right);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void expand_one2()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
		}

		[Test]
		public void expand_one3()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
		}

		[Test]
		public void expand_one4()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void expand_two()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			expectedWidth = Math.Max(expectedWidth, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Right);
		}

		[Test]
		public void expand_two2()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat.Rect.Height +
				sub.Rect.Height +
				cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
		}

		[Test]
		public void expand_two3()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			expectedWidth = Math.Max(expectedWidth, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
		}

		[Test]
		public void expand_two4()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void multiple_expand_two()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height + sub1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		#endregion //multiple expand tests

		#region expand unexpand tests

		[Test]
		public void expand_unexpand()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat.Rect.Height + cat1.Rect.Height + sub1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void expand_unexpand1()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		#endregion //expand unexpand tests

		#region scroll bar tests

		[Test]
		public void scrollbar_default()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);
			

			Assert.AreEqual(_tree.MaxScroll.Y, _tree.TotalRect.Height);
		}

		[Test]
		public void scrollbar_default1()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var expectedHeight = cat.Rect.Height;

			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		[Test]
		public void scrollbar_expand()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		#endregion //unexpanded tests

		#region three items tests

		[Test]
		public void three_items()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				sub2.Rect.Height +
				cat1.Rect.Height + 
				sub1.Rect.Height +
				cat.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void three_items1()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				sub2.Rect.Height +
				cat1.Rect.Height +
				sub1.Rect.Height +
				cat.Rect.Height +
				sub.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void three_items2()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				sub2.Rect.Height +
				cat1.Rect.Height +
				//sub1.Rect.Height +
				cat.Rect.Height +
				sub.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void three_items3()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				//sub2.Rect.Height +
				cat1.Rect.Height +
				//sub1.Rect.Height +
				cat.Rect.Height +
				sub.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void three_items4()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				//sub2.Rect.Height +
				cat1.Rect.Height +
				//sub1.Rect.Height +
				cat.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		#endregion //three items tests

		#region three items scroll

		[Test]
		public void three_items_scroll()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				sub2.Rect.Height +
				cat1.Rect.Height +
				sub1.Rect.Height +
				cat.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		[Test]
		public void three_items1_scroll()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				sub2.Rect.Height +
				cat1.Rect.Height +
				sub1.Rect.Height +
				cat.Rect.Height +
				sub.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		[Test]
		public void three_items2_scroll()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				sub2.Rect.Height +
				cat1.Rect.Height +
				//sub1.Rect.Height +
				cat.Rect.Height +
				sub.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		[Test]
		public void three_items3_scroll()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				//sub2.Rect.Height +
				cat1.Rect.Height +
				//sub1.Rect.Height +
				cat.Rect.Height +
				sub.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		[Test]
		public void three_items4_scroll()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedHeight = cat2.Rect.Height +
				//sub2.Rect.Height +
				cat1.Rect.Height +
				//sub1.Rect.Height +
				cat.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.MaxScroll.Y);
		}

		#endregion //three items tests

		#region three items locations

		[Test]
		public void three_items_locations()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			Assert.AreEqual(0, cat.Position.Y);
			Assert.AreEqual(cat.Rect.Bottom, cat1.Position.Y);
			Assert.AreEqual(cat1.Rect.Bottom, sub1.Position.Y);
			Assert.AreEqual(sub1.Rect.Bottom, cat2.Position.Y);
			Assert.AreEqual(cat2.Rect.Bottom, sub2.Position.Y);
		}

		[Test]
		public void three_items1_locations()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			Assert.AreEqual(0, cat.Position.Y);
			Assert.AreEqual(cat.Rect.Bottom, sub.Position.Y);
			Assert.AreEqual(sub.Rect.Bottom, cat1.Position.Y);
			Assert.AreEqual(cat1.Rect.Bottom, sub1.Position.Y);
			Assert.AreEqual(sub1.Rect.Bottom, cat2.Position.Y);
			Assert.AreEqual(cat2.Rect.Bottom, sub2.Position.Y);
		}

		[Test]
		public void three_items2_locations()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			Assert.AreEqual(0, cat.Position.Y);
			Assert.AreEqual(cat.Rect.Bottom, sub.Position.Y);
			Assert.AreEqual(sub.Rect.Bottom, cat1.Position.Y);
			Assert.AreEqual(cat1.Rect.Bottom, cat2.Position.Y);
			Assert.AreEqual(cat2.Rect.Bottom, sub2.Position.Y);
		}

		[Test]
		public void three_items3_locations()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			Assert.AreEqual(0, cat.Position.Y);
			Assert.AreEqual(cat.Rect.Bottom, sub.Position.Y);
			Assert.AreEqual(sub.Rect.Bottom, cat1.Position.Y);
			Assert.AreEqual(cat1.Rect.Bottom, cat2.Position.Y);
		}

		[Test]
		public void three_items4_locations()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var cat1 = AddItem();
			var sub1 = AddSubItem(cat1);

			var cat2 = AddItem();
			var sub2 = AddSubItem(cat2);

			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat2.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat1.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			Assert.AreEqual(0, cat.Position.Y);
			Assert.AreEqual(cat.Rect.Bottom, cat1.Position.Y);
			Assert.AreEqual(cat1.Rect.Bottom, cat2.Position.Y);
		}

		#endregion //three items locations

		#region test screen

		[Test]
		public void tree_Test_Screen()
		{
			/*
			 string[] words = { "cat", "pants", "buttnuts", "cat1", "pants1", "whoa", "test1", "test2" };
			string[] subWords = { "sub1", "sub2", "sub3" };
			 */
			var cat = AddItem();
			var cat_sub1 = AddSubItem(cat);
			var cat_sub2 = AddSubItem(cat);
			var cat_sub3 = AddSubItem(cat);

			var pants = AddItem();
			var pants_sub1 = AddSubItem(pants);
			var pants_sub2 = AddSubItem(pants);
			var pants_sub3 = AddSubItem(pants);

			var buttnuts = AddItem();
			var buttnuts_sub1 = AddSubItem(buttnuts);
			var buttnuts_sub2 = AddSubItem(buttnuts);
			var buttnuts_sub3 = AddSubItem(buttnuts);

			var cat1 = AddItem();
			AddSubItem(cat1);
			AddSubItem(cat1);
			AddSubItem(cat1);

			var pants1 = AddItem();
			AddSubItem(pants1);
			AddSubItem(pants1);
			AddSubItem(pants1);

			var whoa = AddItem();
			AddSubItem(whoa);
			AddSubItem(whoa);
			AddSubItem(whoa);

			var test1 = AddItem();
			AddSubItem(test1);
			AddSubItem(test1);
			AddSubItem(test1);

			var test2 = AddItem();
			AddSubItem(test2);
			AddSubItem(test2);
			AddSubItem(test2);

			buttnuts.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			pants.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));
			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			Assert.AreEqual(0, cat.Position.Y);
			Assert.AreEqual(cat.Rect.Bottom, cat_sub1.Position.Y);
			Assert.AreEqual(cat_sub1.Rect.Bottom, cat_sub2.Position.Y);
			Assert.AreEqual(cat_sub2.Rect.Bottom, cat_sub3.Position.Y);
			Assert.AreEqual(cat_sub3.Rect.Bottom, pants.Position.Y);
			Assert.AreEqual(pants.Rect.Bottom, pants_sub1.Position.Y);
			Assert.AreEqual(pants_sub1.Rect.Bottom, pants_sub2.Position.Y);
			Assert.AreEqual(pants_sub2.Rect.Bottom, pants_sub3.Position.Y);
			Assert.AreEqual(pants_sub3.Rect.Bottom, buttnuts.Position.Y);
			Assert.AreEqual(buttnuts.Rect.Bottom, buttnuts_sub1.Position.Y);
			Assert.AreEqual(buttnuts_sub1.Rect.Bottom, buttnuts_sub2.Position.Y);
			Assert.AreEqual(buttnuts_sub2.Rect.Bottom, buttnuts_sub3.Position.Y);
			Assert.AreEqual(buttnuts_sub3.Rect.Bottom, cat1.Position.Y);
			Assert.AreEqual(cat1.Rect.Bottom, pants1.Position.Y);
			Assert.AreEqual(pants1.Rect.Bottom, whoa.Position.Y);
			Assert.AreEqual(whoa.Rect.Bottom, test1.Position.Y);
			Assert.AreEqual(test1.Rect.Bottom, test2.Position.Y);
		}

		#endregion //three items locations
	}
}
