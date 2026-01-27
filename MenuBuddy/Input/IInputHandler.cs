using HadoukInput;
using InputHelper;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for managing highlighting and selection of items based on user input.
	/// </summary>
	public interface IInputHandler
	{
		/// <summary>
		/// Gets the current input state containing keyboard, gamepad, and other input information.
		/// </summary>
		IInputState InputState { get; }

		/// <summary>
		/// Handles input and passes it to the specified screen.
		/// </summary>
		/// <param name="screen">The screen to receive input.</param>
		void HandleInput(IScreen screen);

		/// <summary>
		/// Event raised when a click has been handled by a screen or widget.
		/// </summary>
		event EventHandler<ClickEventArgs> OnClickHandled;
	}
}