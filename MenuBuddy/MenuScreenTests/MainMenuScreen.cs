using InputHelper;
using MenuBuddy;
using System.Threading.Tasks;

namespace MenuScreenTests
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

            // Create our menu entries.

            //var entry3 = new MenuEntry("Scroll Test", Content);
            //entry3.OnClick += ((object obj, ClickEventArgs e) =>
            //{
            //	LoadingScreen.Load(ScreenManager, new BigScrollTest());
            //});
            //AddMenuEntry(entry3);

            var entry2 = new MenuEntry("Menu Screen", Content);
            entry2.OnClick += ((object obj, ClickEventArgs e) =>
            {
                ScreenManager.AddScreen(new TestMenuScreen());
            });
            AddMenuEntry(entry2);

            var entry3 = new MenuEntry("Tabbed Menu Screen", Content);
            entry3.OnClick += ((object obj, ClickEventArgs e) =>
            {
                ScreenManager.AddScreen(new TestMenuScreenTabs());
            });
            AddMenuEntry(entry3);

            var loadingTest2 = new MenuEntry("Empty Menu Screen", Content);
            loadingTest2.OnClick += ((obj, e) =>
            {
                ScreenManager.AddScreen(new EmptyMenuScreen(), null);
            });
            AddMenuEntry(loadingTest2);

            var touchMenuEntry = new MenuEntry("Empty Menu Stack Screen (w loading)", Content);
            touchMenuEntry.OnClick += ((obj, e) =>
            {
                LoadingScreen.Load(ScreenManager, new IScreen[] { });
            });
            AddMenuEntry(touchMenuEntry);

            var loadingTest3 = new MenuEntry("Empty Menu Screen (w loading)", Content);
            loadingTest3.OnClick += ((obj, e) =>
            {
                ScreenManager.AddScreen(new EmptyMenuStackScreen(), null);
            });
            AddMenuEntry(loadingTest3);

            var exitMenuEntry = new MenuEntry("Exit", Content);
            exitMenuEntry.OnClick += OnExit;
            AddMenuEntry(exitMenuEntry);
        }

        #endregion //Methods

        #region Handle Input

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