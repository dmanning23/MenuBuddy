
using Microsoft.Xna.Framework.Audio;

namespace MenuBuddy
{
	/// <summary>
	/// A sound effect style item that can be cascaded down
	/// </summary>
	public class SoundStyleItem : StyleItem<SoundEffect>
	{
		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="styleType"></param>
		/// <param name="style"></param>
		public SoundStyleItem(StyleItemType styleType, SoundEffect style)
			: base(styleType, style)
		{
		}

		/// <summary>
		/// Copy Constrcutor
		/// </summary>
		/// <param name="styleItem"></param>
		public SoundStyleItem(SoundStyleItem styleItem)
			: base(styleItem.StyleType, styleItem.Style)
		{
		}

		#endregion //Methods
	}
}