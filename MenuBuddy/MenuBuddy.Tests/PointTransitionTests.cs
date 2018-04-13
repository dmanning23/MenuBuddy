using Microsoft.Xna.Framework;
using Moq;
using NUnit.Framework;
using Shouldly;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class PointTransitionTests
	{
		PointTransitionObject Transition { get; set; }
		Vector2 FinalPosition { get; set; }
		Mock<IScreenTransition> screenTransition;
		Mock<IScreen> screen;

		[SetUp]
		public void Setup()
		{
			screenTransition = new Mock<IScreenTransition>();
			Transition = new PointTransitionObject(new Vector2(0f), screenTransition.Object);
			FinalPosition = new Vector2(10f);
			screen = new Mock<IScreen>();
			screen.Setup(x => x.Transition).Returns(screenTransition.Object);
		}

		[Test]
		public void StartTransition()
		{
			screenTransition.Setup(x => x.TransitionPosition).Returns(0.99f);

			var result = Transition.Position(screen.Object, FinalPosition);
			result.X.ShouldBeLessThanOrEqualTo(1f);

		}

		[Test]
		public void EndTransition()
		{
			screenTransition.Setup(x => x.TransitionPosition).Returns(0.01f);

			var result = Transition.Position(screen.Object, FinalPosition);
			result.X.ShouldBeGreaterThanOrEqualTo(9.9f);
		}

		[Test]
		public void JustStart()
		{
			screenTransition.Setup(x => x.TransitionPosition).Returns(0.9f);

			var result = Transition.Position(screen.Object, FinalPosition);
			result.X.ShouldBeLessThanOrEqualTo(2f);
			result.X.ShouldBeGreaterThanOrEqualTo(0.9f);
		}

		[Test]
		public void MidTransition()
		{
			screenTransition.Setup(x => x.TransitionPosition).Returns(0.5f);

			var result = Transition.Position(screen.Object, FinalPosition);
			result.X.ShouldBeGreaterThanOrEqualTo(5f);
			result.X.ShouldBeLessThanOrEqualTo(8f);
		}

		[Test]
		public void AlmostDone()
		{
			var screenTransition = new Mock<IScreenTransition>();
			screenTransition.Setup(x => x.TransitionPosition).Returns(0.1f);

			var result = Transition.Position(screen.Object, FinalPosition);
			result.X.ShouldBeLessThanOrEqualTo(10f);
			result.X.ShouldBeGreaterThanOrEqualTo(9.1f);
		}
	}
}
