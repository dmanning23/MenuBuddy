using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is a menu entry of a red X that goes in the upper left corner
	/// </summary>
	public class CancelMenuEntry : TouchFillEntry
	{
		#region Methods

		public CancelMenuEntry(ContentManager content, string icon = "Cancel", int iconSize = 96, bool drawOutline = false, bool messageBoxEntry = false)
			: base("", drawOutline, messageBoxEntry)
		{
			TransitionType = MenuTransition.SlideRight;
			
			//load the icon
			Image = content.Load<Texture2D>(icon);

			//set the location
			ButtonRect = new Rectangle(Resolution.TitleSafeArea.Right - (int)(1.5f * iconSize),
				Resolution.TitleSafeArea.Top,
				iconSize, iconSize);

			DrawWhenInactive = false;
		}

		#endregion //Methods
	}
}