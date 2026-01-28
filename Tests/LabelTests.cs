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
			_label.Rect.X.ShouldBe(0);
			_label.Rect.Y.ShouldBe(0);
			_label.Rect.Width.ShouldBe(30);
			_label.Rect.Height.ShouldBe(40);
		}

		[Test]
		public void LabelTests_NullPosition()
		{
			_label.Rect.X.ShouldBe(0);
			_label.Rect.Y.ShouldBe(0);
		}

		#endregion //Defaults

		#region Rect & Position 

		[Test]
		public void LabelTests_ChangePosition_CheckPosition()
		{
			_label.Position = new Point(50, 60);

			_label.Rect.X.ShouldBe(50);
			_label.Rect.Y.ShouldBe(60);
		}

		[Test]
		public void LabelTests_ChangePosition_CheckRect()
		{
			_label.Position = new Point(10, 20);

			_label.Rect.X.ShouldBe(10);
			_label.Rect.Y.ShouldBe(20);
			_label.Rect.Width.ShouldBe(30);
			_label.Rect.Height.ShouldBe(40);
		}

		[Test]
		public void LabelTests_ChangeText_CheckRect()
		{
			_label.Position = new Point(10, 20);
			_font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(50f, 60f));
			_label.Text = "buttnuts";

			_label.Rect.Width.ShouldBe(50);
			_label.Rect.Height.ShouldBe(60);
		}

		#endregion //Rect & Position 

		#region Scale

		[Test]
		public void LabelTests_SetRectThenScale()
		{
			_label.Position = new Point(10, 20);
			_label.Scale = 2.0f;

			_label.Rect.X.ShouldBe(10);
			_label.Rect.Y.ShouldBe(20);
			_label.Rect.Width.ShouldBe(60);
			_label.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void LabelTests_SetScaleThenRect()
		{
			_label.Scale = 2f;
			_label.Position = new Point(10, 20);

			_label.Rect.X.ShouldBe(10);
			_label.Rect.Y.ShouldBe(20);
			_label.Rect.Width.ShouldBe(60);
			_label.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void LabelTests_SetPositionThenScale()
		{
			_label.Position = new Point(10, 20);
			_label.Scale = 2f;

			_label.Rect.X.ShouldBe(10);
			_label.Rect.Y.ShouldBe(20);
			_label.Rect.Width.ShouldBe(60);
			_label.Rect.Height.ShouldBe(80);
		}

		[Test]
		public void LabelTests_SetScaleThenPosition()
		{
			_label.Scale = 2f;
			_label.Position = new Point(10, 20);

			_label.Rect.X.ShouldBe(10);
			_label.Rect.Y.ShouldBe(20);
			_label.Rect.Width.ShouldBe(60);
			_label.Rect.Height.ShouldBe(80);
		}

		#endregion //Scale

		#region Copy

		[Test]
		public void CopyText()
		{
			_label.Text = "buttnuts";
			var copyLabel = new Label(_label as Label);
			copyLabel.Text.ShouldBe("buttnuts");
		}

		//[Test]
		//public void CopyFontSize()
		//{
		//	_label.FontSize = FontSize.Large;
		//	var copyLabel = new Label(_label as Label);
		//	copyLabel.FontSize.ShouldBe(FontSize.Large);
		//}

		[Test]
		public void CopyTextColor()
		{
			_label.TextColor = Color.Pink;
			var copyLabel = new Label(_label as Label);
			copyLabel.TextColor.ShouldBe(Color.Pink);
		}

		[Test]
		public void CopyShadowColor()
		{
			_label.ShadowColor = Color.Pink;
			var copyLabel = new Label(_label as Label);
			copyLabel.ShadowColor.ShouldBe(Color.Pink);
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
