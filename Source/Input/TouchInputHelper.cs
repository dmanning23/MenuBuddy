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

			var widgetScreen = screen as WidgetScreen;
			if (null != widgetScreen)
			{
				//Get all the widgets from the screen
				var buttons = widgetScreen.Buttons;

				if (null != MouseManager)
				{
					//check if the player is holding the LMouseButton down in the widget
					if (MouseManager.LMouseDown)
					{
						CheckHighlight(buttons, MouseManager.MousePos);
					}

					//check if the player selected an item
					if (MouseManager.LMouseClick)
					{
						CheckClick(widgetScreen, buttons, MouseManager.MousePos);
					}
				}

				if (null != TouchManager)
				{
					//check if the player is holding down on the screen
					foreach (var touch in TouchManager.Touches)
					{
						CheckHighlight(buttons, touch);
					}

					//check if the player tapped on the screen
					foreach (var tap in TouchManager.Taps)
					{
						CheckClick(widgetScreen, buttons, tap);
					}
				}
			}
		}

		private void CheckHighlight(IEnumerable<IButton> buttons, Vector2 point)
		{
			foreach (var button in buttons)
			{
				if (button.Rect.Contains(point))
				{
					OnButtonHighlighted(button);
				}
				else
				{
					OnButtonNotHighlighted(button);
				}
			}
		}

		private void CheckClick(WidgetScreen screen, IEnumerable<IButton> buttons, Vector2 point)
		{
			foreach (var button in buttons)
			{
				if (button.Rect.Contains(point))
				{
					OnButtonClick(screen, button);
					break;
				}
			}
		}

		/// <summary>
		/// Called every time update while a widget is highlighted
		/// </summary>
		/// <param name="button"></param>
		private void OnButtonHighlighted(IButton button)
		{
			button.Highlight = true;
		}

		private void OnButtonNotHighlighted(IButton button)
		{
			button.Highlight = false;
		}

		/// <summary>
		/// Called when a widget is selected
		/// </summary>
		/// <param name="screen"></param>
		/// <param name="button"></param>
		private void OnButtonClick(WidgetScreen screen, IButton button)
		{
			//run the selected event
			button.OnSelect(null);

			//tell the menu too
			//screen.OnSelect(null);
		}

		#endregion //Methods
	}
}