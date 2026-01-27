using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Input handler for mouse-based input. Processes highlight, click, drag, and drop events.
	/// </summary>
	public class MouseInputHandler : BaseInputHandler
	{
		#region Properties

		/// <summary>
		/// Gets the input helper service component for mouse events.
		/// </summary>
		public IInputHelper InputHelper { get; private set; }

		/// <summary>
		/// Gets or sets the sprite batch used for rendering.
		/// </summary>
		private SpriteBatch SpriteBatch { get; set; }

		/// <summary>
		/// Gets or sets the input checker that processes mouse events for screens.
		/// </summary>
		private MouseScreenInputChecker InputChecker { get; set; }

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Initializes a new instance of the <see cref="MouseInputHandler"/> class
		/// and registers it as the <see cref="IInputHandler"/> service.
		/// </summary>
		/// <param name="game">The game instance.</param>
		/// <exception cref="Exception">Thrown if <see cref="IInputHelper"/> service is not registered.</exception>
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

			InputChecker = new MouseScreenInputChecker(InputHelper, this)
			{
				InputState = this.InputState
			};

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

		/// <summary>
		/// Handles input for the specified screen by processing mouse events.
		/// </summary>
		/// <param name="screen">The screen to receive input.</param>
		public override void HandleInput(IScreen screen)
		{
			base.HandleInput(screen);

			InputChecker.HandleInput(screen);
		}

		#endregion //Methods
	}
}