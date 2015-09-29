using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Microsoft.Xna.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
    public class ShimTests
    {
		[SetUp]
		public void Setup()
		{
			DefaultStyles.InitUnitTests();
		}

		[Test]
		public void NullRect()
		{
			var shim = new Shim();

			Assert.AreEqual(0, shim.Rect.X);
			Assert.AreEqual(0, shim.Rect.Y);
			Assert.AreEqual(0, shim.Rect.Width);
			Assert.AreEqual(0, shim.Rect.Height);
		}

		[Test]
		public void NullPosition()
		{
			var shim = new Shim();

			Assert.AreEqual(0, shim.Position.X);
			Assert.AreEqual(0, shim.Position.Y);
		}

		[Test]
		public void SetRect_CheckRect()
		{
			var shim = new Shim()
			{
				Rect = new Rectangle(10, 20, 30, 40)
			};

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(30, shim.Rect.Width);
			Assert.AreEqual(40, shim.Rect.Height);
		}

		[Test]
		public void SetRect_CheckPosition()
		{
			var shim = new Shim()
			{
				Rect = new Rectangle(10, 20, 30, 40)
			};

			Assert.AreEqual(10, shim.Position.X);
			Assert.AreEqual(20, shim.Position.Y);
		}

		[Test]
		public void SetPosition_CheckPosition()
		{
			var shim = new Shim()
			{
				Position = new Point(10, 20)
			};

			Assert.AreEqual(10, shim.Position.X);
			Assert.AreEqual(20, shim.Position.Y);
		}

		[Test]
		public void SetPosition_CheckRect()
		{
			var shim = new Shim()
			{
				Position = new Point(10, 20)
			};

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(0, shim.Rect.Width);
			Assert.AreEqual(0, shim.Rect.Height);
		}

		[Test]
		public void ChangePosition_CheckPosition()
		{
			var shim = new Shim();
			shim.Position = new Point(50, 60);

			Assert.AreEqual(50, shim.Position.X);
			Assert.AreEqual(60, shim.Position.Y);
		}

		[Test]
		public void ChangePosition_CheckRect()
		{
			var shim = new Shim();
			shim.Position = new Point(10, 20);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(0, shim.Rect.Width);
			Assert.AreEqual(0, shim.Rect.Height);
		}

		[Test]
		public void ChangeRect_CheckPosition()
		{
			var shim = new Shim();
			shim.Rect = new Rectangle(10, 20, 30, 40);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(30, shim.Rect.Width);
			Assert.AreEqual(40, shim.Rect.Height);
		}

		[Test]
		public void ChangeRect_CheckRect()
		{
			var shim = new Shim();
			shim.Rect = new Rectangle(10, 20, 30, 40);

			Assert.AreEqual(10, shim.Position.X);
			Assert.AreEqual(20, shim.Position.Y);
		}

		[Test]
		public void SetRectThenPadding()
		{
			var shim = new Shim();
			shim.Rect = new Rectangle(10, 20, 30, 40);
			shim.Padding = new Vector2(10, 5);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(50, shim.Rect.Width);
			Assert.AreEqual(50, shim.Rect.Height);
		}

		[Test]
		public void SetPaddingThenRect()
		{
			var shim = new Shim();
			shim.Padding = new Vector2(10, 5);
			shim.Rect = new Rectangle(10, 20, 30, 40);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(30, shim.Rect.Width);
			Assert.AreEqual(40, shim.Rect.Height);
		}

		[Test]
		public void SetPositionThenPadding()
		{
			var shim = new Shim();
			shim.Position = new Point(10, 20);
			shim.Padding = new Vector2(10, 5);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(20, shim.Rect.Width);
			Assert.AreEqual(10, shim.Rect.Height);
		}

		[Test]
		public void SetPaddingThenPosition()
		{
			var shim = new Shim();
			shim.Padding = new Vector2(30, 50);
			shim.Position = new Point(10, 20);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(60, shim.Rect.Width);
			Assert.AreEqual(100, shim.Rect.Height);
		}

		[Test]
		public void SetRectThenScale()
		{
			var shim = new Shim();
			shim.Rect = new Rectangle(10, 20, 30, 40);
			shim.Scale = 2.0f;

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(60, shim.Rect.Width);
			Assert.AreEqual(80, shim.Rect.Height);
		}

		[Test]
		public void SetScaleThenRect()
		{
			var shim = new Shim();
			shim.Scale = 2f;
			shim.Rect = new Rectangle(10, 20, 30, 40);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(30, shim.Rect.Width);
			Assert.AreEqual(40, shim.Rect.Height);
		}

		[Test]
		public void SetPositionThenScale()
		{
			var shim = new Shim();
			shim.Position = new Point(10, 20);
			shim.Scale = 2f;

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(0, shim.Rect.Width);
			Assert.AreEqual(0, shim.Rect.Height);
		}

		[Test]
		public void SetScaleThenPosition()
		{
			var shim = new Shim();
			shim.Scale = 2f;
			shim.Position = new Point(10, 20);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(0, shim.Rect.Width);
			Assert.AreEqual(0, shim.Rect.Height);
		}

		[Test]
		public void SetRectThenPaddingThenScale()
		{
			var shim = new Shim();
			shim.Rect = new Rectangle(10, 20, 30, 40);
			shim.Padding = new Vector2(10, 20);
			shim.Scale = 2f;

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(100, shim.Rect.Width);
			Assert.AreEqual(160, shim.Rect.Height);
		}

		[Test]
		public void SetRectThenScaleThenPadding()
		{
			var shim = new Shim();
			shim.Rect = new Rectangle(10, 20, 30, 40);
			shim.Scale = 2f;
			shim.Padding = new Vector2(10, 20);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(100, shim.Rect.Width);
			Assert.AreEqual(160, shim.Rect.Height);
		}

		[Test]
		public void SetPaddingThenRectThenScale()
		{
			var shim = new Shim();
			shim.Padding = new Vector2(10, 20);
			shim.Rect = new Rectangle(10, 20, 30, 40);
			shim.Scale = 2f;

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(60, shim.Rect.Width);
			Assert.AreEqual(80, shim.Rect.Height);
		}

		[Test]
		public void SetPaddingThenScaleThenRect()
		{
			var shim = new Shim();
			shim.Padding = new Vector2(10, 20);
			shim.Scale = 2f;
			shim.Rect = new Rectangle(10, 20, 30, 40);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(30, shim.Rect.Width);
			Assert.AreEqual(40, shim.Rect.Height);
		}

		[Test]
		public void SetScaleThenPaddingThenRect()
		{
			var shim = new Shim();
			shim.Scale = 2f;
			shim.Padding = new Vector2(10, 20);
			shim.Rect = new Rectangle(10, 20, 30, 40);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(30, shim.Rect.Width);
			Assert.AreEqual(40, shim.Rect.Height);
		}

		[Test]
		public void SetScaleThenRectThenPadding()
		{
			var shim = new Shim();
			shim.Scale = 2f;
			shim.Rect = new Rectangle(10, 20, 30, 40);
			shim.Padding = new Vector2(10, 20);

			Assert.AreEqual(10, shim.Rect.X);
			Assert.AreEqual(20, shim.Rect.Y);
			Assert.AreEqual(50, shim.Rect.Width);
			Assert.AreEqual(80, shim.Rect.Height);
		}
	}
}
