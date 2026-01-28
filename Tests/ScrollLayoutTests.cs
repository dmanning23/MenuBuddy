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
			_layout = new ScrollLayout();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void ScrollLayout_Default()
		{
			_layout.Rect.X.ShouldBe(0);
			_layout.Rect.Y.ShouldBe(0);
			_layout.Rect.Width.ShouldBe(0);
			_layout.Rect.Height.ShouldBe(0);
			_layout.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_layout.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //Defaults

		#region Rect Tests

		[Test]
		public void ScrollLayout_TotalDefault()
		{
			_layout.TotalRect.X.ShouldBe(0);
			_layout.TotalRect.Y.ShouldBe(0);
			_layout.TotalRect.Width.ShouldBe(0);
			_layout.TotalRect.Height.ShouldBe(0);
		}

		[Test]
		public void ScrollLayout_TotalOne()
		{
			_layout.AddItem(new Shim()
			{
				Position = new Point(0, 0),
				Size = new Vector2(30, 40)
			});
			_layout.TotalRect.Left.ShouldBe(0);
			_layout.TotalRect.Top.ShouldBe(0);
			_layout.TotalRect.Right.ShouldBe(30);
			_layout.TotalRect.Bottom.ShouldBe(40);
			_layout.TotalRect.Width.ShouldBe(30);
			_layout.TotalRect.Height.ShouldBe(40);
		}

		[Test]
		public void ScrollLayout_Total_less()
		{
			_layout.AddItem(new Shim()
			{
				Position = new Point(-10, -20),
				Size = new Vector2(30, 40)
			});
			_layout.TotalRect.Left.ShouldBe(-10);
			_layout.TotalRect.Top.ShouldBe(-20);
			_layout.TotalRect.Right.ShouldBe(20);
			_layout.TotalRect.Bottom.ShouldBe(20);
			_layout.TotalRect.Width.ShouldBe(30);
			_layout.TotalRect.Height.ShouldBe(40);
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
			_layout.TotalRect.Left.ShouldBe(-10);
			_layout.TotalRect.Top.ShouldBe(-20);
			_layout.TotalRect.Right.ShouldBe(40);
			_layout.TotalRect.Bottom.ShouldBe(60);
			_layout.TotalRect.Width.ShouldBe(50);
			_layout.TotalRect.Height.ShouldBe(80);
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

			_layout.TotalRect.Left.ShouldBe(0);
			_layout.TotalRect.Top.ShouldBe(0);
			_layout.TotalRect.Right.ShouldBe(100);
			_layout.TotalRect.Bottom.ShouldBe(200);
			_layout.TotalRect.Width.ShouldBe(100);
			_layout.TotalRect.Height.ShouldBe(200);
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

			_layout.MinScroll.X.ShouldBe(-10);
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

			_layout.MinScroll.Y.ShouldBe(-20);
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

			_layout.MaxScroll.X.ShouldBe(60);
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

			_layout.MaxScroll.Y.ShouldBe(140);
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
			_layout.ScrollPosition.X.ShouldBe(50);
			_layout.ScrollPosition.Y.ShouldBe(60);
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

			_layout.MinScroll.X.ShouldBe(-10);

			_layout.ScrollPosition = new Vector2(-50, 60);
			_layout.ScrollPosition.X.ShouldBe(-10);
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

			_layout.MinScroll.Y.ShouldBe(-20);

			_layout.ScrollPosition = new Vector2(50, -60);
			_layout.ScrollPosition.Y.ShouldBe(-20);
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

			_layout.MaxScroll.X.ShouldBe(60);

			_layout.ScrollPosition = new Vector2(500, 60);
			_layout.ScrollPosition.X.ShouldBe(60);
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

			_layout.MaxScroll.Y.ShouldBe(140);

			_layout.ScrollPosition = new Vector2(50, 600);
			_layout.ScrollPosition.Y.ShouldBe(140);
		}

		#endregion //Constrain Scroll

		#region Scrollbar tests

		[Test]
		public void Scrollbar__defaultvertical()
		{
			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(0);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(0);
		}

		[Test]
		public void Scrollbar__defaulthorizontal()
		{
			_layout.HorizontalScrollBar.X.ShouldBe(0);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(0);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);
		}

		[Test]
		public void Scrollbar__defaultdraw_1()
		{
			_layout.ShowScrollBars.ShouldBe(true);
		}

		[Test]
		public void Scrollbar__defaultdraw()
		{
			_layout.DrawVerticalScrollBar.ShouldBe(false);
			_layout.DrawHorizontalScrollBar.ShouldBe(false);
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

			_layout.VerticalScrollBar.X.ShouldBe(84);
			_layout.VerticalScrollBar.Y.ShouldBe(0);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(0);

			_layout.MinScroll.ShouldBe(Vector2.Zero);
			_layout.MaxScroll.X.ShouldBe(100);
			_layout.MaxScroll.Y.ShouldBe(0);

			_layout.DrawVerticalScrollBar.ShouldBe(false);
			_layout.DrawHorizontalScrollBar.ShouldBe(true);
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

			_layout.HorizontalScrollBar.X.ShouldBe(0);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(50);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);
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

			_layout.HorizontalScrollBar.X.ShouldBe(25);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(50);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);
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

			_layout.HorizontalScrollBar.X.ShouldBe(50);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(50);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);
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

			_layout.HorizontalScrollBar.X.ShouldBe(0);
			_layout.HorizontalScrollBar.Y.ShouldBe(84);
			_layout.HorizontalScrollBar.Width.ShouldBe(0);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.MinScroll.ShouldBe(Vector2.Zero);
			_layout.MaxScroll.X.ShouldBe(0);
			_layout.MaxScroll.Y.ShouldBe(100);

			_layout.DrawVerticalScrollBar.ShouldBe(true);
			_layout.DrawHorizontalScrollBar.ShouldBe(false);
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

			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(0);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
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

			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(25);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
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

			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(50);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
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

			_layout.HorizontalScrollBar.X.ShouldBe(0);
			_layout.HorizontalScrollBar.Y.ShouldBe(84);
			_layout.HorizontalScrollBar.Width.ShouldBe(500);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.VerticalScrollBar.X.ShouldBe(984);
			_layout.VerticalScrollBar.Y.ShouldBe(0);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);

			_layout.MinScroll.ShouldBe(Vector2.Zero);
			_layout.MaxScroll.X.ShouldBe(1000);
			_layout.MaxScroll.Y.ShouldBe(100);
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

			_layout.HorizontalScrollBar.X.ShouldBe(250);
			_layout.HorizontalScrollBar.Y.ShouldBe(84);
			_layout.HorizontalScrollBar.Width.ShouldBe(500);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.VerticalScrollBar.X.ShouldBe(984);
			_layout.VerticalScrollBar.Y.ShouldBe(25);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
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

			_layout.HorizontalScrollBar.X.ShouldBe(500);
			_layout.HorizontalScrollBar.Y.ShouldBe(84);
			_layout.HorizontalScrollBar.Width.ShouldBe(500);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.VerticalScrollBar.X.ShouldBe(984);
			_layout.VerticalScrollBar.Y.ShouldBe(50);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
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

			_layout.HorizontalScrollBar.X.ShouldBe(10000);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(500);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.MinScroll.ShouldBe(Vector2.Zero);
			_layout.MaxScroll.X.ShouldBe(1000);
			_layout.MaxScroll.Y.ShouldBe(100);
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

			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(10000);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);

			_layout.MinScroll.ShouldBe(Vector2.Zero);
			_layout.MaxScroll.X.ShouldBe(1000);
			_layout.MaxScroll.Y.ShouldBe(100);
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

			_layout.HorizontalScrollBar.X.ShouldBe(10250);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(500);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(10025);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
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

			_layout.HorizontalScrollBar.X.ShouldBe(10500);
			_layout.HorizontalScrollBar.Y.ShouldBe((int)(_layout.Rect.Bottom - ScrollLayout.ScrollBarWidth));
			_layout.HorizontalScrollBar.Width.ShouldBe(500);
			_layout.HorizontalScrollBar.Height.ShouldBe((int)ScrollLayout.ScrollBarWidth);

			_layout.VerticalScrollBar.X.ShouldBe((int)(_layout.Rect.Right - ScrollLayout.ScrollBarWidth));
			_layout.VerticalScrollBar.Y.ShouldBe(10050);
			_layout.VerticalScrollBar.Width.ShouldBe((int)ScrollLayout.ScrollBarWidth);
			_layout.VerticalScrollBar.Height.ShouldBe(50);
		}

		#endregion //Scrollbar tests
	}
}
