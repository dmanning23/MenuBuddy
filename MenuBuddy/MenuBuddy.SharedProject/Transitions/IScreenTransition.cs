using GameTimer;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	public interface IScreenTransition
	{
		float OnTime { get; set; }

		float OffTime { get; set; }

		float TransitionPosition { get; }

		float Alpha { get; }

		TransitionState State { get; set; }

		event EventHandler OnStateChange;

		Color AlphaColor(Color color);

		bool Update(GameClock gameTime, bool transitionOn);
	}
}
