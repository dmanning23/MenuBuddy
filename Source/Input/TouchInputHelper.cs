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

		private List<ClickEventArgs> Clicks
		{
			get; set;
		}

		private List<HighlightEventArgs> Highlights
		{
			get; set;
		}

		List<DragEventArgs> Drags
		{
			get; set;
		}

		List<DropEventArgs> Drops { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="game"></param>
		public TouchInputHelper(Game game)
			: base(game)
		{
			Clicks = new List<ClickEventArgs>();
			Highlights = new List<HighlightEventArgs>();
			Drags = new List<DragEventArgs>();
			Drops = new List<DropEventArgs>();
				 
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

		public override void Update(GameTime gameTime)
		{
			base.Update(gameTime);

			//clear out all the mouse clicks and highlights
			Clicks.Clear();
			Highlights.Clear();
			Drags.Clear();
			Drops.Clear();

			//Check the mouse for stuff
			if (null != MouseManager)
			{
				//create the highlight event
				Highlights.Add(new HighlightEventArgs()
				{
					Position = MouseManager.MousePos
				});

				//check for mouse events
				foreach (var mouseEvent in MouseManager.MouseEvents)
				{
					var click = mouseEvent as ClickEventArgs;
					if (null != click)
					{
						Clicks.Add(click);
						continue;
					}

					var drag = mouseEvent as DragEventArgs;
					if (null != drag)
					{
						Drags.Add(drag);
						continue;
					}

					var drop = mouseEvent as DropEventArgs;
					if (null != drop)
					{
						Drops.Add(drop);
						continue;
					}
				}
			}

			//check the touch stuff
			if (null != TouchManager)
			{
				//check if the player is holding down on the screen
				foreach (var touch in TouchManager.Touches)
				{
					Highlights.Add(new HighlightEventArgs()
					{
						Position = MouseManager.MousePos
					});
				}

				//check if the player tapped on the screen
				foreach (var tap in TouchManager.Taps)
				{
					Clicks.Add(new ClickEventArgs()
					{
						Position = MouseManager.MousePos,
						Button = MouseButton.Left
					});
				}
			}

			//TODO: check for keydown events
		}

		public override void HandleInput(IScreen screen)
		{
			//check highlights
			var highlightScreen = screen as IHighlightable;
			if (null != highlightScreen)
			{
				int i = 0;
				while (i < Highlights.Count)
				{
					if (highlightScreen.CheckHighlight(Highlights[i]))
					{
						Highlights.RemoveAt(i);
					}
					else
					{
						i++;
					}
                }
			}

			//check clicks
			var clickScreen = screen as IClickable;
			if (null != clickScreen)
			{
				int i = 0;
				while (i < Clicks.Count)
				{
					if (clickScreen.CheckClick(Clicks[i]))
					{
						Clicks.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}
		}

		#endregion //Methods
	}
}