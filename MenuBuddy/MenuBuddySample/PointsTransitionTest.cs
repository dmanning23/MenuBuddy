using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MenuBuddySample
{
	/// <summary>
	/// The main menu screen is the first thing displayed when the game starts up.
	/// </summary>
	internal class PointsTransitionTest : MenuScreen, IMainMenu
	{
		IScreenTransition screenTransition; 

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public PointsTransitionTest()
			: base("Path Test Menu")
		{
		}

		public override async Task LoadContent()
		{
			await base.LoadContent();

			screenTransition = new ScreenTransition
			{
				OnTime = 10f,
				OffTime = 0f,
			};

			var transition = new PathTransitionObject(new List<Vector2>
			{
				new Vector2(100, 100),
				new Vector2(500, 100),
				new Vector2(500, 400),
				new Vector2(100, 400)
			}, screenTransition);

			var label = new Label("TEST", Content, FontSize.Small)
			{
				Position = new Point(100, 100),
				TransitionObject = transition,
				Horizontal = HorizontalAlignment.Center,
				Vertical = VerticalAlignment.Center
			};
			AddItem(label);
		}

		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			UpdateTransition(screenTransition, Time);
		}
	}
}