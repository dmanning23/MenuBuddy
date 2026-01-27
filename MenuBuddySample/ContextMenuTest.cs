using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	public class ContextMenuTest : WidgetScreen
	{
		public ContextMenuTest() : base("ContextMenuTest")
		{
			CoverOtherScreens = true;
			CoveredByOtherScreens = true;
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			//add a label with directions
			AddItem(new Label("Click anywhere to pop up a context menu", Content, FontSize.Small)
			{
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center,
				Position = Resolution.TitleSafeArea.Center,
				Highlightable = false
			});

			var clicker = new RelativeLayoutButton()
			{
				Size = Resolution.ScreenArea.Size.ToVector2(),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top,
				Position = Point.Zero
			};
			clicker.OnClick += PopupMenu;
			AddItem(clicker);

			var cancel = AddCancelButton();
			cancel.OnClick += (obj, e) =>
			{
				var game1 = ScreenManager.Game as Game1;
				ScreenManager.ClearScreens();
				LoadingScreen.Load(ScreenManager, null, string.Empty, game1.GetMainMenuScreenStack());
			};
		}

		public void PopupMenu(object obj, ClickEventArgs e)
		{
			var hamburger = new ContextMenu(e.Position);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\save"), "Save", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\undo"), "Undo", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\redo"), "Redo", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\copy"), "Copy", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\paste"), "Paste", Ok);
			hamburger.AddItem(null, "catpants", Ok);
			hamburger.AddItem(null, "buttnuts", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\pasteSpecial"), "PasteSpecial", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\leftright"), "Mirror", Ok);
			hamburger.AddItem(Content.Load<Texture2D>(@"icons\unkey"), "UnKey", Ok);
			ScreenManager.AddScreen(hamburger);
		}

		private void Ok(object obj, ClickEventArgs e)
		{
			ScreenManager.AddScreen(new OkScreen("selected something"));
		}
	}
}
