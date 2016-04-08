using GameTimer;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button thhat contains a relaitve layout
	/// </summary>
	public abstract class LayoutButton<T> : BaseButton where T:ILayout, new()
	{
		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public LayoutButton()
		{
			Layout = new T();
		}

		public LayoutButton(LayoutButton<T> inst) : base(inst)
		{
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);
		}

		#endregion //Initialization

		#region Methods

		public override void Update(IScreen screen, GameClock gameTime)
		{
			Debug.Assert(null != Layout);
			Layout.Update(screen, gameTime);
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			Debug.Assert(null != Layout);
			Layout.Draw(screen, gameTime);
		}

		#endregion //Methods
	}
}