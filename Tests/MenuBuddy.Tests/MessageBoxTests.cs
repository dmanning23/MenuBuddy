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
			DefaultStyles.InitUnitTests();

			var mainstyle = new StyleSheet("main");
			var font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(70f, 80f));
			mainstyle.SelectedFont = font.Object;
			DefaultStyles.Instance().MainStyle = mainstyle;

			var menuStyles = new StyleSheet("MenuEntryStyle");
            font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = font.Object;
			DefaultStyles.Instance().MenuEntryStyle = menuStyles;

			menuStyles = new StyleSheet("MenuTitleStyle");
            font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = font.Object;
			DefaultStyles.Instance().MenuTitleStyle = menuStyles;

			menuStyles = new StyleSheet("MessageBoxStyle");
            font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = font.Object;
			menuStyles.UnselectedFont = font.Object;
			DefaultStyles.Instance().MessageBoxStyle = menuStyles;

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