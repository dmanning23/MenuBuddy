using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ShimTests_VerticalBottom
	{
		#region Fields

		private IShim _shim;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Shim_Setup()
		{
			StyleSheet.InitUnitTests();
			_shim = new Shim();
			_shim.Vertical = VerticalAlignment.Bottom;
		}

		#endregion //Setup

		#region Rect, Padding, & Scale

		[Test]
		public void ShimTests_VerticalBottom_SetRectThenPaddingThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(-60, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_VerticalBottom_SetRectThenScaleThenPadding()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(-60, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_VerticalBottom_SetPaddingThenRectThenScale()
		{
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);
			_shim.Scale = 2f;

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(-60, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_VerticalBottom_SetPaddingThenScaleThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(-60, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_VerticalBottom_SetScaleThenPaddingThenRect()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(-60, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		[Test]
		public void ShimTests_VerticalBottom_SetScaleThenRectThenPadding()
		{
			_shim.Scale = 2f;
			_shim.Position = new Point(10, 20);
			_shim.Size = new Vector2(30, 40);

			Assert.AreEqual(10, _shim.Rect.X);
			Assert.AreEqual(-60, _shim.Rect.Y);
			Assert.AreEqual(60, _shim.Rect.Width);
			Assert.AreEqual(80, _shim.Rect.Height);
		}

		#endregion //Rect, Padding, & Scale
	}
}
