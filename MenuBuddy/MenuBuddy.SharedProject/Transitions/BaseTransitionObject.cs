using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	public abstract class BaseTransitionObject : ITransitionObject
	{
		#region Properties

		public IScreenTransition ScreenTransition { get; set; }

		#endregion //Properties

		#region Methods

		public BaseTransitionObject(IScreenTransition screenTransition)
		{
			ScreenTransition = screenTransition;
		}

		public void LoadContent(IScreen screen)
		{
			if (null == ScreenTransition)
			{
				ScreenTransition = screen.Transition;
			}
		}

		public abstract Point Position(Rectangle rect);

		public abstract Vector2 Position(Point pos);

		public abstract Vector2 Position(Vector2 pos);

		#endregion //Methods
	}
}
