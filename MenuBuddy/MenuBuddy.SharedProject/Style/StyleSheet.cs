using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	public abstract class StyleSheet
	{
		#region Options

		/// <summary>
		/// The resource to use as the large font (menu title)
		/// </summary>
		public static string LargeFontResource { get; set; }

		/// <summary>
		/// the resource to use as the medium font (widgets)
		/// </summary>
		public static string MediumFontResource { get; set; }

		/// <summary>
		/// the resource to use as the small font (message boxes)
		/// </summary>
		public static string SmallFontResource { get; set; }

		public static int LargeFontSize { get; set; }

		public static int MediumFontSize { get; set; }

		public static int SmallFontSize { get; set; }

		public static bool UseFontPlus { get; set; }

		public static Color NeutralTextColor { get; set; }

		public static Color HighlightedTextColor { get; set; }

		public static Color SelectedTextColor { get; set; }

		public static Color NeutralOutlineColor { get; set; }

		public static Color NeutralBackgroundColor { get; set; }

		public static Color HighlightedOutlineColor { get; set; }

		public static Color HighlightedBackgroundColor { get; set; }

		public static Color TextShadowColor { get; set; }

		public static string HighlightedSoundResource { get; set; }

		public static string ClickedSoundResource { get; set; }

		public static string ButtonBackgroundImageResource { get; set; }

		public static string MessageBoxBackgroundImageResource { get; set; }

		public static string MessageBoxOkImageResource { get; set; }

		public static string MessageBoxCancelImageResource { get; set; }

		public static Color MessageBoxTextColor { get; set; }

		public static bool SmallFontHasShadow { get; set; }

		public static string CancelButtonImageResource { get; set; }

		public static string CancelButtonSoundResource { get; set; }

		public static bool CancelButtonHasOutline { get; set; }

		public static Point CancelButtonOffset { get; set; }

		public static string TreeExpandImageResource { get; set; }

		public static string TreeCollapseImageResource { get; set; }

		public static string DropdownImageResource { get; set; }

		public static string CheckedImageResource { get; set; }

		public static string UncheckedImageResource { get; set; }

		public static string LoadingScreenHourglassImageResource { get; set; }

		public static string FadeBackgroundImageResource { get; set; }

		public static bool HasOutline { get; set; }

		public static TransitionWipeType DefaultTransition { get; set; }

		public static int? CancelButtonSize { get; set; }

		public static Color ClearColor { get; set; }

		#endregion //Options

		#region Methods

		static StyleSheet()
		{
			LargeFontResource = @"Fonts\ArialBlack72";
			MediumFontResource = @"Fonts\ArialBlack48";
			SmallFontResource = @"Fonts\ArialBlack24";

			LargeFontSize = 144;
			MediumFontSize = 96;
			SmallFontSize = 48;

			UseFontPlus = false;

			NeutralTextColor = Color.White;
			MessageBoxTextColor = Color.White;
			HighlightedTextColor = Color.White;
			SelectedTextColor = Color.Yellow;
			NeutralOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.5f);
			NeutralBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.5f);
			HighlightedOutlineColor = new Color(0.8f, 0.8f, 0.8f, 0.7f);
			HighlightedBackgroundColor = new Color(0.0f, 0.0f, 0.2f, 0.7f);
			TextShadowColor = Color.Black;
			HighlightedSoundResource = @"MenuMove";
			ClickedSoundResource = @"MenuSelect";
			ButtonBackgroundImageResource = @"AlphaGradient";
			MessageBoxBackgroundImageResource = @"gradient";
			CancelButtonImageResource = @"Cancel";
			CancelButtonHasOutline = true;
			CancelButtonOffset = new Point(-64, 0);
			CancelButtonSoundResource = ClickedSoundResource;
			TreeExpandImageResource = @"Expand";
			TreeCollapseImageResource = @"Collapse";
			DropdownImageResource = @"Collapse";
			CheckedImageResource = @"Checked";
			UncheckedImageResource = @"Unchecked";
			LoadingScreenHourglassImageResource = @"hourglass";
			FadeBackgroundImageResource = string.Empty;
			HasOutline = true;
			DefaultTransition = TransitionWipeType.SlideLeft;
			ClearColor = new Color(0.0f, 0.1f, 0.2f);

			CancelButtonSize = null;
		}

		#endregion //Methods
	}
}