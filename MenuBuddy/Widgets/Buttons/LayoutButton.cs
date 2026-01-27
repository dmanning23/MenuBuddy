using GameTimer;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

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

		public override async Task LoadContent(IScreen screen)
		{
			await base.LoadContent(screen);
		}

		#endregion //Initialization
	}
}