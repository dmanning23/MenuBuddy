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
			Assert.AreEqual(10, _button.Rect.X);
			Assert.AreEqual(20, _button.Rect.Y);
			Assert.AreEqual(200f, _button.Rect.Width);
			Assert.AreEqual(100f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
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

			Assert.IsTrue(_button.CheckDrag(e));
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

			Assert.AreEqual(50, _button.Rect.X);
			Assert.AreEqual(100, _button.Rect.Y);
			Assert.AreEqual(200f, _button.Rect.Width);
			Assert.AreEqual(100f, _button.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Left, _button.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _button.Vertical);
		}

		#endregion //tests
	}
}
