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
	public class GameScreenTests
	{
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
			var input = new Mock<IInputHelper>();
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
		public void DontUpdateUnderGameScreen()
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
			var input = new Mock<IInputHelper>();
			input.Setup(x => x.HandleInput(It.IsAny<IGameScreen > ())).Callback(() => { });
			screens.Update(new GameTime(), input.Object, false);

			gamescreen.Verify(x => x.Update(It.IsAny<GameTime>(), 
				It.IsAny<bool>(), 
				It.IsAny<bool>()), Times.Once);
			screen.Verify(x => x.Update(It.IsAny<GameTime>(),
				It.IsAny<bool>(),
				It.IsAny<bool>()), Times.Never);
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
		public void DontDrawUnderGameScreen()
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
			screen.Verify(x => x.Draw(It.IsAny<GameTime>()), Times.Never);
		}
	}
}
