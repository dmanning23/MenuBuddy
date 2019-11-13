using HadoukInput;
using InputHelper;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a stack of menu options. 
	/// The user can move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public interface IMenuScreen : IWidgetScreen
	{
		void CheckInput(IInputState input);

		void Cancelled(object obj, ClickEventArgs e);
	}
}