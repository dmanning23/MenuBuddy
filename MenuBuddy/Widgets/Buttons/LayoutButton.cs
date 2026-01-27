using GameTimer;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A generic button that uses a specified <see cref="ILayout"/> type to arrange its child items.
	/// </summary>
	/// <typeparam name="T">The layout type used by this button.</typeparam>
	public abstract class LayoutButton<T> : BaseButton where T:ILayout, new()
	{
		#region Initialization

		/// <summary>
		/// Initializes a new <see cref="LayoutButton{T}"/> with a default layout instance.
		/// </summary>
		public LayoutButton()
		{
			Layout = new T();
		}

		/// <summary>
		/// Initializes a new <see cref="LayoutButton{T}"/> by copying values from an existing instance.
		/// </summary>
		/// <param name="inst">The button to copy from.</param>
		public LayoutButton(LayoutButton<T> inst) : base(inst)
		{
		}

		/// <inheritdoc/>
		public override async Task LoadContent(IScreen screen)
		{
			await base.LoadContent(screen);
		}

		#endregion //Initialization
	}
}