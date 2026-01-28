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
			_tree.Rect.X.ShouldBe(0);
			_tree.Rect.Y.ShouldBe(0);
			_tree.Rect.Width.ShouldBe(0);
			_tree.Rect.Height.ShouldBe(0);
			_tree.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_tree.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Tree_Default2()
		{
			_tree.Position = new Point(10, 20);
			_tree.Rect.X.ShouldBe(10);
			_tree.Rect.Y.ShouldBe(20);
			_tree.Rect.Width.ShouldBe(0);
			_tree.Rect.Height.ShouldBe(0);
			_tree.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_tree.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Defaults

		#region unexpanded tests

		[Test]
		public void unexpanded_oneitem()
		{
			//create the top item
			var cat = AddItem();

			_tree.TotalRect.Right.ShouldBe(cat.Rect.Width);
			_tree.TotalRect.Bottom.ShouldBe(cat.Rect.Height);
			_tree.TotalRect.Width.ShouldBe(cat.Rect.Width);
			_tree.TotalRect.Height.ShouldBe(cat.Rect.Height);
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

			_tree.TotalRect.Right.ShouldBe(expectedWidth);
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

			_tree.TotalRect.Bottom.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Width.ShouldBe(expectedWidth);
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

			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Right.ShouldBe(expectedWidth);
			_tree.TotalRect.Bottom.ShouldBe(expectedHeight);
			_tree.TotalRect.Width.ShouldBe(expectedWidth);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
		}

		[Test]
		public void expand_one2()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			_tree.TotalRect.Bottom.ShouldBe(expectedHeight);
		}

		[Test]
		public void expand_one3()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			_tree.TotalRect.Width.ShouldBe(expectedWidth);
		}

		[Test]
		public void expand_one4()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height;

			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Right.ShouldBe(expectedWidth);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Bottom.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Width.ShouldBe(expectedWidth);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
		}

		#endregion //expand unexpand tests

		#region scroll bar tests

		[Test]
		public void scrollbar_default()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);
			

			_tree.TotalRect.Height.ShouldBe((int)_tree.MaxScroll.Y);
		}

		[Test]
		public void scrollbar_default1()
		{
			var cat = AddItem();
			var sub = AddSubItem(cat);

			var expectedHeight = cat.Rect.Height;

			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.TotalRect.Height.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			_tree.TotalRect.Left.ShouldBe(0);
			_tree.TotalRect.Top.ShouldBe(0);
			_tree.MaxScroll.Y.ShouldBe(expectedHeight);
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

			cat.Position.Y.ShouldBe(0);
			cat1.Position.Y.ShouldBe(cat.Rect.Bottom);
			sub1.Position.Y.ShouldBe(cat1.Rect.Bottom);
			cat2.Position.Y.ShouldBe(sub1.Rect.Bottom);
			sub2.Position.Y.ShouldBe(cat2.Rect.Bottom);
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

			cat.Position.Y.ShouldBe(0);
			sub.Position.Y.ShouldBe(cat.Rect.Bottom);
			cat1.Position.Y.ShouldBe(sub.Rect.Bottom);
			sub1.Position.Y.ShouldBe(cat1.Rect.Bottom);
			cat2.Position.Y.ShouldBe(sub1.Rect.Bottom);
			sub2.Position.Y.ShouldBe(cat2.Rect.Bottom);
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

			cat.Position.Y.ShouldBe(0);
			sub.Position.Y.ShouldBe(cat.Rect.Bottom);
			cat1.Position.Y.ShouldBe(sub.Rect.Bottom);
			cat2.Position.Y.ShouldBe(cat1.Rect.Bottom);
			sub2.Position.Y.ShouldBe(cat2.Rect.Bottom);
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

			cat.Position.Y.ShouldBe(0);
			sub.Position.Y.ShouldBe(cat.Rect.Bottom);
			cat1.Position.Y.ShouldBe(sub.Rect.Bottom);
			cat2.Position.Y.ShouldBe(cat1.Rect.Bottom);
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

			cat.Position.Y.ShouldBe(0);
			cat1.Position.Y.ShouldBe(cat.Rect.Bottom);
			cat2.Position.Y.ShouldBe(cat1.Rect.Bottom);
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

			cat.Position.Y.ShouldBe(0);
			cat_sub1.Position.Y.ShouldBe(cat.Rect.Bottom);
			cat_sub2.Position.Y.ShouldBe(cat_sub1.Rect.Bottom);
			cat_sub3.Position.Y.ShouldBe(cat_sub2.Rect.Bottom);
			pants.Position.Y.ShouldBe(cat_sub3.Rect.Bottom);
			pants_sub1.Position.Y.ShouldBe(pants.Rect.Bottom);
			pants_sub2.Position.Y.ShouldBe(pants_sub1.Rect.Bottom);
			pants_sub3.Position.Y.ShouldBe(pants_sub2.Rect.Bottom);
			buttnuts.Position.Y.ShouldBe(pants_sub3.Rect.Bottom);
			buttnuts_sub1.Position.Y.ShouldBe(buttnuts.Rect.Bottom);
			buttnuts_sub2.Position.Y.ShouldBe(buttnuts_sub1.Rect.Bottom);
			buttnuts_sub3.Position.Y.ShouldBe(buttnuts_sub2.Rect.Bottom);
			cat1.Position.Y.ShouldBe(buttnuts_sub3.Rect.Bottom);
			pants1.Position.Y.ShouldBe(cat1.Rect.Bottom);
			whoa.Position.Y.ShouldBe(pants1.Rect.Bottom);
			test1.Position.Y.ShouldBe(whoa.Rect.Bottom);
			test2.Position.Y.ShouldBe(test1.Rect.Bottom);
		}

		#endregion //three items locations
	}
}
