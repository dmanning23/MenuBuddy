using InputHelper;
using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using ResolutionBuddy;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class GameScreenTests
	{
		[SetUp]
		public void Setup()
		{
			var resolution = new Mock<IResolution>();
			resolution.Setup(x => x.ScreenArea).Returns(new Rectangle(0, 0, 1280, 720));
			Resolution.Init(resolution.Object);
		}

		[Test]
		public void UnderNotActiveGameScreen()
		{
			var screens = new ScreenStack();

			var screen = new Mock<IScreen>();
			screen.Setup(x => x.IsActive).Returns(true);

			var gamescreen = new Mock<IGameScreen>();
			gamescreen.Setup(x => x.TransitionState).Returns(TransitionState.TransitionOn);
			gamescreen.Setup(x => x.CoverOtherScreens).Returns(false);
			gamescreen.Setup(x => x.IsActive).Returns(true);

			screens.Screens.Add(screen.Object);
			screens.Screens.Add(gamescreen.Object);

			var time = new GameTime();
			var input = new Mock<IInputHandler>();
			input.Setup(x => x.HandleInput(It.IsAny<IGameScreen>())).Callback(() => { });
			screens.Update(new GameTime(), input.Object, false);

			gamescreen.Verify(x => x.Update(It.IsAny<GameTime>(),
				It.IsAny<bool>(),
				It.IsAny<bool>()), Times.Once);
			screen.Verify(x => x.Update(It.IsAny<GameTime>(),
				It.IsAny<bool>(),
				It.IsAny<bool>()), Times.Once);
		}

		[Test]
		public void UpdateUnderGameScreen()
		{
			var screens = new ScreenStack();

			var screen = new Mock<IScreen>();
			screen.Setup(x => x.IsActive).Returns(true);

			var gamescreen = new Mock<IGameScreen>();
			gamescreen.Setup(x => x.TransitionState).Returns(TransitionState.Active);
			gamescreen.Setup(x => x.CoverOtherScreens).Returns(false);
			gamescreen.Setup(x => x.IsActive).Returns(true);

			screens.Screens.Add(screen.Object);
			screens.Screens.Add(gamescreen.Object);

			var time = new GameTime();
			var input = new Mock<IInputHandler>();
			input.Setup(x => x.HandleInput(It.IsAny<IGameScreen>())).Callback(() => { });
			screens.Update(new GameTime(), input.Object, false);

			gamescreen.Verify(x => x.Update(It.IsAny<GameTime>(),
				It.IsAny<bool>(),
				It.IsAny<bool>()), Times.Once);
			screen.Verify(x => x.Update(It.IsAny<GameTime>(),
				It.IsAny<bool>(),
				It.IsAny<bool>()), Times.Once);
		}

		[Test]
		public void DrawUnderNotActiveGameScreen()
		{
			var screens = new ScreenStack();

			var screen = new Mock<IScreen>();
			screen.Setup(x => x.IsActive).Returns(true);

			var gamescreen = new Mock<IGameScreen>();
			gamescreen.Setup(x => x.TransitionState).Returns(TransitionState.TransitionOn);
			gamescreen.Setup(x => x.CoverOtherScreens).Returns(false);
			gamescreen.Setup(x => x.IsActive).Returns(true);

			screens.Screens.Add(screen.Object);
			screens.Screens.Add(gamescreen.Object);

			var time = new GameTime();
			screens.Draw(new GameTime());

			gamescreen.Verify(x => x.Draw(It.IsAny<GameTime>()), Times.Once);
			screen.Verify(x => x.Draw(It.IsAny<GameTime>()), Times.Once);
		}

		[Test]
		public void DrawUnderGameScreen()
		{
			var screens = new ScreenStack();

			var screen = new Mock<IScreen>();
			screen.Setup(x => x.IsActive).Returns(true);

			var gamescreen = new Mock<IGameScreen>();
			gamescreen.Setup(x => x.TransitionState).Returns(TransitionState.Active);
			gamescreen.Setup(x => x.CoverOtherScreens).Returns(false);
			gamescreen.Setup(x => x.IsActive).Returns(true);

			screens.Screens.Add(screen.Object);
			screens.Screens.Add(gamescreen.Object);

			var time = new GameTime();
			screens.Draw(new GameTime());

			gamescreen.Verify(x => x.Draw(It.IsAny<GameTime>()), Times.Once);
			screen.Verify(x => x.Draw(It.IsAny<GameTime>()), Times.Once);
		}

		[Test]
		public void DrawUnderWidgetScreen_ScreensCorrect_1()
		{
			var screens = new ScreenStack();

			//create the screen that will be on the bottom
			var bottomScreen = new Mock<WidgetScreen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			//create the screen that will cover it
			var menuScreen = new Mock<WidgetScreen>("menuScreen") { CallBase = true };
			menuScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			menuScreen.Setup(x => x.CoverOtherScreens).Returns(true);
			menuScreen.Setup(x => x.IsExiting).Returns(false);
			menuScreen.Setup(x => x.CheckClick(It.IsAny<ClickEventArgs>())).Returns(false);

			//add to the screen manager
			screens.Screens.Add(bottomScreen.Object);
			screens.Screens.Add(menuScreen.Object);

			//create the fake mouse handler
			var mouseClicks = new List<ClickEventArgs>() { new ClickEventArgs(Vector2.Zero, MouseButton.Left, null) };
			var mouse = new Mock<IInputHelper>();
			mouse.Setup(x => x.Clicks).Returns(mouseClicks);
			var mouseHandler = new MouseScreenInputChecker(mouse.Object);

			//update the whole screen stack
			screens.Update(new GameTime(), mouseHandler, false);

			//Verify that the screen states are correct
			Assert.IsTrue(bottomScreen.Object.IsActive);
		}

		[Test]
		public void DrawUnderWidgetScreen_ScreensCorrect_2()
		{
			var screens = new ScreenStack();

			//create the screen that will be on the bottom
			var bottomScreen = new Mock<WidgetScreen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			//create the screen that will cover it
			var menuScreen = new Mock<WidgetScreen>("menuScreen") { CallBase = true };
			menuScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			menuScreen.Setup(x => x.CoverOtherScreens).Returns(true);
			menuScreen.Setup(x => x.IsExiting).Returns(false);
			menuScreen.Setup(x => x.CheckClick(It.IsAny<ClickEventArgs>())).Returns(false);

			//add to the screen manager
			screens.Screens.Add(bottomScreen.Object);
			screens.Screens.Add(menuScreen.Object);

			//create the fake mouse handler
			var mouseClicks = new List<ClickEventArgs>() { new ClickEventArgs(Vector2.Zero, MouseButton.Left, null) };
			var mouse = new Mock<IInputHelper>();
			mouse.Setup(x => x.Clicks).Returns(mouseClicks);
			var mouseHandler = new MouseScreenInputChecker(mouse.Object);

			//update the whole screen stack
			screens.Update(new GameTime(), mouseHandler, false);

			//Verify that the screen states are correct
			Assert.IsTrue(menuScreen.Object.IsActive);
		}

		[Test]
		public void DrawUnderWidgetScreen_Covered()
		{
			var screens = new ScreenStack();

			//create the screen that will be on the bottom
			var bottomScreen = new Mock<WidgetScreen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			//create the screen that will cover it
			var menuScreen = new Mock<WidgetScreen>("menuScreen") { CallBase = true };
			menuScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			menuScreen.Setup(x => x.CoverOtherScreens).Returns(true);
			menuScreen.Setup(x => x.IsExiting).Returns(false);
			menuScreen.Setup(x => x.CheckClick(It.IsAny<ClickEventArgs>())).Returns(false);

			//add to the screen manager
			screens.Screens.Add(bottomScreen.Object);
			screens.Screens.Add(menuScreen.Object);

			//create the fake mouse handler
			var mouseClicks = new List<ClickEventArgs>() { new ClickEventArgs(Vector2.Zero, MouseButton.Left, null) };
			var mouse = new Mock<IInputHelper>();
			mouse.Setup(x => x.Clicks).Returns(mouseClicks);
			var mouseHandler = new MouseScreenInputChecker(mouse.Object);

			//update the whole screen stack
			screens.Update(new GameTime(), mouseHandler, false);

			//Verify that the click was caught by the bottom screen
			bottomScreen.Verify(x => x.CheckClick(It.IsAny<ClickEventArgs>()), Times.Never);
		}

		[Test]
		public void DrawUnderWidgetScreen_AcceptsClicks()
		{
			var screens = new ScreenStack();

			//create the screen that will be on the bottom
			var bottomScreen = new Mock<WidgetScreen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			//create the screen that will cover it
			var menuScreen = new Mock<WidgetScreen>("menuScreen") { CallBase = true };
			menuScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			menuScreen.Setup(x => x.CoverOtherScreens).Returns(true);
			menuScreen.Setup(x => x.IsExiting).Returns(false);
			menuScreen.Setup(x => x.CheckClick(It.IsAny<ClickEventArgs>())).Returns(false);

			//add to the screen manager
			screens.Screens.Add(bottomScreen.Object);
			screens.Screens.Add(menuScreen.Object);

			//create the fake mouse handler
			var mouseClicks = new List<ClickEventArgs>() { new ClickEventArgs(Vector2.Zero, MouseButton.Left, null) };
			var mouse = new Mock<IInputHelper>();
			mouse.Setup(x => x.Clicks).Returns(mouseClicks);
			var mouseHandler = new MouseScreenInputChecker(mouse.Object);

			//update the whole screen stack
			screens.Update(new GameTime(), mouseHandler, false);

			//Verify that the click was caught by the bottom screen
			bottomScreen.Verify(x => x.CheckClick(It.IsAny<ClickEventArgs>()), Times.Once);
		}

		[Test]
		public void HasFocus_OtherWindowHasFocus()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), true, true);
			Assert.IsFalse(bottomScreen.Object.HasFocus);
		}

		[Test]
		public void HasFocus_OtherScreenOnTop()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), false, true);
			Assert.IsFalse(bottomScreen.Object.HasFocus);
		}

		[Test]
		public void HasFocus_OnTop()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), false, false);
			Assert.IsTrue(bottomScreen.Object.HasFocus);
		}

		[Test]
		public void IsActive_OtherWindowHasFocus()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), true, true);
			Assert.IsFalse(bottomScreen.Object.IsActive);
		}

		[Test]
		public void IsActive_OtherScreenOnTop()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), false, true);
			Assert.IsFalse(bottomScreen.Object.IsActive);
		}

		[Test]
		public void IsActive_DontCareAboutOtherScreenOnTop()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), false, true);
			Assert.IsTrue(bottomScreen.Object.IsActive);
		}

		[Test]
		public void IsActive_OnTop()
		{
			//create the screen that will be on the bottom
			var bottomScreen = new Mock<Screen>("bottomScreen") { CallBase = true };
			bottomScreen.Setup(x => x.CoveredByOtherScreens).Returns(true);
			bottomScreen.Setup(x => x.CoverOtherScreens).Returns(false);
			bottomScreen.Setup(x => x.IsExiting).Returns(false);

			bottomScreen.Object.Update(new GameTime(), false, false);
			Assert.IsTrue(bottomScreen.Object.IsActive);
		}
	}
}
