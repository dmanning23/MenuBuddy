using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// THis is a menu entry that pops up from the bottom of a window and says "Continue"
	/// </summary>
	public class CancelButton : RelativeLayoutButton
	{
		#region Properties

		/// <summary>
		/// The size of the icon to draw
		/// </summary>
		private int IconSize { get; set; }

		public override Rectangle Rect
		{
			set
			{
				base.Rect = new Rectangle(Resolution.TitleSafeArea.Right - (int)(1.5f * IconSize),
					Resolution.TitleSafeArea.Top,
					IconSize, IconSize);
			}
		}

		private string IconTextureName { get; set; }

		#endregion //Properties

		#region Methods

		public CancelButton(string iconTextureName = "Cancel", int iconSize = 96)
		{
			IconTextureName = iconTextureName;
			IconSize = iconSize;
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			//load the icon
			StyleSheet style = Style;
			style.Transition = TransitionType.PopRight;
			style.HasBackground = false;
			style.Texture = screen.ScreenManager.Game.Content.Load<Texture2D>(IconTextureName);
			var image = new Image
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center
			};
			AddItem(image);

			DrawWhenInactive = false;
		}

		#endregion //Methods
	}
}