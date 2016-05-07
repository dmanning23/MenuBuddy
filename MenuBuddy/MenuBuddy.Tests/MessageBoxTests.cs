using FontBuddyLib;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class MessageBoxTests
	{
		#region Fields

		private Mock<MessageBoxScreen> _screen;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			StyleSheet.InitUnitTests();

			var font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(120f, 1500f));
			StyleSheet.Instance().LargeHighlightedFont = font.Object;
			StyleSheet.Instance().LargeNeutralFont = font.Object;

			font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(70f, 80f));
			StyleSheet.Instance().MediumHighlightedFont= font.Object;
			StyleSheet.Instance().MediumNeutralFont = font.Object;

			font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			StyleSheet.Instance().SmallHighlightedFont = font.Object;
			StyleSheet.Instance().SmallNeutralFont = font.Object;

			_screen = new Mock<MessageBoxScreen>("test", "catpants") { CallBase = true };
			_screen.Setup(x => x.AddBackgroundImage(It.IsAny<ILayout>())).Callback(() => { });
			_screen.Object.LoadContent();
        }

		#endregion //Setup

		#region Tests

		[Test]
		public void Default()
		{
		}

		[Test]
		public void Selected_Index()
		{
			Assert.AreEqual(0, _screen.Object.SelectedIndex);
		}

		[Test]
		public void Selected_Button()
		{
			Assert.NotNull(_screen.Object.SelectedEntry);
		}

		#endregion //Tests
	}
}