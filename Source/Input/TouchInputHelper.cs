using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MouseBuddy;
using ResolutionBuddy;
using System.Collections.Generic;
using System.Diagnostics;
using TouchScreenBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that does mouse/touchscreen input
	/// </summary>
	public class TouchInputHelper : BaseInputHelper
	{
		#region Properties

		/// <summary>
		/// Get the mouse position in game coords
		/// </summary>
		public Vector2 MousePos
		{
			get
			{
				Vector2 mouse = Vector2.Zero;
				if (null != MouseManager)
				{
					mouse = Resolution.ScreenToGameCoord(MouseManager.MousePos);
				}
				return mouse;
			}
		}

		public IMouseManager MouseManager { get; private set; }

		/// <summary>
		/// the touch manager service component.
		/// warning: this dude might be null if the compoent isnt in this game
		/// </summary>
		public ITouchManager TouchManager { get; private set; }

		private SpriteBatch SpriteBatch { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="game"></param>
		public TouchInputHelper(Game game)
			: base(game)
		{
			//Find all the components we need
			TouchManager = game.Services.GetService(typeof(ITouchManager)) as ITouchManager;
			MouseManager = game.Services.GetService(typeof(IMouseManager)) as IMouseManager;

			//make sure that stuff was init correctly
			Debug.Assert((null != TouchManager) || (null != MouseManager));

			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IInputHelper), this);
		}

		/// <summary>
		/// Load your graphics content.
		/// </summary>
		protected override void LoadContent()
		{
			base.LoadContent();
			SpriteBatch = new SpriteBatch(GraphicsDevice);
		}

		#endregion //Initialization

		#region Methods

		public override void HandleInput(IScreen screen)
		{
			base.HandleInput(screen);

			var widgetScreen = screen as IWidgetScreen;
			if (null != widgetScreen)
			{
				if (null != MouseManager)
				{
					//check if the player is holding the LMouseButton down in the widget
					if (MouseManager.LMouseDown)
					{
						CheckHighlight(widgetScreen, MouseManager.MousePos);
					}

					//check if the player selected an item
					if (MouseManager.LMouseClick)
					{
						CheckClick(widgetScreen, MouseManager.MousePos);
					}
				}

				if (null != TouchManager)
				{
					//check if the player is holding down on the screen
					foreach (var touch in TouchManager.Touches)
					{
						CheckHighlight(widgetScreen, touch);
					}

					//check if the player tapped on the screen
					foreach (var tap in TouchManager.Taps)
					{
						CheckClick(widgetScreen, tap);
					}
				}
			}
		}

		private void CheckHighlight(IWidgetScreen screen, Vector2 point)
		{
			screen.CheckHighlight(point);
        }

		private void CheckClick(IWidgetScreen screen, Vector2 point)
		{
			if (!screen.CheckClick(point))
			{
				//if no buttons were clicked, send it to the game screen itself
				var gameScreen = screen as IGameScreen;
				if (null != gameScreen)
				{
					gameScreen.Click(point);
				}
			}
		}

		#endregion //Methods
	}
}