using System;
using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that does mouse/touchscreen input
	/// </summary>
	public class MouseScreenInputChecker : IInputHandler
	{
		#region Properties

		/// <summary>
		/// the touch manager service component.
		/// warning: this dude might be null if the compoent isnt in this game
		/// </summary>
		public IInputHelper InputHelper { get; private set; }

		private MouseInputHandler MouseInputHandler { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="game"></param>
		public MouseScreenInputChecker(IInputHelper inputHelper, MouseInputHandler mouseInputHandler = null)
		{
			//Find all the components we need
			InputHelper = inputHelper;
			MouseInputHandler = mouseInputHandler;
		}

		public event EventHandler<ClickEventArgs> OnClickHandled;

		#endregion //Initialization

		#region Methods

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