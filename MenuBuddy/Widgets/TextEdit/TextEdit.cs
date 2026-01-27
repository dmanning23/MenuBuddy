using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework.Content;

namespace MenuBuddy
{
	/// <summary>
	/// A text input widget that opens a <see cref="TextEditScreen"/> for keyboard-based text editing when clicked.
	/// </summary>
	public class TextEdit : BaseTextEdit
	{
		#region Methods

		/// <summary>
		/// Initializes a new <see cref="TextEdit"/> with managed fonts from the content manager.
		/// </summary>
		/// <param name="text">The initial text value.</param>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontSize">The font size category. Defaults to <see cref="FontSize.Medium"/>.</param>
		public TextEdit(string text, ContentManager content, FontSize fontSize = FontSize.Medium) : base(text, content, fontSize)
		{
			OnClick += CreateTextPad;
		}

		/// <summary>
		/// Initializes a new <see cref="TextEdit"/> with pre-loaded fonts.
		/// </summary>
		/// <param name="text">The initial text value.</param>
		/// <param name="font">The font to use for normal rendering.</param>
		/// <param name="highlightedFont">An optional font to use when highlighted.</param>
		public TextEdit(string text, IFontBuddy font, IFontBuddy highlightedFont = null) : base (text, font, highlightedFont)
		{
			OnClick += CreateTextPad;
		}

		/// <summary>
		/// Creates and displays a <see cref="TextEditScreen"/> for keyboard text input.
		/// </summary>
		/// <param name="obj">The source of the click event.</param>
		/// <param name="e">The click event arguments.</param>
		public async void CreateTextPad(object obj, ClickEventArgs e)
		{
			//create the dropdown screen
			var numpad = new TextEditScreen(this);

			//add the screen over the current one
			await Screen.ScreenManager.AddScreen(numpad);
		}

		#endregion //Methods
	}
}
