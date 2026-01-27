using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ShimTests_HorizontalRight
	{
		#region Fields

		private IShim _shim;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void ShimTests_HorizontalRight_Setup()
		{
			_shim = new Shim();
			_shim.Horizontal = HorizontalAlignment.Right;
		}

		#endregion //Setup

		#region Rect, Padding, & Scale

		[Test]
		public void ShimTests_HorizontalRight_SetRectThenPaddingThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(-50, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalRight_SetRectThenScaleThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(-50, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalRight_SetPaddingThenRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(-50, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalRight_SetPaddingThenScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-50, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalRight_SetScaleThenPaddingThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-50, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_HorizontalRight_SetScaleThenRectThenPadding()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(-50, _shim.Rect.X);
			Assert.AreEqual(20, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		#endregion //Rect, Padding, & Scale
	}
}
