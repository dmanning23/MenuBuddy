using InputHelper;
using MenuBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	public class MainMenuScreen : MenuStackScreen, IMainMenu
	{
		#region Methods

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public MainMenuScreen()
			: base("Main Menu")
		{
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//TODO: Uncomment or add the screens you want to test.

			// Create our menu entries.

			//var loadingTest = new MenuEntry("Loading Screen Test", Content);
			//loadingTest.OnClick += ((obj, e) =>
			//{
			//	LoadingScreen.Load(ScreenManager, new DropdownTestWithDelay());
			//});
			//AddMenuEntry(loadingTest);

			//var touchMenuEntry = new MenuEntry("Dropdown Test", Content);
			//touchMenuEntry.OnClick += ((obj, e) =>
			//{
			//	ScreenManager.AddScreen(new DropdownTest(), null);
			//});
			//AddMenuEntry(touchMenuEntry);

			//var entry200 = new MenuEntry("Big Scroll Test", Content);
			//entry200.OnClick += ((object obj, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new BigScrollTest());
			//});
			//AddMenuEntry(entry200);

			//var entry = new MenuEntry("Scroll Test", Content);
			//entry.OnClick += ((object obj, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new ScrollOptionsScreen());
			//});
			//AddMenuEntry(entry);

			//var optionsMenuEntry = new MenuEntry("Tree Test", Content);
			//optionsMenuEntry.OnClick += ((obj, e) =>
			//{
			//	ScreenManager.AddScreen(new TreeTest(), null);
			//});
			//AddMenuEntry(optionsMenuEntry);

			//var entry3 = new MenuEntry("Text Edit W/ Dialog", Content);
			//entry3.OnClick += ((object obj, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new TextEditMessageBoxTest());
			//});
			//AddMenuEntry(entry3);

			//var entry2 = new MenuEntry("Path Transition Test", Content);
			//entry2.OnClick += ((object obj, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new PointsTransitionTest());
			//});
			//AddMenuEntry(entry2);

			//var contextMenuTest = new MenuEntry("Context Menu Test", Content);
			//contextMenuTest.OnClick += ((object sender, ClickEventArgs e) =>
			//{
			//	ScreenManager.ClearScreens();
			//	LoadingScreen.Load(ScreenManager, null, string.Empty, new ContextMenuTest());
			//});
			//AddMenuEntry(contextMenuTest);

			//var numEditTest = new MenuEntry("NumEdit Test", Content);
			//numEditTest.OnClick += ((object sender, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new NumEditTest(), null);
			//});
			//AddMenuEntry(numEditTest);

			//var dragdrop = new MenuEntry("DragDropButton Test", Content);
			//dragdrop.OnClick += ((object sender, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new DragDropScreen(), null);
			//});
			//AddMenuEntry(dragdrop);

			//var startGame = new MenuEntry("Slider Test");
			//startGame.OnClick += ((object sender, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new SliderTest(), null);
			//});
			//AddMenuEntry(startGame);

			//var entry1 = new MenuEntry("Text Edit Test", Content);
			//entry1.OnClick += ((object obj, ClickEventArgs e) =>
			//{
			//	ScreenManager.AddScreen(new TextEditTest());
			//});
			//AddMenuEntry(entry1);

			var okMenuEntry = new MenuEntry("Ok Screen", Content);
			okMenuEntry.OnClick += (e, obj) =>
			{
				ScreenManager.AddScreen(new OkScreen("Testing the OK Screen!", Content));
			};
			AddMenuEntry(okMenuEntry);

			var exitMenuEntry = new MenuEntry("Exit", Content);
			exitMenuEntry.OnClick += OnExit;
			AddMenuEntry(exitMenuEntry);
		}

		#endregion //Methods

		#region Handle Input

		/// <summary>
		/// Event handler for when the High Scores menu entry is selected.
		/// </summary>
		private void OptionsMenuEntrySelected(object sender, ClickEventArgs e)
		{
			//screen to adjust mic sensitivity
			ScreenManager.AddScreen(new OptionsScreen(), null);
		}

		/// <summary>
		/// Event handler for when the High Scores menu entry is selected.
		/// </summary>
		private void TouchMenuEntrySelected(object sender, ClickEventArgs e)
		{
		}

		/// <summary>
		/// When the user cancels the main menu, ask if they want to exit the sample.
		/// </summary>
		protected void OnExit(object sender, ClickEventArgs e)
		{
			const string message = "Are you sure you want to exit?";
			var confirmExitMessageBox = new MessageBoxScreen(message);
			confirmExitMessageBox.OnSelect += ConfirmExitMessageBoxAccepted;
			ScreenManager.AddScreen(confirmExitMessageBox, e.PlayerIndex);
		}

		/// <summary>
		/// Event handler for when the user selects ok on the "are you sure
		/// you want to exit" message box.
		/// </summary>
		private void ConfirmExitMessageBoxAccepted(object sender, ClickEventArgs e)
		{
#if !__IOS__
			ScreenManager.Game.Exit();
#endif
		}

		/// <summary>
		/// Ignore the cancel message from the main menu
		/// </summary>
		public override void Cancelled(object obj, ClickEventArgs e)
		{
			//do nothing here!
		}

		#endregion
	}
}