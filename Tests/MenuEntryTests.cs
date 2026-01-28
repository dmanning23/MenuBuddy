using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using Moq;
using FontBuddyLib;
using Microsoft.Xna.Framework;
using ResolutionBuddy;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class MenuEntryTests
	{
		#region Fields

		private Mock<IScreen> _screen;
		private MenuEntry _entry;
		private IFontBuddy _font;

		#endregion //Fields

		#region Setup

		[SetUp]
		public async Task Setup()
		{
			var resolution = new Mock<IResolution>();
			resolution.Setup(x => x.ScreenArea).Returns(new Rectangle(0, 0, 1280, 720));
			Resolution.Init(resolution.Object);

			var font = new Mock<IFontBuddy>() { CallBase = true };
			font.Setup(x => x.MeasureString(It.IsAny<string>()))
				.Returns(new Vector2(30f, 40f));
			_font = font.Object;

			_screen = new Mock<IScreen>();

			_entry = new MenuEntry("test", _font);
			await _entry.LoadContent(_screen.Object);
		}

		#endregion //Setup

		#region Defaults

		[Test]
		public void MenuEntryTests_Default()
		{
			_entry.Position.X.ShouldBe(0);
			_entry.Position.Y.ShouldBe(0);
			_entry.Rect.X.ShouldBe(-384);
			_entry.Rect.Y.ShouldBe(0);
			_entry.Rect.Width.ShouldBe(768);
			_entry.Rect.Height.ShouldBe(40);
			_entry.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_entry.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void MenuEntryTests_Label_Default()
		{
			_entry.Label.Position.X.ShouldBe(0);
			_entry.Label.Position.Y.ShouldBe(0);
			_entry.Label.Rect.X.ShouldBe(-15);
			_entry.Label.Rect.Y.ShouldBe(0);
			_entry.Label.Rect.Width.ShouldBe(30);
			_entry.Label.Rect.Height.ShouldBe(40);
			_entry.Label.Horizontal.ShouldBe(HorizontalAlignment.Center);
			_entry.Label.Vertical.ShouldBe(VerticalAlignment.Top);
		}

		[Test]
		public void MenuEntryTests_Layout_Scale()
		{
			_entry.Scale = .5f;

			_entry.Layout.Rect.Width.ShouldBe(384);
			_entry.Layout.Rect.Height.ShouldBe(20);
		}

		[Test]
		public void MenuEntryTests_LabelScaled()
		{
			_entry.Scale = .5f;
			_entry.Label.Scale.ShouldBe(.5f);
		}

		[Test]
		public void MenuEntryTests_Label_Scale()
		{
			_entry.Scale = .5f;

			_entry.Rect.Width.ShouldBe(384);
			_entry.Rect.Height.ShouldBe(20);

			_entry.Label.Scale.ShouldBe(.5f);
			_entry.Label.Rect.Width.ShouldBe(15);
			_entry.Label.Rect.Height.ShouldBe(20);
		}

		#endregion //Defaults
	}
}
