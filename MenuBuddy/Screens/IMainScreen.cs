namespace MenuBuddy
{
	/// <summary>
	/// Marker interface to identify the main menu screen of the game.
	/// A game should have only one screen implementing this interface.
	/// The implementing class should also inherit from a Screen class.
	/// </summary>
	public interface IMainMenu : IScreen
	{
	}
}