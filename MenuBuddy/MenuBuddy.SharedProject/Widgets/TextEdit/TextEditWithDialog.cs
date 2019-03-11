using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	public class TextEditWithDialog : BaseTextEdit
	{
		#region Properties

		/// <summary>
		/// TExt to use as the title of the message box that is popped up 
		/// </summary>
		public string MessageBoxTitle { get; set; }

		/// <summary>
		/// Text to use as the description of the message box
		/// </summary>
		public string MessageBoxDescription { get; set; }

		/// <summary>
		/// Event that fires before the dialog is displayed
		/// </summary>
		public event EventHandler<ClickEventArgs> OnPopupDialog;

		#endregion //Properties

		#region Methods

		public TextEditWithDialog(string text, ContentManager content, FontSize fontSize = FontSize.Medium) : base(text, content, fontSize)
		{
			OnClick += TextEditWithDialog_OnClick;
		}

		public TextEditWithDialog(string text, IFontBuddy font, IFontBuddy highlightedFont = null) : base(text, font, highlightedFont)
		{
			OnClick += TextEditWithDialog_OnClick;
		}

		private void TextEditWithDialog_OnClick(object sender, ClickEventArgs e)
		{
			OnPopupDialog?.Invoke(sender, e);

			Task.Run(async () =>
			{
				var result = await KeyboardInput.Show(MessageBoxTitle, MessageBoxDescription, Text, IsPassword);
				if (null != result)
				{
					SetText(result);
				}
			});
		}

		#endregion //Methods
	}
}
