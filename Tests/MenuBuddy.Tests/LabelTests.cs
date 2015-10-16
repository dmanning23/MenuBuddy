using Moq;
using FontBuddyLib;
using Microsoft.Xna.Framework;
using NUnit.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class LabelTests
	{
		#region Fields

		private Mock<IFontBuddy> _font;
		private Mock<IScreen> _screen;
		private ILabel _label;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void LabelTests_Setup()
		{
			DefaultStyles.InitUnitTests();
			var menuStyles = new StyleSheet();
			DefaultStyles.Instance().MainStyle = menuStyles;

			_font = new Mock<IFontBuddy>() { CallBase = true };
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = _font.Object;

			_screen = new Mock<IScreen>();

			_label = new Label("test");
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void LabelTests_NullRect()
		{
			Assert.AreEqual(0, _label.Rect.X);
			Assert.AreEqual(0, _label.Rect.Y);
			Assert.AreEqual(30, _label.Rect.Width);
			Assert.AreEqual(40, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_NullPosition()
		{
			Assert.AreEqual(0, _label.Rect.X);
			Assert.AreEqual(0, _label.Rect.Y);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void LabelTests_ChangePosition_CheckPosition()
		{
			_label.Position = new Point(50, 60);

			Assert.AreEqual(50, _label.Rect.X);
			Assert.AreEqual(60, _label.Rect.Y);
		}

		[Test]
		public void LabelTests_ChangePosition_CheckRect()
		{
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(30, _label.Rect.Width);
			Assert.AreEqual(40, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_ChangeText_CheckRect()
		{
			_label.Position = new Point(10, 20);
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(50f, 60f));
			_label.Text = "buttnuts";

			Assert.AreEqual(50, _label.Rect.Width);
			Assert.AreEqual(60, _label.Rect.Height);
		}

		#endregion //Rect & Position 

		#region Padding

		[Test]
		public void LabelTests_SetRectThenPadding()
		{
			_label.Position = new Point(10, 20);
			_label.Padding = new Vector2(10, 5);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(50, _label.Rect.Width);
			Assert.AreEqual(50, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetPaddingThenRect()
		{
			_label.Padding = new Vector2(10, 5);
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(50, _label.Rect.Width);
			Assert.AreEqual(50, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetPositionThenPadding()
		{
			_label.Position = new Point(10, 20);
			_label.Padding = new Vector2(10, 5);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(50, _label.Rect.Width);
			Assert.AreEqual(50, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetPaddingThenPosition()
		{
			_label.Padding = new Vector2(30, 50);
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(90, _label.Rect.Width);
			Assert.AreEqual(140, _label.Rect.Height);
		}

		#endregion //Padding

		#region Scale

		[Test]
		public void LabelTests_SetRectThenScale()
		{
			_label.Position = new Point(10, 20);
			_label.Scale = 2.0f;

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(60, _label.Rect.Width);
			Assert.AreEqual(80, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetScaleThenRect()
		{
			_label.Scale = 2f;
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(60, _label.Rect.Width);
			Assert.AreEqual(80, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetPositionThenScale()
		{
			_label.Position = new Point(10, 20);
			_label.Scale = 2f;

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(60, _label.Rect.Width);
			Assert.AreEqual(80, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetScaleThenPosition()
		{
			_label.Scale = 2f;
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(60, _label.Rect.Width);
			Assert.AreEqual(80, _label.Rect.Height);
		}

		#endregion //Scale

		#region Rect, Padding, & Scale

		[Test]
		public void LabelTests_SetRectThenPaddingThenScale()
		{
			_label.Position = new Point(10, 20);
			_label.Padding = new Vector2(10, 20);
			_label.Scale = 2f;

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(100, _label.Rect.Width);
			Assert.AreEqual(160, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetRectThenScaleThenPadding()
		{
			_label.Position = new Point(10, 20);
			_label.Scale = 2f;
			_label.Padding = new Vector2(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(100, _label.Rect.Width);
			Assert.AreEqual(160, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetPaddingThenRectThenScale()
		{
			_label.Padding = new Vector2(10, 20);
			_label.Position = new Point(10, 20);
			_label.Scale = 2f;

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(100, _label.Rect.Width);
			Assert.AreEqual(160, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetPaddingThenScaleThenRect()
		{
			_label.Padding = new Vector2(10, 20);
			_label.Scale = 2f;
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(100, _label.Rect.Width);
			Assert.AreEqual(160, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetScaleThenPaddingThenRect()
		{
			_label.Scale = 2f;
			_label.Padding = new Vector2(10, 20);
			_label.Position = new Point(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(100, _label.Rect.Width);
			Assert.AreEqual(160, _label.Rect.Height);
		}

		[Test]
		public void LabelTests_SetScaleThenRectThenPadding()
		{
			_label.Scale = 2f;
			_label.Position = new Point(10, 20);
			_label.Padding = new Vector2(10, 20);

			Assert.AreEqual(10, _label.Rect.X);
			Assert.AreEqual(20, _label.Rect.Y);
			Assert.AreEqual(100, _label.Rect.Width);
			Assert.AreEqual(160, _label.Rect.Height);
		}

		#endregion //Rect, Padding, & Scale
	}
}
