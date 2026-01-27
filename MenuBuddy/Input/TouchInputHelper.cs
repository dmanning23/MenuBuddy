using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Input handler for touch screen input. Processes highlights, clicks, drags, drops, pinches, flicks, and holds.
	/// </summary>
	public class TouchInputHelper : BaseInputHandler
	{
		#region Properties

		/// <summary>
		/// Gets the input helper service component for touch events.
		/// </summary>
		public IInputHelper InputHelper { get; private set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="TouchInputHelper"/> class
		/// and registers it as the <see cref="IInputHandler"/> service.
		/// </summary>
		/// <param name="game">The game instance.</param>
		/// <exception cref="Exception">Thrown if <see cref="IInputHelper"/> service is not registered.</exception>
		public TouchInputHelper(Game game)
			: base(game)
		{
			InputState.CheckControllers = false;

			//Find all the components we need
			InputHelper = game.Services.GetService(typeof(IInputHelper)) as IInputHelper;

			//make sure that stuff was init correctly
			if (null == InputHelper)
			{
				throw new Exception("Cannot initialize TouchInputHelper without first adding IInputHelper service");
			}

			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IInputHandler), this);
		}

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Processes touch input events and routes them to the specified screen.
		/// Handles highlights, clicks, drags, drops, pinches, flicks, and holds.
		/// Pinch operations take priority and disable other gesture processing.
		/// </summary>
		/// <param name="screen">The screen to receive input.</param>
		public override void HandleInput(IScreen screen)
		{
			base.HandleInput(screen);

			//whether or not there is an ongoing pinch op
			var hasPinch = InputHelper.Pinches.Count > 0;

			//check highlights
			var highlightScreen = screen as IHighlightable;
			if (null != highlightScreen && !hasPinch)
			{
				//Usually there won't be a highlight in the touchinput
				if (0 == InputHelper.Highlights.Count)
				{
					InputHelper.Highlights.Add(new HighlightEventArgs(new Vector2(float.NaN, float.NaN), InputHelper));
				}

				for (var i = 0; i < InputHelper.Highlights.Count; i++)
				{
					highlightScreen.CheckHighlight(InputHelper.Highlights[i]);
				}
			}

			//check clicks
			var clickScreen = screen as IClickable;
			if (null != clickScreen)
			{
				int i = 0;
				while (i < InputHelper.Clicks.Count)
				{
					if (clickScreen.CheckClick(InputHelper.Clicks[i]))
					{
						ClickHandled(this, InputHelper.Clicks[i]);
						InputHelper.Clicks.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check drag operations
			var dragScreen = screen as IDraggable;
			if (null != dragScreen && !hasPinch)
			{
				int i = 0;
				while (i < InputHelper.Drags.Count)
				{
					if (dragScreen.CheckDrag(InputHelper.Drags[i]))
					{
						InputHelper.Drags.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check drop operations
			var dropScreen = screen as IDroppable;
			if (null != dropScreen && !hasPinch)
			{
				int i = 0;
				while (i < InputHelper.Drops.Count)
				{
					if (dropScreen.CheckDrop(InputHelper.Drops[i]))
					{
						InputHelper.Drops.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check pinch operations
			var pinchScreen = screen as IPinchable;
			if (null != pinchScreen)
			{
				int i = 0;
				while (i < InputHelper.Pinches.Count)
				{
					if (pinchScreen.CheckPinch(InputHelper.Pinches[i]))
					{
						InputHelper.Pinches.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check flick operations
			var flickScreen = screen as IFlickable;
			if (null != flickScreen && !hasPinch)
			{
				int i = 0;
				while (i < InputHelper.Flicks.Count)
				{
					if (flickScreen.CheckFlick(InputHelper.Flicks[i]))
					{
						InputHelper.Flicks.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check hold operations
			var holdScreen = screen as IHoldable;
			if (null != holdScreen && !hasPinch)
			{
				int i = 0;
				while (i < InputHelper.Holds.Count)
				{
					if (holdScreen.CheckHold(InputHelper.Holds[i]))
					{
						InputHelper.Holds.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		#endregion //Methods
	}
}