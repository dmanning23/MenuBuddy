
namespace MenuBuddy
{
	/// <summary>
	/// A list of all the items in a screen that can possibly be styled
	/// </summary>
	public enum StyleItemType
	{
		/// <summary>
		/// the font buddy that will be used when this item is selected
		/// </summary>
		SelectedFont,

		/// <summary>
		/// the font buddy that will be used when this item is not selected
		/// </summary>
		UnselectedFont,

		/// <summary>
		/// The color to write the text of this item when selected
		/// </summary>
		SelectedTextColor,

		/// <summary>
		/// The color to write the text of this item when not selected
		/// </summary>
		UnselectedTextColor,

		/// <summary>
		/// The color to draw the text shadow of this item when selected
		/// </summary>
		SelectedShadowColor,

		/// <summary>
		/// The color to draw the text shadow of this item when not selected
		/// </summary>
		UnselectedShadowColor,

		/// <summary>
		/// The color to draw the outline around the border of this item when selected
		/// </summary>
		SelectedOutlineColor,

		/// <summary>
		/// The color to draw the outline around the border of this item when not selected
		/// </summary>
		UnselectedOutlineColor,

		/// <summary>
		/// The color to draw the background image of this item when selected
		/// </summary>
		SelectedBackgroundColor,

		/// <summary>
		/// The color to draw the background image of this item when not selected
		/// </summary>
		UnselectedBackgroundColor,

		/// <summary>
		/// Whether or not this item has an outline around the border
		/// </summary>
		HasOutline,

		/// <summary>
		/// Whether or not this item has a background drawn behind it
		/// </summary>
		HasBackground,

		/// <summary>
		/// Whether or not this item should play any sounds when it is selected
		/// </summary>
		IsQuiet,
		
		/// <summary>
		/// The transitions style of this item
		/// </summary>
		Transition,

		/// <summary>
		/// item selected sound effect
		/// </summary>
		SelectedSoundEffect,

		/// <summary>
		/// item selection change sound effect
		/// </summary>
		SelectionChangeSoundEffect,

		/// <summary>
		/// background texture
		/// </summary>
		Texture
	}
}