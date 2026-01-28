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
			_shim = new Shim();
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void ShimTests_NullRect()
		{
			_shim.Rect.X.ShouldBe(0);
			_shim.Rect.Y.ShouldBe(0);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_NullPosition()
		{
			_shim.Rect.X.ShouldBe(0);
			_shim.Rect.Y.ShouldBe(0);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void ShimTests_ChangePosition_CheckPosition()
		{
			_shim.Position = new Point(50, 60);

			_shim.Rect.X.ShouldBe(50);
			_shim.Rect.Y.ShouldBe(60);
		}

		[Test]
		public void ShimTests_ChangePosition_CheckRect()
		{
			_shim.Position = new Point(10, 20);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_ChangeRect_CheckPosition()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(30);
			_shim.Rect.Height.ShouldBe(40);
		}

		[Test]
		public void ShimTests_ChangeRect_CheckRect()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
		}

		#endregion //Rect & Position 

		#region Scale

		[Test]
		public void ShimTests_SetRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2.0f;

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetPositionThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_SetScaleThenPosition()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(0);
			_shim.Rect.Height.ShouldBe(0);
		}

		[Test]
		public void ShimTests_ChangeScale()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30f, 40f);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);

			_shim.Size = new Vector2(50f, 60f);
			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(100);
			_shim.Rect.Height.ShouldBe(120);
		}

		#endregion //Scale

		#region Rect, Padding, & Scale

		[Test]
		public void ShimTests_SetRectThenPaddingThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetRectThenScaleThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetPaddingThenRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetPaddingThenScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetScaleThenPaddingThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void ShimTests_SetScaleThenRectThenPadding()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			_shim.Rect.X.ShouldBe(10);
			_shim.Rect.Y.ShouldBe(20);
			_shim.Rect.Width.ShouldBe(60);
			_shim.Rect.Height.ShouldBe(80);
		}

		#endregion //Rect, Padding, & Scale
	}
}
