using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Base class for screens that contain a menu of options. The user can
	/// move up and down to select an entry, or cancel to back out of the screen.
	/// </summary>
	public abstract class MenuStackScreen : MenuScreen
	{
		#region Fields

		private Point _menuTitlePosition;

		private Point _menuEntryPosition;

		/// <summary>
		/// The game type, as loaded from the screenmanager.game
		/// </summary>
		private GameType _gameType;

		#endregion

		#region Properties

		/// <summary>
		/// The title of this menu
		/// </summary>
		public Label MenuTitle { get; private set; }

		/// <summary>
		/// Gets the list of menu entries, so derived classes can add or change the menu contents.
		/// </summary>
		protected StackLayout MenuEntries { get; private set; }

		protected Point MenuTitlePosition
		{
			get { return _menuTitlePosition; }
			set
			{
				_menuTitlePosition = value;
				if (null != MenuTitle)
				{
					MenuTitle.Position = _menuTitlePosition;
				}
			}
		}

		protected Point MenuEntryPosition
		{
			get { return _menuEntryPosition; }
			set
			{
				_menuEntryPosition = value;
				MenuEntries.Position = _menuEntryPosition;
			}
		}

		public override string ScreenName
		{
			get
			{
				return base.ScreenName;
			}
			set
			{
				base.ScreenName = value;
				if (null != MenuTitle)
				{
					MenuTitle.Text = base.ScreenName;
				}
			}
		}

		#endregion

		#region Methods

		/// <summary>
		/// Constructor.
		/// </summary>
		protected MenuStackScreen(string menuTitle = "", ContentManager content = null)
			: base(menuTitle, content)
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;

			_menuEntryPosition = new Point(Resolution.TitleSafeArea.Center.X, (int)(Resolution.TitleSafeArea.Center.Y * 0.8f));
			_menuTitlePosition = new Point(Resolution.TitleSafeArea.Center.X, (int)(Resolution.TitleSafeArea.Center.Y * 0.5f));
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			var game = ScreenManager?.Game as DefaultGame;
			_gameType = null != game ? game.GameType : GameType.Controller;


			//Create the stack layout for teh menu entries
			MenuEntries = new StackLayout()
			{
				Alignment = StackAlignment.Top,
				Horizontal = HorizontalAlignment.Center,
				Layer = 1.0f,
				Position = MenuEntryPosition
			};
			AddItem(MenuEntries);

			//Add the menu title
			MenuTitle = new MenuTitle(ScreenName, Content)
			{
				Layer = 2.0f,
				Position = MenuTitlePosition
			};
			MenuTitle.ShrinkToFit(Resolution.TitleSafeArea.Width);
			AddItem(MenuTitle);
		}

		protected void AddMenuEntry(IScreenItem menuEntry)
		{
			menuEntry.LoadContent(this);
			AddMenuItem(menuEntry);

			MenuEntries.AddItem(menuEntry);
		}

		/// <summary>
		/// This method adds a continue button to the menu and attachs it to OnCancel
		/// </summary>
		protected ILeftRightItem AddContinueButton()
		{
			var continueButton = new ContinueMenuEntry(Content);
			continueButton.OnClick += ((obj, e) => { ExitScreen(); });
			AddMenuEntry(continueButton);
			return continueButton;
		}

		#endregion //Methods
	}
}