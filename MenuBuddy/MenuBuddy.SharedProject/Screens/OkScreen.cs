
namespace MenuBuddy
{
	/// <summary>
	/// A popup message box screen that just says "Ok"
	/// </summary>
	public class OkScreen : MessageBoxScreen
	{
		/// <summary>
		/// Constructor
		/// </summary>
		public OkScreen(string message)
			: base(message)
		{
		}

		protected override void AddButtons(StackLayout stack)
		{
			//just an ok button on this screen
			stack.AddItem(AddMessageBoxOkButton());
		}
	}
}