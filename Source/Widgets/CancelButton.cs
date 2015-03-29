using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// THis is a menu entry that pops up from the bottom of a window and says "Continue"
	/// </summary>
	public class CancelButton : ImageButton
	{
		#region Properties

		private int IconSize { get; set; }

		public override Rectangle Rect
		{
			get { return base.Rect; }
			set
			{
				base.Rect = new Rectangle(Resolution.TitleSafeArea.Right - (int)(1.5f * IconSize),
					Resolution.TitleSafeArea.Top,
					IconSize, IconSize);
			}
		}

		#endregion //Properties

		#region Methods

		public CancelButton(StyleSheet style, ContentManager content, string icon = "Cancel", int iconSize = 96)
			: base(style, "")
		{
			style.Transition = TransitionType.PopRight;
			style.HasBackground = false;

			//load the icon
			Image.Texture = content.Load<Texture2D>(icon);
			IconSize = iconSize;

			DrawWhenInactive = false;
		}

		public CancelButton(StyleSheet style)
			: base(style, "Cancel")
		{
		}

		#endregion //Methods
	}
}