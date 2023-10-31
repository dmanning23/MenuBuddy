using InputHelper;
using MenuBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	/// <summary>
	/// The options screen is brought up over the top of the main menu
	/// screen, and gives the user a chance to configure the game
	/// in various hopefully useful ways.
	/// </summary>
	internal class OptionsScreen : MenuStackScreen
	{
		#region Fields

		/// <summary>
		/// menu entry to change the buttnus
		/// </summary>
		private MenuEntry buttnutsEntry;
		private int currentButtnuts = 0;

		MenuEntry touchMenuEntry;
		MenuEntry textRectEntry;

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor.
		/// </summary>
		public OptionsScreen()
			: base("Options")
		{
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			// Create our menu entries.
			buttnutsEntry = new MenuEntry(string.Empty, Content);
			buttnutsEntry.OnClick += ButtnutsEntrySelected;
			SetMenuEntryText();
			AddMenuEntry(buttnutsEntry);

			touchMenuEntry = new MenuEntry("Touch Menus", Content);
			AddMenuEntry(touchMenuEntry);

			textRectEntry = new MenuEntry("Text Rect", Content);
			AddMenuEntry(textRectEntry);

			var backMenuEntry = new MenuEntry("Back", Content);
			backMenuEntry.OnClick += Cancelled;
			AddMenuEntry(backMenuEntry);
		}

		/// <summary>
		/// Fills in the latest values for the options screen menu text.
		/// </summary>
		private void SetMenuEntryText()
		{
			buttnutsEntry.Text = string.Format("buttnuts: {0}", currentButtnuts.ToString());
		}

		#endregion

		#region Handle Input

		/// <summary>
		/// Event handler for when the buttnuts selection menu entry is selected.
		/// </summary>
		private void ButtnutsEntrySelected(object sender, ClickEventArgs e)
		{
			//increment the mic
			currentButtnuts++;
			SetMenuEntryText();
		}
		
		#endregion
	}
}