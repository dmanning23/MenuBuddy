using InputHelper;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A checkbox button that toggles between checked and unchecked states,
	/// displaying the corresponding texture from the style sheet.
	/// </summary>
	public class Checkbox : RelativeLayoutButton, ICheckbox
	{
		#region Properties

		/// <summary>
		/// The texture displayed when the checkbox is checked.
		/// </summary>
		private Texture2D CheckedTexture { get; set; }

		/// <summary>
		/// The texture displayed when the checkbox is unchecked.
		/// </summary>
		private Texture2D UncheckedTexture { get; set; }

		/// <summary>
		/// The image widget that displays the current checked/unchecked texture.
		/// </summary>
		private Image CheckedImage { get; set; }

		private bool _isChecked;

		/// <summary>
		/// Whether this checkbox is currently checked. Setting this updates the displayed texture.
		/// </summary>
		public bool IsChecked
		{
			get
			{
				return _isChecked;
			}
			set
			{
				_isChecked = value;
				SetCheckImage();
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="Checkbox"/> with the specified initial checked state.
		/// </summary>
		/// <param name="isChecked">The initial checked state.</param>
		public Checkbox(bool isChecked) : base()
		{
			IsChecked = isChecked;
		}

		/// <summary>
		/// Loads the checked and unchecked textures from the style sheet and adds the initial image to the layout.
		/// </summary>
		/// <param name="screen">The screen whose content manager is used for loading.</param>
		public override async Task LoadContent(IScreen screen)
		{
			await base.LoadContent(screen);

			//Load the images
			CheckedTexture = screen.Content.Load<Texture2D>(StyleSheet.CheckedImageResource);
			UncheckedTexture = screen.Content.Load<Texture2D>(StyleSheet.UncheckedImageResource);

			//add the image
			CheckedImage = new Image(IsChecked ? CheckedTexture : UncheckedTexture)
			{
				Size = this.Size,
				Horizontal = this.Horizontal,
				Vertical = this.Vertical,
				FillRect = true
			};
			AddItem(CheckedImage);
		}

		/// <summary>
		/// Toggles the checked state and updates the displayed texture when clicked.
		/// </summary>
		/// <param name="obj">The source of the click event.</param>
		/// <param name="e">The click event arguments.</param>
		public override void Clicked(object obj, ClickEventArgs e)
		{
			IsChecked = !IsChecked;
			SetCheckImage();
			base.Clicked(obj, e);
		}

		/// <summary>
		/// Updates the displayed texture to match the current <see cref="IsChecked"/> state.
		/// </summary>
		private void SetCheckImage()
		{
			if (null != CheckedImage)
			{
				CheckedImage.Texture = IsChecked ? CheckedTexture : UncheckedTexture;
			}
		}

		#endregion //Methods
	}
}
