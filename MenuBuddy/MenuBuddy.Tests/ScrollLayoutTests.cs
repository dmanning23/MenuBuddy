using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ScrollLayoutTests
	{
		#region Fields

		private ScrollLayout _layout;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			DefaultStyles.InitUnitTests();
			_layout = new ScrollLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void ScrollLayout_Default()
		{
			Assert.AreEqual(0, _layout.Rect.X);
			Assert.AreEqual(0, _layout.Rect.Y);
			Assert.AreEqual(0, _layout.Rect.Width);
			Assert.AreEqual(0, _layout.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _layout.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _layout.Vertical);
		}

		#endregion //Defaults

		#region Rect Tests

		[Test]
		public void ScrollLayout_TotalDefault()
		{
			Assert.AreEqual(0, _layout.TotalRect.X);
			Assert.AreEqual(0, _layout.TotalRect.Y);
			Assert.AreEqual(0, _layout.TotalRect.Width);
			Assert.AreEqual(0, _layout.TotalRect.Height);
		}

		[Test]
		public void ScrollLayout_TotalOne()
		{
			_layout.AddItem(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			Assert.AreEqual(0, _layout.TotalRect.Left);
			Assert.AreEqual(0, _layout.TotalRect.Top);
			Assert.AreEqual(30, _layout.TotalRect.Right);
			Assert.AreEqual(40, _layout.TotalRect.Bottom);
			Assert.AreEqual(30, _layout.TotalRect.Width);
			Assert.AreEqual(40, _layout.TotalRect.Height);
		}

		[Test]
		public void ScrollLayout_Total_less()
		{
			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(30, 40)
			});
			Assert.AreEqual(-10, _layout.TotalRect.Left);
			Assert.AreEqual(-20, _layout.TotalRect.Top);
			Assert.AreEqual(20, _layout.TotalRect.Right);
			Assert.AreEqual(20, _layout.TotalRect.Bottom);
			Assert.AreEqual(30, _layout.TotalRect.Width);
			Assert.AreEqual(40, _layout.TotalRect.Height);
		}

		[Test]
		public void ScrollLayout_Total_two()
		{
			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(30, 40)
			});

			_layout.AddItem(new Shim()
			{
				Position = new Point(10, 20),
				Size = new Vector2(30, 40)
			});
			Assert.AreEqual(-10, _layout.TotalRect.Left);
			Assert.AreEqual(-20, _layout.TotalRect.Top);
			Assert.AreEqual(40, _layout.TotalRect.Right);
			Assert.AreEqual(60, _layout.TotalRect.Bottom);
			Assert.AreEqual(50, _layout.TotalRect.Width);
			Assert.AreEqual(80, _layout.TotalRect.Height);
		}

		[Test]
		public void ScrollLayout_total_scrollrect()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(0, _layout.TotalRect.Left);
			Assert.AreEqual(0, _layout.TotalRect.Top);
			Assert.AreEqual(100, _layout.TotalRect.Right);
			Assert.AreEqual(200, _layout.TotalRect.Bottom);
			Assert.AreEqual(100, _layout.TotalRect.Width);
			Assert.AreEqual(200, _layout.TotalRect.Height);
		}

		#endregion //Rect Tests

		#region MinMax Scroll

		[Test]
		public void ScrollLayout_min_x()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(-10, _layout.MinScroll.X);
		}

		[Test]
		public void ScrollLayout_min_y()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(-20, _layout.MinScroll.Y);
		}

		[Test]
		public void ScrollLayout_max_x()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(60, _layout.MaxScroll.X);
		}

		[Test]
		public void ScrollLayout_max_y()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(140, _layout.MaxScroll.Y);
		}

		#endregion //MinMax Scroll

		#region Constrain Scroll

		[Test]
		public void ScrollLayout_constrain_ok()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			_layout.ScrollPosition = new Vector2(50, 60);
			Assert.AreEqual(50, _layout.ScrollPosition.X);
			Assert.AreEqual(60, _layout.ScrollPosition.Y);
		}

		[Test]
		public void ScrollLayout_constrainmin_x()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(-10, _layout.MinScroll.X);

			_layout.ScrollPosition = new Vector2(-50, 60);
			Assert.AreEqual(-10, _layout.ScrollPosition.X);
		}

		[Test]
		public void ScrollLayout_constrainmin_y()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(-20, _layout.MinScroll.Y);

			_layout.ScrollPosition = new Vector2(50, -60);
			Assert.AreEqual(-20, _layout.ScrollPosition.Y);
		}

		[Test]
		public void ScrollLayout_constrainmax_x()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(60, _layout.MaxScroll.X);

			_layout.ScrollPosition = new Vector2(500, 60);
			Assert.AreEqual(60, _layout.ScrollPosition.X);
		}

		[Test]
		public void ScrollLayout_constrainmax_y()
		{
			_layout.Position = new Point(10, 20);
			_layout.Size = new Vector2(30, 40);

			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(100, 200)
			});

			Assert.AreEqual(140, _layout.MaxScroll.Y);

			_layout.ScrollPosition = new Vector2(50, 600);
			Assert.AreEqual(140, _layout.ScrollPosition.Y);
		}

		#endregion //Constrain Scroll

		#region Scrollbar tests

		[Test]
		public void Scrollbar__defaultvertical()
		{
			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(0, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(0, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar__defaulthorizontal()
		{
			Assert.AreEqual(0, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(0, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);
		}

		[Test]
		public void Scrollbar__defaultdraw()
		{
			Assert.AreEqual(false, _layout.DrawVerticalScrollBar);
			Assert.AreEqual(false, _layout.DrawHorizontalScrollBar);
		}

		[Test]
		public void Scrollbar_HorizontalDefaults()
		{
			_layout.Size = new Vector2(100, 0);

			var shim = new Shim()
			{
				Size = new Vector2(200, 0)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(84, _layout.VerticalScrollBar.X);
			Assert.AreEqual(0, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(0, _layout.VerticalScrollBar.Height);

			Assert.AreEqual(Vector2.Zero, _layout.MinScroll);
			Assert.AreEqual(100, _layout.MaxScroll.X);
			Assert.AreEqual(0, _layout.MaxScroll.Y);

			Assert.AreEqual(false, _layout.DrawVerticalScrollBar);
			Assert.AreEqual(true, _layout.DrawHorizontalScrollBar);
		}

		[Test]
		public void Scrollbar_HorizontalStart()
		{
			_layout.Size = new Vector2(100, 0);

			var shim = new Shim()
			{
				Size = new Vector2(200, 0)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(0, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(50, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_HorizontalMid()
		{
			_layout.Size = new Vector2(100, 0);

			var shim = new Shim()
			{
				Size = new Vector2(200, 0)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(50, 0);

			Assert.AreEqual(25, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(50, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_HorizontalEnd()
		{
			_layout.Size = new Vector2(100, 0);

			var shim = new Shim()
			{
				Size = new Vector2(200, 0)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(100, 0);

			Assert.AreEqual(50, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(50, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_VerticalDefaults()
		{
			_layout.Size = new Vector2(0, 100);

			var shim = new Shim()
			{
				Size = new Vector2(0, 200)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(0, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(84, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(0, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(Vector2.Zero, _layout.MinScroll);
			Assert.AreEqual(0, _layout.MaxScroll.X);
			Assert.AreEqual(100, _layout.MaxScroll.Y);

			Assert.AreEqual(true, _layout.DrawVerticalScrollBar);
			Assert.AreEqual(false, _layout.DrawHorizontalScrollBar);
		}

		[Test]
		public void Scrollbar_VerticalStart()
		{
			_layout.Size = new Vector2(0, 100);

			var shim = new Shim()
			{
				Size = new Vector2(0, 200)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(0, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_VerticalMid()
		{
			_layout.Size = new Vector2(0, 100);

			var shim = new Shim()
			{
				Size = new Vector2(0, 200)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(0, 50);

			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(25, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_VerticalEnd()
		{
			_layout.Size = new Vector2(0, 100);

			var shim = new Shim()
			{
				Size = new Vector2(0, 200)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(0, 100);

			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_BothStart()
		{
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(0, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(84, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(500, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(984, _layout.VerticalScrollBar.X);
			Assert.AreEqual(0, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);

			Assert.AreEqual(Vector2.Zero, _layout.MinScroll);
			Assert.AreEqual(1000, _layout.MaxScroll.X);
			Assert.AreEqual(100, _layout.MaxScroll.Y);
		}

		[Test]
		public void Scrollbar_BothMid()
		{
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(500, 50);

			Assert.AreEqual(250, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(84, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(500, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(984, _layout.VerticalScrollBar.X);
			Assert.AreEqual(25, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_BothEnd()
		{
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(1000, 100);

			Assert.AreEqual(500, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(84, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(500, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(984, _layout.VerticalScrollBar.X);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_MoveLayout_HorizontalStart()
		{
			_layout.Position = new Point(10000, 10000);
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(10000, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(500, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(Vector2.Zero, _layout.MinScroll);
			Assert.AreEqual(1000, _layout.MaxScroll.X);
			Assert.AreEqual(100, _layout.MaxScroll.Y);
		}

		[Test]
		public void Scrollbar_MoveLayout_VerticalStart()
		{
			_layout.Position = new Point(10000, 10000);
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(10000, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);

			Assert.AreEqual(Vector2.Zero, _layout.MinScroll);
			Assert.AreEqual(1000, _layout.MaxScroll.X);
			Assert.AreEqual(100, _layout.MaxScroll.Y);
		}

		[Test]
		public void Scrollbar_MoveLayout_Mid()
		{
			_layout.Position = new Point(10000, 10000);
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(500, 50);

			Assert.AreEqual(10250, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(500, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(10025, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		[Test]
		public void Scrollbar_MoveLayout_End()
		{
			_layout.Position = new Point(10000, 10000);
			_layout.Size = new Vector2(1000, 100);

			var shim = new Shim()
			{
				Size = new Vector2(2000, 200)
			};
			_layout.AddItem(shim);

			_layout.ScrollPosition = new Vector2(1000, 100);

			Assert.AreEqual(10500, _layout.HorizontalScrollBar.X);
			Assert.AreEqual(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Y);
			Assert.AreEqual(500, _layout.HorizontalScrollBar.Width);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.HorizontalScrollBar.Height);

			Assert.AreEqual(_layout.Rect.Right - ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.X);
			Assert.AreEqual(10050, _layout.VerticalScrollBar.Y);
			Assert.AreEqual(ScrollLayout.ScrollBarWidth, _layout.VerticalScrollBar.Width);
			Assert.AreEqual(50, _layout.VerticalScrollBar.Height);
		}

		#endregion //Scrollbar tests
	}
}
