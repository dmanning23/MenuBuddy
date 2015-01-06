
namespace MenuBuddy
{
	/// <summary>
	/// A popup message box screen that just says "Ok"
	/// </summary>
	public class OkScreen : MessageBoxScreen
	{
		/// <summary>
		/// Constructor automatically includes the standard "A=ok, B=cancel"
		/// usage text prompt.
		/// </summary>
		public OkScreen(string message)
			: this(message, true)
		{
		}

		/// <summary>
		/// Constructor lets the caller specify whether to include the standard
		/// "A=ok, B=cancel" usage text prompt.
		/// </summary>
		public OkScreen(string message, bool includeUsageText)
			: base(message, includeUsageText)
		{
		}

		protected override void AddButtons(bool includeUsageText)
		{
			//just an ok button on this screen
			AddOkButton(includeUsageText);
		}
	}
}