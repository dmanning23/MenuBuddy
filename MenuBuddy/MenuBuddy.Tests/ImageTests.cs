using Moq;
using FontBuddyLib;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy.Tests
{
	public class TestTexture2D : Texture2D
	{
		public TestTexture2D() : base(null, 0, 0)
		{
		}

		public new virtual int Width
		{
			get
			{
				return base.Width;
			}
		}

		public new virtual int Height
		{
			get
			{
				return base.Height;
			}
		}

		public new virtual Rectangle Bounds
		{
			get
			{
				return base.Bounds;
			}
		}
	}

	[TestFixture]
	public class ImageTests
	{
		#region Fields

		private Mock<Image> _mockImage;
		private Image _image;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void ImageTests_Setup()
		{
			StyleSheet.InitUnitTests();

			_mockImage = new Mock<Image>() { CallBase = true };
			_mockImage.Setup(x => x.Width).Returns(30);
			_mockImage.Setup(x => x.Height).Returns(40);
			_mockImage.Setup(x => x.Bounds).Returns(new Rectangle(Point.Zero, new Point(30, 40)));
			_mockImage.Object.Texture = null;
			_image = _mockImage.Object;
        }

		#endregion //Setup

		#region Defaults

		[Test]
		public void ImageTests_NullRect()
		{
			Assert.AreEqual(0, _image.Rect.X);
			Assert.AreEqual(0, _image.Rect.Y);
			Assert.AreEqual(30, _image.Rect.Width);
			Assert.AreEqual(40, _image.Rect.Height);
		}

		[Test]
		public void ImageTests_NullPosition()
		{
			Assert.AreEqual(0, _image.Rect.X);
			Assert.AreEqual(0, _image.Rect.Y);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void ImageTests_ChangePosition_CheckPosition()
		{
			_image.Position = new Point(50, 60);

			Assert.AreEqual(50, _image.Rect.X);
			Assert.AreEqual(60, _image.Rect.Y);
		}

		[Test]
		public void ImageTests_ChangePosition_CheckRect()
		{
			_image.Position = new Point(10, 20);

			Assert.AreEqual(10, _image.Rect.X);
			Assert.AreEqual(20, _image.Rect.Y);
			Assert.AreEqual(30, _image.Rect.Width);
			Assert.AreEqual(40, _image.Rect.Height);
		}

		[Test]
		public void ImageTests_ChangeText_CheckRect()
		{
			_image.Position = new Point(10, 20);

			_mockImage.Setup(x => x.Width).Returns(50);
			_mockImage.Setup(x => x.Height).Returns(60);
			_mockImage.Setup(x => x.Bounds).Returns(new Rectangle(Point.Zero, new Point(50, 60)));
			_image.Texture = null;

			Assert.AreEqual(50, _image.Rect.Width);
			Assert.AreEqual(60, _image.Rect.Height);
		}

		#endregion //Rect & Position 

		#region Scale

		[Test]
		public void ImageTests_SetRectThenScale()
		{
			_image.Position = new Point(10, 20);
			_image.Scale = 2.0f;

			Assert.AreEqual(10, _image.Rect.X);
			Assert.AreEqual(20, _image.Rect.Y);
			Assert.AreEqual(60, _image.Rect.Width);
			Assert.AreEqual(80, _image.Rect.Height);
		}

		[Test]
		public void ImageTests_SetScaleThenRect()
		{
			_image.Scale = 2f;
			_image.Position = new Point(10, 20);

			Assert.AreEqual(10, _image.Rect.X);
			Assert.AreEqual(20, _image.Rect.Y);
			Assert.AreEqual(60, _image.Rect.Width);
			Assert.AreEqual(80, _image.Rect.Height);
		}

		[Test]
		public void ImageTests_SetPositionThenScale()
		{
			_image.Position = new Point(10, 20);
			_image.Scale = 2f;

			Assert.AreEqual(10, _image.Rect.X);
			Assert.AreEqual(20, _image.Rect.Y);
			Assert.AreEqual(60, _image.Rect.Width);
			Assert.AreEqual(80, _image.Rect.Height);
		}

		[Test]
		public void ImageTests_SetScaleThenPosition()
		{
			_image.Scale = 2f;
			_image.Position = new Point(10, 20);

			Assert.AreEqual(10, _image.Rect.X);
			Assert.AreEqual(20, _image.Rect.Y);
			Assert.AreEqual(60, _image.Rect.Width);
			Assert.AreEqual(80, _image.Rect.Height);
		}

		#endregion //Scale

		#region Rect & Scale

		[Test]
		public void ShimTests_HorizontalCentered_SetRectThenPadding()
		{
			_image.Horizontal = HorizontalAlignment.Center;
			_image.FillRect = true;
			_image.Position = new Point(10, 20);
			_image.Size = new Vector2(30, 40);

			Assert.AreEqual(-5, _image.Rect.X);
			Assert.AreEqual(20, _image.Rect.Y);
			Assert.AreEqual(30, _image.Rect.Width);
			Assert.AreEqual(40, _image.Rect.Height);
		}

		#endregion //Rect, Padding, & Scale
	}
}
