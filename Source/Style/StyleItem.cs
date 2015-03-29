using FontBuddyLib;
using Microsoft.Xna.Framework;
using System.Diagnostics;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	/// <summary>
	/// A single style item that can be cascaded down
	/// Can manage a color, boolean, enum, etc.
	/// </summary>
	public abstract class StyleItem<T>
	{
		#region Properties

		/// <summary>
		/// The style being used by the item this dude manages
		/// </summary>
		public T Style { get; set; }

		/// <summary>
		/// The type of item this dude styles
		/// </summary>
		public StyleItemType StyleType { get; private set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="styleType"></param>
		/// <param name="style"></param>
		protected StyleItem(StyleItemType styleType, T style)
		{
			Style = style;
			StyleType = styleType;

#if DEBUG
			if (null == style)
			{
				return;
			}

			//do some error checking for sanity sake
			switch (StyleType)
			{
				case StyleItemType.SelectedFont:
				{
					Debug.Assert(Style is IFontBuddy);
				}
				break;
				case StyleItemType.UnselectedFont:
				{
					Debug.Assert(Style is IFontBuddy);
				}
				break;
				case StyleItemType.SelectedTextColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.UnselectedTextColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.SelectedShadowColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.UnselectedShadowColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.SelectedOutlineColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.UnselectedOutlineColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.SelectedBackgroundColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.UnselectedBackgroundColor:
				{
					Debug.Assert(Style is Color);
				}
				break;
				case StyleItemType.HasOutline:
				{
					Debug.Assert(Style is bool);
				}
				break;
				case StyleItemType.HasBackground:
				{
					Debug.Assert(Style is bool);
				}
				break;
				case StyleItemType.IsQuiet:
				{
					Debug.Assert(Style is bool);
				}
				break;
				case StyleItemType.Transition:
				{
					Debug.Assert(Style is TransitionType);
				}
				break;
				case StyleItemType.SelectedSoundEffect:
				{
					Debug.Assert(Style is SoundEffect);
				}
				break;
				case StyleItemType.SelectionChangeSoundEffect:
				{
					Debug.Assert(Style is SoundEffect);
				}
				break;
				case StyleItemType.Texture:
				{
					Debug.Assert(Style is Texture2D);
				}
				break;
				default:
				{
					Debug.Assert(false);
				}
				break;
			}
#endif
		}

		#endregion //Methods
	}
}