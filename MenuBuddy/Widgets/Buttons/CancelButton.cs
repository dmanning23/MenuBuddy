using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A button that exits the current screen when clicked, typically positioned at the top-right corner
	/// with a pop-right transition effect.
	/// </summary>
	public class CancelButton : RelativeLayoutButton
	{
		#region Properties

		/// <summary>
		/// The texture resource name for the cancel icon.
		/// </summary>
		private string IconTextureName { get; set; }

		/// <summary>
		/// The image widget displaying the cancel icon.
		/// </summary>
		public Image CancelIcon { get; private set; }

		/// <summary>
		/// An optional custom size for the cancel icon, in pixels. If <c>null</c>, the style sheet or texture size is used.
		/// </summary>
		private int? CustomSize;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="CancelButton"/> with an optional custom icon texture.
		/// </summary>
		/// <param name="iconTextureName">The texture resource name for the icon, or empty to use the style sheet default.</param>
		public CancelButton(string iconTextureName = "")
		{
			ClickedSound = StyleSheet.CancelButtonSoundResource;
			IconTextureName = !string.IsNullOrEmpty(iconTextureName) ? iconTextureName : StyleSheet.CancelButtonImageResource;
		}

		/// <summary>
		/// Initializes a new <see cref="CancelButton"/> with a custom icon size.
		/// </summary>
		/// <param name="customSize">The size in pixels for the cancel icon, or <c>null</c> to use the default.</param>
		public CancelButton(int? customSize) : this()
		{
			CustomSize = customSize;
		}

		/// <summary>
		/// Loads the cancel icon, configures positioning and transitions, and wires up the click handler to exit the screen.
		/// </summary>
		/// <param name="screen">The screen whose content manager is used for loading.</param>
		public override async Task LoadContent(IScreen screen)
		{
			await base.LoadContent(screen);

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
			Vector2 size = Vector2.Zero;
			if (CustomSize.HasValue)
			{
				size = new Vector2(CustomSize.Value);
				CancelIcon.FillRect = true;
				CancelIcon.Size = size;
			}
			else if (StyleSheet.CancelButtonSize.HasValue)
			{
				size = new Vector2(StyleSheet.CancelButtonSize.Value);
				CancelIcon.FillRect = true;
				CancelIcon.Size = size;
			}
			else
			{
				size = new Vector2(CancelIcon.Texture.Bounds.Width, CancelIcon.Texture.Bounds.Height);
			}

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