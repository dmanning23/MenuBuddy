using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework.Content;

namespace MenuBuddy
{
	/// <summary>
	/// A TextEdit control. 
	/// This is a widget with text in it. 
	/// When the widget is clicked, the user can edit the text using the keyboard.
	/// After the user finishes editing the text, a special event is fired off.
	/// </summary>
	public class TextEdit : BaseTextEdit
	{
		#region Methods

		public TextEdit(string text, ContentManager content, FontSize fontSize = FontSize.Medium) : base(text, content, fontSize)
		{
			OnClick += CreateTextPad;
		}

		public TextEdit(string text, IFontBuddy font, IFontBuddy highlightedFont = null) : base (text, font, highlightedFont)
		{
			OnClick += CreateTextPad;
		}

		/// <summary>
		/// Method that gets called when the label is clicked to create the numpad.
		/// Adds a new screen with a numpad.
		/// </summary>
		/// <param name="obj"></param>
		/// <param name="e"></param>
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
