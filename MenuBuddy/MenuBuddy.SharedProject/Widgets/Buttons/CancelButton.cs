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

		public Image CancelIcon { get; private set; }

		#endregion //Properties

		#region Methods

		public CancelButton(string iconTextureName = "")
		{
			IconTextureName = !string.IsNullOrEmpty(iconTextureName) ? iconTextureName : StyleSheet.CancelButtonImageResource;
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			//load the icon
			TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight);
			HasBackground = false;
			HasOutline = StyleSheet.CancelButtonHasOutline;
			Horizontal = HorizontalAlignment.Right;
			Vertical = VerticalAlignment.Top;
			DrawWhenInactive = false;

			CancelIcon = new Image(screen.Content.Load<Texture2D>(IconTextureName))
			{
				Vertical = VerticalAlignment.Top,
				Horizontal = HorizontalAlignment.Right,
				TransitionObject = new WipeTransitionObject(TransitionWipeType.PopRight),
			};
			AddItem(CancelIcon);

			//set the size to the texture size
			var size = new Vector2(CancelIcon.Texture.Bounds.Width, CancelIcon.Texture.Bounds.Height);
			var relLayout = Layout as RelativeLayout;
			relLayout.Size = size;
			Size = size;

			Position = new Point(Resolution.TitleSafeArea.Right, Resolution.TitleSafeArea.Top) + StyleSheet.CancelButtonOffset;

			//Exit the screen when this button is selected
			OnClick += ((object obj, ClickEventArgs e) =>
			{
				screen.ExitScreen();
			});
		}

		#endregion //Methods
	}
}