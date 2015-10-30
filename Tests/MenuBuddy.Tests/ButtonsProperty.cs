using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class ButtonsProperty
	{
		[SetUp]
		public void ButtonsProperty_Setup()
		{
			DefaultStyles.InitUnitTests();
			var menuStyles = new StyleSheet();
			DefaultStyles.Instance().MainStyle = menuStyles;
		}

        [Test]
		public void LayoutWithButton()
		{
			var layout = new Mock<Layout>() { CallBase = true };

			layout.Object.Items.Add(new RelativeLayoutButton());
			var buttons = layout.Object.Buttons;
			Assert.AreEqual(1, buttons.Count());
		}

		[Test]
		public void NestedLayoutWithButton()
		{
			var layout = new Mock<Layout>() { CallBase = true };

			var secondLayout = new Mock<Layout>() { CallBase = true };
			secondLayout.Object.Items.Add(new RelativeLayoutButton());
			layout.Object.Items.Add(secondLayout.Object);

			var buttons = layout.Object.Buttons;
			Assert.AreEqual(1, buttons.Count());
		}

		[Test]
		public void ScreenWithButton()
		{
			var layout = new Mock<Layout>() { CallBase = true };

			var secondLayout = new Mock<Layout>() { CallBase = true };
			secondLayout.Object.Items.Add(new RelativeLayoutButton());
			layout.Object.Items.Add(secondLayout.Object);

			var screen = new Mock<WidgetScreen>("test screen") { CallBase = true };
			screen.Object.AddItem(layout.Object);

			var buttons = screen.Object.Buttons;
			Assert.AreEqual(1, buttons.Count());
		}
	}
}
