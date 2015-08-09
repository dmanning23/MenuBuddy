using HadoukInput;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// A game screen is something like an xbox or ouya game 
	/// that requires the controller for input.
	/// Should inherit from this and also Screen
	/// </summary>
	public interface IGameScreen : IScreen
	{
		/// <summary>
		/// Do the input logic for the main game.
		/// </summary>
		/// <param name="input"></param>
		void HandleInput(InputState input);

		void Click(Vector2 pos);
	}
}