using InputHelper;
using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface for managing highlighting and selection of items based on user input
	/// </summary>
	public interface IInputHandler
	{
		/// <summary>
		/// Handle teh input and pass it to the current top screen.
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		void HandleInput(IScreen screen);

		event EventHandler<ClickEventArgs> OnClickHandled;
	}
}