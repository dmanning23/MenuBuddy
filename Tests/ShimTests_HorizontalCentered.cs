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
			_shim = new Shim();
			_shim.Horizontal = HorizontalAlignment.Center;
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void ShimTests_HorizontalCentered_NullRect()
		{
			_shim.Rect.X.ShouldBe(0);
			_shim.Rect.Y.ShouldBe(0);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_HorizontalCentered_NullPosition()
		{
			_shim.Rect.X.ShouldBe(0);
			_shim.Rect.Y.ShouldBe(0);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void ShimTests_HorizontalCentered_ChangePosition_CheckPosition()
		{
			var _shim = new Shim();
			_shim.Position = new Point(50, 60);

			_shim.Rect.X.ShouldBe(50);
			_shim.Rect.Y.ShouldBe(60);
		}

		[Test]
		public void ShimTests_HorizontalCentered_ChangePosition_CheckRect()
		{
			var _shim = new Shim();
			_shim.Position = new Point(10, 20);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_HorizontalCentered_ChangeRect_CheckPosition()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(-5);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(30);
			_shim.Rect.Height.ShouldBe(40);
		}

		[Test]
		public void ShimTests_HorizontalCentered_ChangeRect_CheckRect()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(-5);
			_shim.Rect.Y.ShouldBe(20);
		}

		#endregion //Rect & Position 

		#region Scale

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2.0f;

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPositionThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenPosition()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		#endregion //Scale

		#region Rect, Padding, & Scale

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenPaddingThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenScaleThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPaddingThenRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetPaddingThenScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenPaddingThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_HorizontalCentered_SetScaleThenRectThenPadding()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(-20);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		#endregion //Rect, Padding, & Scale
	}
}
