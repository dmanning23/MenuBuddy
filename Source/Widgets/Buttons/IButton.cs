using Microsoft.Xna.Framework;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Interface for the button object
	/// This is a widget that can be selected
	/// </summary>
	public interface IButton : IWidget, IScreenItemContainer, ISelectable, IClickable
	{
		Vector2 Size { set; }

		/// <summary>
		/// A description of the function of the button.
		/// </summary>
		string Description { get; }
	}
}