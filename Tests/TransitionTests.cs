using GameTimer;
using Microsoft.Xna.Framework;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MenuBuddy.Tests
{
	[TestFixture]
	public class TransitionTests
	{
		[Test]
		public void DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			transition.TransitionPosition.ShouldBe(0f);
		}

		[Test]
		public void TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false);
			transition.TransitionPosition.ShouldBe(1f);
		}

		[Test]
		public void TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			transition.TransitionPosition.ShouldBe(1f);
		}

		[Test]
		public void HalfTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			transition.Update(clock, false);
			transition.TransitionPosition.ShouldBe(.5f);
		}

		[Test]
		public void HalfTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			transition.Update(clock, true);
			transition.TransitionPosition.ShouldBe(.5f);
		}

		[Test]
		public void QuarterTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 250), new TimeSpan(0, 0, 0, 0, 250)));

			transition.Update(clock, false);
			transition.TransitionPosition.ShouldBe(.25f);
		}

		[Test]
		public void QuarterTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 250), new TimeSpan(0, 0, 0, 0, 250)));

			transition.Update(clock, true);
			transition.TransitionPosition.ShouldBe(.75f);
		}

		[Test]
		public void Alpha_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			transition.Alpha.ShouldBe(1f);
		}

		[Test]
		public void Alpha_TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false);
			transition.Alpha.ShouldBe(0f);
		}

		[Test]
		public void Alpha_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			transition.Alpha.ShouldBe(0f);
		}

		[Test]
		public void Alpha_NoneTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), false);
			transition.Alpha.ShouldBe(1f);
		}

		[Test]
		public void Alpha_NoneTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), true);
			transition.Alpha.ShouldBe(0f);
		}

		[Test]
		public void AlphaColor_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			var target = new Color(255, 255, 255, 255);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void AlphaColor_TransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false);
			var target = new Color(255, 255, 255, 0);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void AlphaColor_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			var target = new Color(255, 255, 255, 0);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void AlphaColor_NoneTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), false);
			var target = new Color(255, 255, 255, 255);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void AlphaColor_NoneTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), true);
			var target = new Color(255, 255, 255, 0);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void HalfAlphaColor_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			var color = new Color(255, 255, 255, 127);
			var target = new Color(255, 255, 255, 127);
			transition.AlphaColor(color).ShouldBe(target);
		}

		[Test]
		public void HalfAlphaColor_TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false);
			var color = new Color(255, 255, 255, 127);
			var target = new Color(255, 255, 255, 0);
			transition.AlphaColor(color).ShouldBe(target);
		}

		[Test]
		public void HalfAlphaColor_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			var color = new Color(255, 255, 255, 127);
			var target = new Color(255, 255, 255, 0);
			transition.AlphaColor(color).ShouldBe(target);
		}

		[Test]
		public void Alpha_HalfTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			transition.Update(clock, false);
			var target = new Color(255, 255, 255, 127);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void Alpha_HalfTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			transition.Update(clock, true);
			var target = new Color(255, 255, 255, 127);
			transition.AlphaColor(Color.White).ShouldBe(target);
		}

		[Test]
		public void AlphaColor_HalfTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			transition.Update(clock, false);
			var color = new Color(255, 255, 255, 255);
			var target = new Color(255, 255, 255, 127);
			transition.AlphaColor(color).ShouldBe(target);
		}

		[Test]
		public void AlphaColor_HalfTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			var clock = new GameClock();
			clock.Update(new GameTime(new TimeSpan(0, 0, 0, 0, 500), new TimeSpan(0, 0, 0, 0, 500)));

			transition.Update(clock, true);
			var color = new Color(255, 255, 255, 255);
			var target = new Color(255, 255, 255, 127);
			transition.AlphaColor(color).ShouldBe(target);
		}

		#region return values

		[Test]
		public void CorrectReturnValue_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true).ShouldBeFalse();
		}

		[Test]
		public void CorrectReturnValue_TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false).ShouldBeFalse();
		}

		[Test]
		public void CorrectReturnValue_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true).ShouldBeTrue();
		}

		[Test]
		public void CorrectReturnValue_HalfTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), false).ShouldBeTrue();
		}

		[Test]
		public void CorrectReturnValue_HalfTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			transition.Update(new GameClock(), true).ShouldBeTrue();
		}

		[Test]
		public void CorrectReturnValue_QuarterTransitionOff()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), false).ShouldBeTrue();
		}

		[Test]
		public void CorrectReturnValue_QuarterTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			transition.Update(new GameClock(), true).ShouldBeTrue();
		}

		#endregion //return values

		#region transition state

		[Test]
		public void DefaultState()
		{
			var transition = new ScreenTransition();
			transition.State.ShouldBe(TransitionState.Hidden);
		}

		[Test]
		public void active_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.StateChange(false).ShouldBeTrue();
		}

		[Test]
		public void on_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOn
			};
			transition.StateChange(false).ShouldBeTrue();
		}

		[Test]
		public void hidden_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Hidden
			};
			transition.StateChange(false).ShouldBeFalse();
		}

		[Test]
		public void off_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOff
			};
			transition.StateChange(false).ShouldBeFalse();
		}

		[Test]
		public void active_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			transition.StateChange(true).ShouldBeFalse();
		}

		[Test]
		public void on_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOn
			};
			transition.StateChange(true).ShouldBeFalse();
		}

		[Test]
		public void hidden_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Hidden
			};
			transition.StateChange(true).ShouldBeTrue();
		}

		[Test]
		public void off_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOff
			};
			transition.StateChange(true).ShouldBeTrue();
		}

		[Test]
		public void off_on_update()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOff
			};
			transition.Update(new GameClock(), true).ShouldBeTrue();
		}

		[Test]
		public void StateEvent()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Hidden
			};
			bool stateChanged = false;
			transition.OnStateChange += (obj, e) =>
			{
				stateChanged = true;
			};
			transition.Update(new GameClock(), true);
			stateChanged.ShouldBeTrue();
		}

		#endregion //transition state
	}
}
