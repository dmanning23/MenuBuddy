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
			Assert.AreEqual(0f, transition.TransitionPosition);
		}

		[Test]
		public void TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false);
			Assert.AreEqual(1f, transition.TransitionPosition);
		}

		[Test]
		public void TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			Assert.AreEqual(1f, transition.TransitionPosition);
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
			Assert.AreEqual(.5f, transition.TransitionPosition);
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
			Assert.AreEqual(.5f, transition.TransitionPosition);
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
			Assert.AreEqual(.25f, transition.TransitionPosition);
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
			Assert.AreEqual(.75f, transition.TransitionPosition);
		}

		[Test]
		public void Alpha_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			Assert.AreEqual(1f, transition.Alpha);
		}

		[Test]
		public void Alpha_TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), false);
			Assert.AreEqual(0f, transition.Alpha);
		}

		[Test]
		public void Alpha_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			Assert.AreEqual(0f, transition.Alpha);
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
			Assert.AreEqual(1f, transition.Alpha);
		}

		[Test]
		public void Alpha_NoneTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), true);
			Assert.AreEqual(0f, transition.Alpha);
		}

		[Test]
		public void AlphaColor_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			var target = new Color(255, 255, 255, 255);
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
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
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
		}

		[Test]
		public void AlphaColor_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			transition.Update(new GameClock(), true);
			var target = new Color(255, 255, 255, 0);
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
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
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
		}

		[Test]
		public void AlphaColor_NoneTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 1f;

			transition.Update(new GameClock(), true);
			var target = new Color(255, 255, 255, 0);
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
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
			Assert.AreEqual(target, transition.AlphaColor(color));
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
			Assert.AreEqual(target, transition.AlphaColor(color));
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
			Assert.AreEqual(target, transition.AlphaColor(color));
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
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
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
			Assert.AreEqual(target, transition.AlphaColor(Color.White));
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
			Assert.AreEqual(target, transition.AlphaColor(color));
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
			Assert.AreEqual(target, transition.AlphaColor(color));
		}

		#region return values

		[Test]
		public void CorrectReturnValue_DoneTransitioning()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			Assert.IsFalse(transition.Update(new GameClock(), true));
		}

		[Test]
		public void CorrectReturnValue_TransitionOff()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 0f;
			transition.OffTime = 0f;

			Assert.IsFalse(transition.Update(new GameClock(), false));
		}

		[Test]
		public void CorrectReturnValue_TransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OffTime = 0f;

			Assert.IsTrue(transition.Update(new GameClock(), true));
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

			Assert.IsTrue(transition.Update(new GameClock(), false));
		}

		[Test]
		public void CorrectReturnValue_HalfTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			Assert.IsTrue(transition.Update(new GameClock(), true));
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

			Assert.IsTrue(transition.Update(new GameClock(), false));
		}

		[Test]
		public void CorrectReturnValue_QuarterTransitionOn()
		{
			var transition = new ScreenTransition();
			transition.OnTime = 1f;
			transition.OnTime = 1f;

			Assert.IsTrue(transition.Update(new GameClock(), true));
		}

		#endregion //return values

		#region transition state

		[Test]
		public void DefaultState()
		{
			var transition = new ScreenTransition();
			Assert.AreEqual(TransitionState.Hidden, transition.State);
		}

		[Test]
		public void active_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			Assert.IsTrue(transition.StateChange(false));
		}

		[Test]
		public void on_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOn
			};
			Assert.IsTrue(transition.StateChange(false));
		}

		[Test]
		public void hidden_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Hidden
			};
			Assert.IsFalse(transition.StateChange(false));
		}

		[Test]
		public void off_off()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOff
			};
			Assert.IsFalse(transition.StateChange(false));
		}

		[Test]
		public void active_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Active
			};
			Assert.IsFalse(transition.StateChange(true));
		}

		[Test]
		public void on_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOn
			};
			Assert.IsFalse(transition.StateChange(true));
		}

		[Test]
		public void hidden_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.Hidden
			};
			Assert.IsTrue(transition.StateChange(true));
		}

		[Test]
		public void off_on()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOff
			};
			Assert.IsTrue(transition.StateChange(true));
		}

		[Test]
		public void off_on_update()
		{
			var transition = new ScreenTransition()
			{
				State = TransitionState.TransitionOff
			};
			Assert.IsTrue(transition.Update(new GameClock(), true));
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
			Assert.IsTrue(stateChanged);
		}

		#endregion //transition state
	}
}
