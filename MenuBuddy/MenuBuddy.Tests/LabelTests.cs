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
			_font = new Mock<IFontBuddy>() { CallBase = true };
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));

			_screen = new Mock<IScreen>();

			_label = new Label("test", _font.Object);
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

		#region Copy

		[Test]
		public void CopyText()
		{
			_label.Text = "buttnuts";
			var copyLabel = new Label(_label as Label);
			Assert.AreEqual("buttnuts", copyLabel.Text);
		}

		//[Test]
		//public void CopyFontSize()
		//{
		//	_label.FontSize = FontSize.Large;
		//	var copyLabel = new Label(_label as Label);
		//	Assert.AreEqual(FontSize.Large, copyLabel.FontSize);
		//}

		[Test]
		public void CopyTextColor()
		{
			_label.TextColor = Color.Pink;
			var copyLabel = new Label(_label as Label);
			Assert.AreEqual(Color.Pink, copyLabel.TextColor);
		}

		[Test]
		public void CopyShadowColor()
		{
			_label.ShadowColor = Color.Pink;
			var copyLabel = new Label(_label as Label);
			Assert.AreEqual(Color.Pink, copyLabel.ShadowColor);
		}

		#endregion //Copy

		#region crappy labels

		[Test]
		public void Empty_Label()
		{
			_label = new Label("", _font.Object);
		}

		[Test]
		public void nullstring_Label()
		{
			string test = null;
			_label = new Label(test, _font.Object);
		}

		[Test]
		public void Empty_Label_move()
		{
			_label = new Label("", _font.Object);
			LabelTests_ChangePosition_CheckPosition();
		}

		[Test]
		public void nullstring_Label_move()
		{
			string test = null;
			_label = new Label(test, _font.Object);
			LabelTests_ChangePosition_CheckPosition();
		}

		#endregion //crappy labels
	}
}
