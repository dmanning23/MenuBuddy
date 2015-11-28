using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	public class StyleSheet
	{
		#region Fields

		//All teh style items in a style sheet

		private FontStyleItem _selectedFontStyle;
		private FontStyleItem _unselectedFontStyle;
		private ColorStyleItem _selectedTextColorStyle;
		private ColorStyleItem _unselectedTextColorStyle;
		private ColorStyleItem _selectedShadowColorStyle;
		private ColorStyleItem _unselectedShadowColorStyle;
		private ColorStyleItem _selectedOutlineColorStyle;
		private ColorStyleItem _unselectedOutlineColorStyle;
		private ColorStyleItem _selectedBackgroundColorStyle;
		private ColorStyleItem _unselectedBackgroundColorStyle;
		private BoolStyleItem _hasOutlineStyle;
		private BoolStyleItem _hasBackgroundStyle;
		private BoolStyleItem _isQuietStyle;
		private TransitionStyleItem _transitionStyle;
		private SoundStyleItem _selectedSoundEffect;
		private SoundStyleItem _selectionChangeSoundEffect;
		private ImageStyleItem _texture;

		#endregion //Fields

		#region Properties

		public string Name { get; set; }

		public IFontBuddy SelectedFont
		{
			get { return _selectedFontStyle.Style; }
			set
			{
				//set the font item
				_selectedFontStyle = new FontStyleItem(StyleItemType.SelectedFont, value);

				//also set teh shadow color
				var shadow = SelectedFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = SelectedShadowColor;
				}
			}
		}

		public IFontBuddy UnselectedFont
		{
			get { return _unselectedFontStyle.Style; }
			set
			{
				_unselectedFontStyle = new FontStyleItem(StyleItemType.UnselectedFont, value);
				var shadow = UnselectedFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = UnselectedShadowColor;
				}
			}
		}

		public Color SelectedTextColor
		{
			get { return _selectedTextColorStyle.Style; }
			set { _selectedTextColorStyle = new ColorStyleItem(StyleItemType.SelectedTextColor, value); }
		}

		public Color UnselectedTextColor
		{
			get { return _unselectedTextColorStyle.Style; }
			set { _unselectedTextColorStyle = new ColorStyleItem(StyleItemType.UnselectedTextColor, value); }
		}

		public Color SelectedShadowColor
		{
			get { return _selectedShadowColorStyle.Style; }
			set
			{
				//set teh shadow color
				_selectedShadowColorStyle = new ColorStyleItem(StyleItemType.SelectedShadowColor, value);

				//tell the selected font to use that shadow color too
				var shadow = SelectedFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = SelectedShadowColor;
				}
			}
		}

		public Color UnselectedShadowColor
		{
			get { return _unselectedShadowColorStyle.Style; }
			set
			{
				//set the shadow color
				_unselectedShadowColorStyle = new ColorStyleItem(StyleItemType.UnselectedShadowColor, value);

				//also set it in the font item
				var shadow = UnselectedFont as ShadowTextBuddy;
				if (null != shadow)
				{
					shadow.ShadowColor = UnselectedShadowColor;
				}
			}
		}

		public Color SelectedOutlineColor
		{
			get { return _selectedOutlineColorStyle.Style; }
			set { _selectedOutlineColorStyle = new ColorStyleItem(StyleItemType.SelectedOutlineColor, value); }
		}

		public Color UnselectedOutlineColor
		{
			get { return _unselectedOutlineColorStyle.Style; }
			set { _unselectedOutlineColorStyle = new ColorStyleItem(StyleItemType.UnselectedOutlineColor, value); }
		}

		public Color SelectedBackgroundColor
		{
			get { return _selectedBackgroundColorStyle.Style; }
			set { _selectedBackgroundColorStyle = new ColorStyleItem(StyleItemType.SelectedBackgroundColor, value); }
		}

		public Color UnselectedBackgroundColor
		{
			get { return _unselectedBackgroundColorStyle.Style; }
			set { _unselectedBackgroundColorStyle = new ColorStyleItem(StyleItemType.UnselectedBackgroundColor, value); }
		}

		public bool HasOutline
		{
			get { return _hasOutlineStyle.Style; }
			set { _hasOutlineStyle = new BoolStyleItem(StyleItemType.HasOutline, value); }
		}

		public bool HasBackground
		{
			get { return _hasBackgroundStyle.Style; }
			set { _hasBackgroundStyle = new BoolStyleItem(StyleItemType.HasBackground, value); }
		}

		public bool IsQuiet
		{
			get { return _isQuietStyle.Style; }
			set { _isQuietStyle = new BoolStyleItem(StyleItemType.IsQuiet, value); }
		}

		public TransitionType Transition
		{
			get { return _transitionStyle.Style; }
			set { _transitionStyle = new TransitionStyleItem(StyleItemType.Transition, value); }
		}

		public SoundEffect SelectedSoundEffect
		{
			get { return _selectedSoundEffect.Style; }
			set { _selectedSoundEffect = new SoundStyleItem(StyleItemType.SelectedSoundEffect, value); }
		}

		public SoundEffect SelectionChangeSoundEffect
		{
			get { return _selectionChangeSoundEffect.Style; }
			set { _selectionChangeSoundEffect = new SoundStyleItem(StyleItemType.SelectionChangeSoundEffect, value); }
		}

		public Texture2D BackgroundImage
		{
			get { return _texture.Style; }
			set { _texture = new ImageStyleItem(StyleItemType.Texture, value); }
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Default constructor
		/// </summary>
		public StyleSheet(string name = "")
		{
			Name = name;
			SelectedFont = null;
			UnselectedFont = null;
			SelectedTextColor = Color.Red;
			UnselectedTextColor = Color.White;
			SelectedShadowColor = Color.Black;
			UnselectedShadowColor = Color.Black;
			SelectedOutlineColor = Color.LightGray;
			UnselectedOutlineColor = Color.LightGray;
			SelectedBackgroundColor = new Color(0.0f, 0.0f, 0.2f);
			UnselectedBackgroundColor = new Color(0.0f, 0.0f, 0.2f);
			HasOutline = false;
			HasBackground = false;
			IsQuiet = false;
			Transition = TransitionType.SlideLeft;
			SelectedSoundEffect = null;
			SelectionChangeSoundEffect = null;
			BackgroundImage = null;
		}

		/// <summary>
		/// Copy constructor: this is how styles sheets are cascaded
		/// </summary>
		/// <param name="styles"></param>
		public StyleSheet(StyleSheet styles)
		{
			//Shallow copy to all the parent style sheet
			Name = styles.Name;
			_selectedFontStyle = styles._selectedFontStyle;
			_unselectedFontStyle = styles._unselectedFontStyle;
			_selectedTextColorStyle = styles._selectedTextColorStyle;
			_unselectedTextColorStyle = styles._unselectedTextColorStyle;
			_selectedShadowColorStyle = styles._selectedShadowColorStyle;
			_unselectedShadowColorStyle = styles._unselectedShadowColorStyle;
			_selectedOutlineColorStyle = styles._selectedOutlineColorStyle;
			_unselectedOutlineColorStyle = styles._unselectedOutlineColorStyle;
			_selectedBackgroundColorStyle = styles._selectedBackgroundColorStyle;
			_unselectedBackgroundColorStyle = styles._unselectedBackgroundColorStyle;
			_hasOutlineStyle = styles._hasOutlineStyle;
			_hasBackgroundStyle = styles._hasBackgroundStyle;
			_isQuietStyle = styles._isQuietStyle;
			_transitionStyle = styles._transitionStyle;
			_selectedSoundEffect = styles._selectedSoundEffect;
			_selectionChangeSoundEffect = styles._selectionChangeSoundEffect;
			_texture = styles._texture;
		}

		public override string ToString()
		{
			return Name;
		}

		#endregion //Methods
	}
}