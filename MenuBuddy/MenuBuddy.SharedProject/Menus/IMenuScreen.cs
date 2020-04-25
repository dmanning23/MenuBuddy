using HadoukInput;
using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// This is a class that has several options that can be tabbed through by the player.
	/// </summary>
	public interface IMenuScreen : IWidgetScreen, IGameScreen
	{
		int SelectedIndex { get; }

		IScreenItem SelectedItem { get; }

		void SetSelectedIndex(int index);

		void SetSelectedItem(IScreenItem item);

		void AddMenuItem(IScreenItem menuItem, int tabOrder = 0);

		void RemoveMenuItem(IScreenItem entry);

		void RemoveMenuItem(int index);
	}
}