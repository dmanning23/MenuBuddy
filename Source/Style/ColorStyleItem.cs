using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// A color style item that can be cascaded down
	/// </summary>
	public class ColorStyleItem : StyleItem<Color>
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="styleType"></param>
		/// <param name="style"></param>
		public ColorStyleItem(StyleItemType styleType, Color style)
			: base(styleType, style)
		{
		}

		/// <summary>
		/// Copy Constrcutor
		/// </summary>
		/// <param name="styleItem"></param>
		public ColorStyleItem(ColorStyleItem styleItem)
			: base(styleItem.StyleType, styleItem.Style)
		{
		}

		#endregion //Methods
	}
}