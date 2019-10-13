using Microsoft.Xna.Framework.Content;
using System.Threading.Tasks;

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
		public OkScreen(string message, ContentManager content = null)
			: base(message, "", content)
		{
		}

		protected override async Task AddButtons(StackLayout stack)
		{
			//just an ok button on this screen
			stack.AddItem(await AddMessageBoxOkButton());
		}
	}
}