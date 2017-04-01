using InputHelper;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	public delegate void ClickDelegate(object obj, ClickEventArgs clickEvent);

	/// <summary>
	/// outward facing interface for a hambuger icon.
	/// Clicking this icon brings up a sidebar menu.
	/// </summary>
	public interface IHamburger : IButton
	{
		void AddItem(Texture2D icon, string iconText, ClickDelegate clickEvent);
	}
}
