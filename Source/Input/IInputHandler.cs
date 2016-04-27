using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This is an interface for managing highlighting and selection of items based on user input
	/// </summary>
	public interface IInputHandler : IUpdateable
	{
		/// <summary>
		/// Get the mouse position in game coords
		/// </summary>
		List<Vector2> CursorPos
		{
			get;
		}

		/// <summary>
		/// Handle teh input and pass it to the current top screen.
		/// </summary>
		/// <param name="screen"></param>
		/// <returns></returns>
		void HandleInput(IScreen screen);
	}
}