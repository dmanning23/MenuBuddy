using InputHelper;
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

		public CancelButton(string iconTextureName = "Cancel")
		{
			IconTextureName = iconTextureName;
			Scale = 3f;
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			//load the icon
			Transition = new WipeTransitionObject(TransitionWipeType.PopRight);
			HasBackground = false;
			HasOutline = true;
			var image = new Image(screen.ScreenManager.Game.Content.Load<Texture2D>(IconTextureName))
			{
				Vertical = VerticalAlignment.Center,
				Horizontal = HorizontalAlignment.Center,
				Transition = new WipeTransitionObject(TransitionWipeType.PopRight)
			};
			AddItem(image);

			//set the size to the texture size
			var size = new Vector2(image.Texture.Bounds.Width, image.Texture.Bounds.Height);
			var relLayout = Layout as RelativeLayout;
			relLayout.Size = size;
			Size = size;

			var imageRect = image.Rect;
			Position = new Point(Resolution.TitleSafeArea.Right - (int)(1.5f * imageRect.Width),
					Resolution.TitleSafeArea.Top);

			DrawWhenInactive = false;

			//Exit the screen when this button is selected
			OnClick += ((object obj, ClickEventArgs e) => 
			{
				screen.ExitScreen();
			});
        }

		#endregion //Methods
	}
}