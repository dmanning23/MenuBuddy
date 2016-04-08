using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ShimTests_HorizontalCentered
	{
		#region Fields

		private IShim _shim;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Shim_Setup()
		{
			DefaultStyles.InitUnitTests();
			_shim = new Shim();
			_shim.Horizontal = HorizontalAlignment.Center;
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void ShimTests_HorizontalCentered_NullRect()
		{
			Assert.AreEqual(0, _shim.Rect.X);
			Assert.AreEqual(0, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_NullPosition()
		{
			Assert.AreEqual(0, _shim.Rect.X);
			Assert.AreEqual(0, _shim.Rect.Y);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void ShimTests_HorizontalCentered_ChangePosition_CheckPosition()
		{
			var _shim = new Shim();
			_shim.Position = new Point(50, 60);

			Assert.AreEqual(50, _shim.Rect.X);
			Assert.AreEqual(60, _shim.Rect.Y);
		}

		[Test]
		public void ShimTests_HorizontalCentered_ChangePosition_CheckRect()
		{
			var _shim = new Shim();
			_shim.Position = new Point(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_ChangeRect_CheckPosition()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-5, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(30, _shim.Rect.Width);
			Assert.AreEqual(40, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_ChangeRect_CheckRect()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-5, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
		}

		#endregion //Rect & Position 

		#region Padding

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Padding = new Vector2(10, 50);

			Assert.AreEqual(-15, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(50, _shim.Rect.Width);
			Assert.AreEqual(140, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPaddingThenRect()
		{
			_shim.Padding = new Vector2(10, 50);
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-15, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(50, _shim.Rect.Width);
			Assert.AreEqual(140, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPositionThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Padding = new Vector2(10, 50);

			Assert.AreEqual(0, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(20, _shim.Rect.Width);
			Assert.AreEqual(100, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPaddingThenPosition()
		{
			_shim.Padding = new Vector2(30, 50);
			_shim.Position = new Point(10, 20);

			Assert.AreEqual(-20, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(100, _shim.Rect.Height);
		}

		#endregion //Padding

		#region Scale

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2.0f;

			Assert.AreEqual(-20, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-20, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPositionThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenPosition()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(0, _shim.Rect.Width);
			Assert.AreEqual(0, _shim.Rect.Height);
		}

		#endregion //Scale

		#region Rect, Padding, & Scale

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenPaddingThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Padding = new Vector2(10, 20);
			_shim.Scale = 2f;

			Assert.AreEqual(-40, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenScaleThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;
			_shim.Padding = new Vector2(10, 20);

			Assert.AreEqual(-40, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPaddingThenRectThenScale()
		{
			_shim.Padding = new Vector2(10, 20);
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(-40, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPaddingThenScaleThenRect()
		{
			_shim.Padding = new Vector2(10, 20);
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-40, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenPaddingThenRect()
		{
			_shim.Scale = 2f;
			_shim.Padding = new Vector2(10, 20);
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-40, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenRectThenPadding()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Padding = new Vector2(10, 20);

			Assert.AreEqual(-40, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(100, _shim.Rect.Width);
			Assert.AreEqual(160, _shim.Rect.Height);
		}

		#endregion //Rect, Padding, & Scale
	}
}
