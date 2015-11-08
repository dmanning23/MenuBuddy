using GameTimer;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// This the interface for a screen that can display a bunch of widgets.
	/// </summary>
	public interface IWidgetScreen : IScreenItemContainer, IClickable
	{
		#region Methods

		void OnSelect(object obj, PlayerIndexEventArgs playerIndex);

		void OnCancel(object obj, PlayerIndexEventArgs playerIndex);

		#endregion //Methods
	}
}