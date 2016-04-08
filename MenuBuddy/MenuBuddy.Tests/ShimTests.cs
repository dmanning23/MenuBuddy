using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ShimTests
	{
		#region Fields

		private IShim _shim;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void ShimTests_Setup()
		{
			DefaultStyles.InitUnitTests();
			_shim = new Shim();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void ShimTests_NullRect()
		{
			Assert.AreEqual(0, _shim.Rect.X);
			Assert.AreEqual(0, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_NullPosition()
		{
			Assert.AreEqual(0, _shim.Rect.X);
			Assert.AreEqual(0, _shim.Rect.Y);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void ShimTests_ChangePosition_CheckPosition()
		{
			_shim.Position = new Point(50, 60);

			Assert.AreEqual(50, _shim.Rect.X);
			Assert.AreEqual(60, _shim.Rect.Y);
		}

		[Test]
		public void ShimTests_ChangePosition_CheckRect()
		{
			_shim.Position = new Point(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_ChangeRect_CheckPosition()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(30, _shim.Rect.Width);
			Assert.AreEqual(40, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_ChangeRect_CheckRect()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
		}

		#endregion //Rect & Position 

		#region Padding

		[Test]
		public void ShimTests_SetRectThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Padding = new Vector2(10, 5);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(50, _shim.Rect.Width);
			Assert.AreEqual(50, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetPaddingThenRect()
		{
			_shim.Padding = new Vector2(10, 5);
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(50, _shim.Rect.Width);
			Assert.AreEqual(50, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetPositionThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Padding = new Vector2(10, 5);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(20, _shim.Rect.Width);
			Assert.AreEqual(10, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetPaddingThenPosition()
		{
			_shim.Padding = new Vector2(30, 50);
			_shim.Position = new Point(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(100, _shim.Rect.Height);
		}

		#endregion //Padding

		#region Scale

		[Test]
		public void ShimTests_SetRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2.0f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetPositionThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetScaleThenPosition()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_ChangeScale()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30f, 40f);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60f, _shim.Rect.Width);
			Assert.AreEqual(80f, _shim.Rect.Height);

			_shim.Size = new Vector2(50f, 60f);
			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100f, _shim.Rect.Width);
			Assert.AreEqual(120f, _shim.Rect.Height);
		}

		#endregion //Scale

		#region Rect, Padding, & Scale

		[Test]
		public void ShimTests_SetRectThenPaddingThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Padding = new Vector2(10, 20);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetRectThenScaleThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;
			_shim.Padding = new Vector2(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetPaddingThenRectThenScale()
		{
			_shim.Padding = new Vector2(10, 20);
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetPaddingThenScaleThenRect()
		{
			_shim.Padding = new Vector2(10, 20);
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetScaleThenPaddingThenRect()
		{
			_shim.Scale = 2f;
			_shim.Padding = new Vector2(10, 20);
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_SetScaleThenRectThenPadding()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Padding = new Vector2(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		#endregion //Rect, Padding, & Scale
	}
}
