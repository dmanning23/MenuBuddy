using HadoukInput;
using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that has several options that can be tabbed through by the player.
	/// </summary>
	public interface IMenuScreen : IWidgetScreen, IGameScreen
	{
		void AddMenuItem(IWidget menuItem, int tabOrder = 0);

		void RemoveMenuItem(IWidget entry);

		void RemoveMenuItem(int index);
	}
}