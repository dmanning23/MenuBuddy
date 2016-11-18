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

		private Tree _tree;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			StyleSheet.InitUnitTests();
			var screen = new WidgetScreen("Test");
			_tree = new Tree(screen);
			_tree.LoadContent(screen);
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
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});

			//create the sub items
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub1);

			_tree.AddItem(cat);

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(cat.Rect.Width, _tree.TotalRect.Right);
			Assert.AreEqual(cat.Rect.Height, _tree.TotalRect.Bottom);
			Assert.AreEqual(cat.Rect.Width, _tree.TotalRect.Width);
			Assert.AreEqual(cat.Rect.Height, _tree.TotalRect.Height);
		}

		[Test]
		public void unexpanded_twoitem()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

			var expectedWidth = Math.Max(cat.Rect.Width, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Right);
		}

		[Test]
		public void unexpanded_twoitem1()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

			var expectedWidth = cat.Rect.Width + cat1.Rect.Width;
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
		}

		[Test]
		public void unexpanded_twoitem2()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

			var expectedWidth = Math.Max(cat.Rect.Width, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
		}

		[Test]
		public void unexpanded_twoitem3()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

			var expectedWidth = cat.Rect.Width + cat1.Rect.Width;
			var expectedHeight = cat.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		#endregion //unexpanded tests

		#region expand tests

		[Test]
		public void expand_one()
		{
			//create the top item
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});

			//create the sub items
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub1);

			_tree.AddItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Right);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void expand_one2()
		{
			//create the top item
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});

			//create the sub items
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub1);

			_tree.AddItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
		}

		[Test]
		public void expand_one3()
		{
			//create the top item
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});

			//create the sub items
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub1);

			_tree.AddItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedWidth, _tree.TotalRect.Width);
		}

		[Test]
		public void expand_one4()
		{
			//create the top item
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});

			//create the sub items
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub1);

			_tree.AddItem(cat);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void expand_two()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

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
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			expectedWidth = Math.Max(expectedWidth, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Bottom);
		}

		[Test]
		public void expand_two3()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

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
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

			cat.ToggleExpansion(this, new ClickEventArgs(Vector2.Zero, InputHelper.MouseButton.Left, null));

			var expectedWidth = Math.Max(cat.Rect.Width, sub.Rect.Width);
			expectedWidth = Math.Max(expectedWidth, cat1.Rect.Width);
			var expectedHeight = cat.Rect.Height + sub.Rect.Height + cat1.Rect.Height;

			Assert.AreEqual(0, _tree.TotalRect.Left);
			Assert.AreEqual(0, _tree.TotalRect.Top);
			Assert.AreEqual(expectedHeight, _tree.TotalRect.Height);
		}

		[Test]
		public void multiple_expand_two()
		{
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

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
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

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
			var cat = new TreeItem(_tree, null);
			cat.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub = new TreeItem(_tree, cat);
			sub.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat.ChildItems.Add(sub);

			_tree.AddItem(cat);

			var cat1 = new TreeItem(_tree, null);
			cat1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			var sub1 = new TreeItem(_tree, cat);
			sub1.SetButton(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			cat1.ChildItems.Add(sub1);

			_tree.AddItem(cat1);

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
	}
}
