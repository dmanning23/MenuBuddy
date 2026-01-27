using FontBuddyLib;
using InputHelper;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A text input widget that opens a native keyboard dialog for text editing when clicked.
	/// </summary>
	public class TextEditWithDialog : BaseTextEdit
	{
		#region Properties

		/// <summary>
		/// The title displayed in the native keyboard dialog.
		/// </summary>
		public string MessageBoxTitle { get; set; }

		/// <summary>
		/// The description displayed in the native keyboard dialog.
		/// </summary>
		public string MessageBoxDescription { get; set; }

		/// <summary>
		/// Raised just before the native keyboard dialog is displayed.
		/// </summary>
		public event EventHandler<ClickEventArgs> OnPopupDialog;

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="TextEditWithDialog"/> with managed fonts from the content manager.
		/// </summary>
		/// <param name="text">The initial text value.</param>
		/// <param name="content">The content manager used to load font resources.</param>
		/// <param name="fontSize">The font size category. Defaults to <see cref="FontSize.Medium"/>.</param>
		public TextEditWithDialog(string text, ContentManager content, FontSize fontSize = FontSize.Medium) : base(text, content, fontSize)
		{
			OnClick += TextEditWithDialog_OnClick;
		}

		/// <summary>
		/// Initializes a new <see cref="TextEditWithDialog"/> with pre-loaded fonts.
		/// </summary>
		/// <param name="text">The initial text value.</param>
		/// <param name="font">The font to use for normal rendering.</param>
		/// <param name="highlightedFont">An optional font to use when highlighted.</param>
		public TextEditWithDialog(string text, IFontBuddy font, IFontBuddy highlightedFont = null) : base(text, font, highlightedFont)
		{
			OnClick += TextEditWithDialog_OnClick;
		}

		/// <summary>
		/// Opens the native keyboard dialog for text input. Fires <see cref="OnPopupDialog"/> first,
		/// then shows the dialog and commits the result if the user confirms.
		/// </summary>
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
