using FontBuddyLib;

namespace MenuBuddy
{
	/// <summary>
	/// A single style item that can be cascaded down
	/// Can manage a color, boolean, enum, etc.
	/// </summary>
	public class FontStyleItem : StyleItem<IFontBuddy>
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="styleType"></param>
		/// <param name="style"></param>
		public FontStyleItem(StyleItemType styleType, IFontBuddy style)
			: base(styleType, style)
		{
		}

		/// <summary>
		/// Copy Constrcutor
		/// </summary>
		/// <param name="styleItem"></param>
		public FontStyleItem(FontStyleItem styleItem)
			: base(styleItem.StyleType, styleItem.Style)
		{
		}

		#endregion //Methods
	}
}