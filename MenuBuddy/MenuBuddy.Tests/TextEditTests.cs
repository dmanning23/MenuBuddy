using FontBuddyLib;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy
{
	[TestFixture]
	public class TextEditTests
	{
		#region Fields

		private Mock<IScreen> _screen;
		private ITextEdit _text;
		private IFontBuddy _font;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void LabelTests_Setup()
		{
			var font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			_font = font.Object;

			_screen = new Mock<IScreen>();

			_text = new TextEdit("test", _font);
		}

		#endregion //Setup

		#region Rect & Position 

		[Test]
		public void LabelTests_ChangePosition_CheckPosition()
		{
			_text.Position = new Point(50, 60);

			Assert.AreEqual(50, _text.Rect.X);
			Assert.AreEqual(60, _text.Rect.Y);
		}

		#endregion //Rect & Position 

		#region crappy labels

		[Test]
		public void Empty_Label()
		{
			_text = new TextEdit("", _font);
		}

		[Test]
		public void nullstring_Label()
		{
			string test = null;
			_text = new TextEdit(test, _font);
		}

		[Test]
		public void Empty_Label_move()
		{
			_text = new TextEdit("", _font);
			LabelTests_ChangePosition_CheckPosition();
		}

		[Test]
		public void nullstring_Label_move()
		{
			string test = null;
			_text = new TextEdit(test, _font);
			LabelTests_ChangePosition_CheckPosition();
		}

		#endregion //crappy labels
	}
}
