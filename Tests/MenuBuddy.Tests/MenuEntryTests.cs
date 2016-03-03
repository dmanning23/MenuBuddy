using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FontBuddyLib;
using Microsoft.Xna.Framework;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class MenuEntryTests
	{
		#region Fields

		private Mock<IScreen> _screen;
		private MenuEntry _entry;

		#endregion //Fields

		#region Setup

		[SetUp]
		public void Setup()
		{
			DefaultStyles.InitUnitTests();

			var mainstyle = new StyleSheet();
			var font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(70f, 80f));
			mainstyle.SelectedFont = font.Object;
			DefaultStyles.Instance().MainStyle = mainstyle;

			var menuStyles = new StyleSheet();
			font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			menuStyles.SelectedFont = font.Object;
			DefaultStyles.Instance().MenuEntryStyle = menuStyles;

			_screen = new Mock<IScreen>();

			_entry = new MenuEntry("test");
			_entry.LoadContent(_screen.Object);
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void MenuEntryTests_Default()
		{
			Assert.AreEqual(0, _entry.Position.X);
			Assert.AreEqual(0, _entry.Position.Y);
			Assert.AreEqual(-384, _entry.Rect.X);
			Assert.AreEqual(0, _entry.Rect.Y);
			Assert.AreEqual(768, _entry.Rect.Width);
			Assert.AreEqual(40, _entry.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _entry.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _entry.Vertical);
		}

		[Test]
		public void MenuEntryTests_Label_Default()
		{
			Assert.AreEqual(0f, _entry.Label.Position.X);
			Assert.AreEqual(0f, _entry.Label.Position.Y);
			Assert.AreEqual(-15f, _entry.Label.Rect.X);
			Assert.AreEqual(0, _entry.Label.Rect.Y);
			Assert.AreEqual(30f, _entry.Label.Rect.Width);
			Assert.AreEqual(40f, _entry.Label.Rect.Height);
			Assert.AreEqual(HorizontalAlignment.Center, _entry.Label.Horizontal);
			Assert.AreEqual(VerticalAlignment.Top, _entry.Label.Vertical);
		}

		[Test]
		public void MenuEntryTests_Layout_Scale()
		{
			_entry.Scale = .5f;

			Assert.AreEqual(384, _entry.Layout.Rect.Width);
			Assert.AreEqual(20, _entry.Layout.Rect.Height);
		}

		[Test]
		public void MenuEntryTests_LabelScaled()
		{
			_entry.Scale = .5f;
			Assert.AreEqual(.5f, _entry.Label.Scale);
		}

		[Test]
		public void MenuEntryTests_Label_Scale()
		{
			_entry.Scale = .5f;

			Assert.AreEqual(384, _entry.Rect.Width);
			Assert.AreEqual(20, _entry.Rect.Height);

			Assert.AreEqual(.5f, _entry.Label.Scale);
			Assert.AreEqual(15f, _entry.Label.Rect.Width);
			Assert.AreEqual(20f, _entry.Label.Rect.Height);
		}

		#endregion //Defaults
	}
}
