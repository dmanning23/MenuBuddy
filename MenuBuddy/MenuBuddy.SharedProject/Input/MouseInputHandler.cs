using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// This is an input helper that does mouse/touchscreen input
	/// </summary>
	public class MouseInputHandler : BaseInputHandler
	{
		#region Properties

		/// <summary>
		/// the touch manager service component.
		/// warning: this dude might be null if the compoent isnt in this game
		/// </summary>
		public IInputHelper InputHelper { get; private set; }

		private SpriteBatch SpriteBatch { get; set; }

		private MouseScreenInputChecker InputChecker { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="game"></param>
		public MouseInputHandler(Game game)
			: base(game)
		{
			//Find all the components we need
			InputHelper = game.Services.GetService(typeof(IInputHelper)) as IInputHelper;

			//make sure that stuff was init correctly
			if (null == InputHelper)
			{
				throw new Exception("Cannot initialize TouchInputHelper without first adding IInputHelper service");
			}

			InputChecker = new MouseScreenInputChecker(InputHelper, this);

			//Register ourselves to implement the DI container service.
			game.Components.Add(this);
			game.Services.AddService(typeof(IInputHandler), this);

			InputState.CheckControllers = false;
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

			InputChecker.HandleInput(screen);
		}

		#endregion //Methods
	}
}