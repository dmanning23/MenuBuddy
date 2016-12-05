using InputHelper;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	public class Checkbox : RelativeLayoutButton, ICheckbox
	{
		#region Properties

		private Texture2D CheckedTexture { get; set; }

		private Texture2D UncheckedTexture { get; set; }

		private Image CheckedImage { get; set; }

		private bool _isChecked;
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

		public Checkbox(bool isChecked) : base()
		{
			IsChecked = isChecked;
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);

			//Load the images
			CheckedTexture = screen.ScreenManager.Game.Content.Load<Texture2D>(StyleSheet.CheckedImageResource);
			UncheckedTexture = screen.ScreenManager.Game.Content.Load<Texture2D>(StyleSheet.UncheckedImageResource);

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

		public override void Clicked(object obj, ClickEventArgs e)
		{
			IsChecked = !IsChecked;
			SetCheckImage();
			base.Clicked(obj, e);
		}

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
