using FontBuddyLib;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace MenuBuddy
{
	public class TextEditWithDialog : BaseTextEdit
	{
		public string MessageBoxTitle { get; set; }
		public string MessageBoxDescription { get; set; }

		public TextEditWithDialog(string text, ContentManager content, FontSize fontSize = FontSize.Medium) : base(text, content, fontSize)
		{
			OnClick += TextEditWithDialog_OnClick;
		}

		public TextEditWithDialog(string text, IFontBuddy font, IFontBuddy highlightedFont = null) : base(text, font, highlightedFont)
		{
			OnClick += TextEditWithDialog_OnClick;
		}

		private void TextEditWithDialog_OnClick(object sender, InputHelper.ClickEventArgs e)
		{
			Task.Run(async () =>
			{
				var result = await KeyboardInput.Show(MessageBoxTitle, MessageBoxDescription, Text, IsPassword);
				if (null != result)
				{
					Text = result;
				}
			});
		}
	}
}
