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
	public class DragDropTests
	{
		#region Fields

		private DragDropButton _button;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			_button = new DragDropButton();
		}

		#endregion //Setup

		#region tests

		[Test]
		public void Position_Default()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(200f, 100f);
			_button.Rect.X.ShouldBe(10);
			_button.Rect.Y.ShouldBe(20);
			_button.Rect.Width.ShouldBe(200);
			_button.Rect.Height.ShouldBe(100);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void Position_ContainsEvent()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(200f, 100f);

			var e = new DragEventArgs
			{
				Start = new Vector2(20f, 40f),
				Current = new Vector2(50, 100)
			};

			_button.CheckDrag(e).ShouldBeTrue();
		}

		[Test]
		public void Position_Moved()
		{
			_button.Position = new Point(10, 20);
			_button.Size = new Vector2(200f, 100f);

			var e = new DragEventArgs
			{
				Start = new Vector2(20f, 40f),
				Current = new Vector2(50, 100)
			};

			_button.CheckDrag(e);

			_button.Rect.X.ShouldBe(50);
			_button.Rect.Y.ShouldBe(100);
			_button.Rect.Width.ShouldBe(200);
			_button.Rect.Height.ShouldBe(100);
			_button.Horizontal.ShouldBe(HorizontalAlignment.Left);
			_button.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		#endregion //tests
	}
}
