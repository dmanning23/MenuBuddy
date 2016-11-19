using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System.Diagnostics;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that does mouse/touchscreen input
	/// </summary>
	public class MouseInputHelper : BaseInputHandler
	{
		#region Properties

		/// <summary>
		/// the touch manager service component.
		/// warning: this dude might be null if the compoent isnt in this game
		/// </summary>
		public IInputHelper InputHelper { get; private set; }

		private SpriteBatch SpriteBatch { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="game"></param>
		public MouseInputHelper(Game game)
			: base(game)
		{
			//Find all the components we need
			InputHelper = game.Services.GetService(typeof(IInputHelper)) as IInputHelper;

			//make sure that stuff was init correctly
			Debug.Assert(null != InputHelper);

			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IInputHandler), this);
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

			//TODO: check for keydown events
		}

		public override void HandleInput(IScreen screen)
		{
			//check highlights
			var highlightScreen = screen as IHighlightable;
			if (null != highlightScreen)
			{
				int i = 0;
				while (i < InputHelper.Highlights.Count)
				{
					if (highlightScreen.CheckHighlight(InputHelper.Highlights[i]))
					{
						InputHelper.Highlights.RemoveAt(i);
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
				while (i < InputHelper.Clicks.Count)
				{
					if (clickScreen.CheckClick(InputHelper.Clicks[i]))
					{
						InputHelper.Clicks.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check drag operations
			var dragScreen = screen as IDraggable;
			if (null != dragScreen)
			{
				int i = 0;
				while (i < InputHelper.Drags.Count)
				{
					if (dragScreen.CheckDrag(InputHelper.Drags[i]))
					{
						InputHelper.Drags.RemoveAt(i);
					}
					else
					{
						i++;
					}
				}
			}

			//check drop operations
			var dropScreen = screen as IDroppable;
			if (null != dropScreen)
			{
				int i = 0;
				while (i < InputHelper.Drops.Count)
				{
					if (dropScreen.CheckDrop(InputHelper.Drops[i]))
					{
						InputHelper.Drops.RemoveAt(i);
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