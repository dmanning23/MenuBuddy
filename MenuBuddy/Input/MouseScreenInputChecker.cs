using HadoukInput;
using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Processes mouse input events (highlights, clicks, drags, drops) and routes them to screens.
	/// </summary>
	public class MouseScreenInputChecker : IInputHandler
	{
		#region Properties

		/// <summary>
		/// Gets the input helper that provides mouse events.
		/// </summary>
		public IInputHelper InputHelper { get; private set; }

		/// <summary>
		/// Gets or sets the input state for keyboard and controller input.
		/// </summary>
		public IInputState InputState { get; set; }

		/// <summary>
		/// Gets the parent mouse input handler, if any.
		/// </summary>
		private MouseInputHandler MouseInputHandler { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="MouseScreenInputChecker"/> class.
		/// </summary>
		/// <param name="inputHelper">The input helper providing mouse events.</param>
		/// <param name="mouseInputHandler">Optional parent handler to notify when clicks are handled.</param>
		public MouseScreenInputChecker(IInputHelper inputHelper, MouseInputHandler mouseInputHandler = null)
		{
			//Find all the components we need
			InputHelper = inputHelper;
			MouseInputHandler = mouseInputHandler;
		}

		/// <summary>
		/// Event raised when a click has been handled.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnClickHandled;

		#endregion //Initialization

		#region Methods

		/// <summary>
		/// Processes mouse input events and routes them to the specified screen.
		/// Handles highlights, clicks, drags, and drops.
		/// </summary>
		/// <param name="screen">The screen to receive input.</param>
		public void HandleInput(IScreen screen)
		{
			//check highlights
			var highlightScreen = screen as IHighlightable;
			if (null != highlightScreen)
			{
				int i = 0;
				while (i < InputHelper.Highlights?.Count)
				{
					if (highlightScreen.CheckHighlight(InputHelper.Highlights[i]))
					{
						InputHelper.Highlights.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check clicks
			var clickScreen = screen as IClickable;
			if (null != clickScreen)
			{
				int i = 0;
				while (i < InputHelper.Clicks?.Count)
				{
					if (clickScreen.CheckClick(InputHelper.Clicks[i]))
					{
						if (null != MouseInputHandler)
						{
							MouseInputHandler.ClickHandled(this, InputHelper.Clicks[i]);
						}
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
			if (null != dragScreen)
			{
				int i = 0;
				while (i < InputHelper.Drags?.Count)
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
			if (null != dropScreen)
			{
				int i = 0;
				while (i < InputHelper.Drops?.Count)
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
		}

		#endregion //Methods
	}
}