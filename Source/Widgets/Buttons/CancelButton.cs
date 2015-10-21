using Microsoft.Xna.Framework;
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
			var image = new Image(screen.ScreenManager.Game.Content.Load<Texture2D>(IconTextureName))
            {
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Style = this.Style,
				Scale = 2f
			};
			AddItem(image);

			Position = new Point(Resolution.TitleSafeArea.Right - (int)(1.5f * IconSize),
					Resolution.TitleSafeArea.Top);

			DrawWhenInactive = false;
		}

		#endregion //Methods
	}
}