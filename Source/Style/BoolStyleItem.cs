
namespace MenuBuddy
{
	/// <summary>
	/// A bool style item that can be cascaded down
	/// </summary>
	public class BoolStyleItem : StyleItem<bool>
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="styleType"></param>
		/// <param name="style"></param>
		public BoolStyleItem(StyleItemType styleType, bool style)
			: base(styleType, style)
		{
		}

		/// <summary>
		/// Copy Constrcutor
		/// </summary>
		/// <param name="styleItem"></param>
		public BoolStyleItem(BoolStyleItem styleItem)
			: base(styleItem.StyleType, styleItem.Style)
		{
		}

		#endregion //Methods
	}
}