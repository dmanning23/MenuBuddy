using HadoukInput;
using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface for managing highlighting and selection of items based on user input
	/// </summary>
	public interface IInputHandler
	{
		IInputState InputState { get; }

		/// <summary>
		/// Handle teh input and pass it to the current top screen.
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		void HandleInput(IScreen screen);

		event EventHandler<ClickEventArgs> OnClickHandled;
	}
}