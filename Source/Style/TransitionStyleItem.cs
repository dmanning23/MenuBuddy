
namespace MenuBuddy
{
	/// <summary>
	/// A bool style item that can be cascaded down
	/// </summary>
	public class TransitionStyleItem : StyleItem<TransitionType>
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="styleType"></param>
		/// <param name="style"></param>
		public TransitionStyleItem(StyleItemType styleType, TransitionType style)
			: base(styleType, style)
		{
		}

		/// <summary>
		/// Copy Constrcutor
		/// </summary>
		/// <param name="styleItem"></param>
		public TransitionStyleItem(TransitionStyleItem styleItem)
			: base(styleItem.StyleType, styleItem.Style)
		{
		}

		#endregion //Methods
	}
}