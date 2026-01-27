using HadoukInput;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for gameplay screens that handle controller/keyboard input directly.
	/// Implementing classes should also inherit from a Screen class.
	/// </summary>
	public interface IGameScreen : IScreen
	{
		/// <summary>
		/// Handles input for the gameplay logic.
		/// </summary>
		/// <param name="input">The current input state containing keyboard and controller data.</param>
		void HandleInput(IInputState input);
	}
}