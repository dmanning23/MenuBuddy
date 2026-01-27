using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// A modal screen that captures keyboard input for editing text. Displays a cursor and supports
	/// alphanumeric keys, symbols, backspace, enter, and escape.
	/// </summary>
	public class TextEditScreen : WidgetScreen, IHighlightable
	{
		#region Properties

		private KeyboardState _prev;
		private KeyboardState _current;

		/// <summary>
		/// The text edit widget that created this screen.
		/// </summary>
		public ITextEdit TextWidget { get; set; }

		/// <summary>
		/// The transition object for this screen (set to no transition).
		/// </summary>
		private ITransitionObject TransitionObject { get; set; }

		/// <summary>
		/// The current text being edited, before it is committed back to the widget.
		/// </summary>
		private string CorrectText { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="TextEditScreen"/> for editing the specified text widget.
		/// </summary>
		/// <param name="widget">The text edit widget whose text will be edited.</param>
		public TextEditScreen(ITextEdit widget) : base("TextEdit")
		{
			TransitionObject = new WipeTransitionObject(TransitionWipeType.None);
			Transition.OnTime = 0f;
			Transition.OffTime = 0f;
			TextWidget = widget;
			CoverOtherScreens = false;
			CorrectText = TextWidget.Text;

			_current = Keyboard.GetState();
			_prev = Keyboard.GetState();
		}

		/// <inheritdoc/>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			SetTextWidgetText(false);
		}

		/// <summary>
		/// Processes keyboard input each frame, handling character entry, backspace, enter, and escape keys.
		/// </summary>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			//get the current keyboard state
			_current = Keyboard.GetState();

			//check if the shift keys are held down
			var shift = CheckShiftKeys();

			//check if the user hit enter or escape
			if (CheckKey(Keys.Enter) || CheckKey(Keys.Escape))
			{
				ExitScreen();
			}

			//check for backspace
			if (CheckKey(Keys.Back))
			{
				var prevText = CorrectText;
				if (!string.IsNullOrEmpty(prevText))
				{
					CorrectText = prevText.Substring(0, prevText.Length - 1);
					SetTextWidgetText(false);
				}
			}

			//check for space key
			if (CheckKey(Keys.Space))
			{
				AppendText(" ");
			}

			//check for various keys
			CheckAndAppendText(Keys.D0, shift);
			CheckAndAppendText(Keys.D1, shift);
			CheckAndAppendText(Keys.D2, shift);
			CheckAndAppendText(Keys.D3, shift);
			CheckAndAppendText(Keys.D4, shift);
			CheckAndAppendText(Keys.D5, shift);
			CheckAndAppendText(Keys.D6, shift);
			CheckAndAppendText(Keys.D7, shift);
			CheckAndAppendText(Keys.D8, shift);
			CheckAndAppendText(Keys.D9, shift);
			CheckAndAppendText(Keys.OemSemicolon, shift);
			CheckAndAppendText(Keys.OemPlus, shift);
			CheckAndAppendText(Keys.OemComma, shift);
			CheckAndAppendText(Keys.OemMinus, shift);
			CheckAndAppendText(Keys.OemPeriod, shift);
			CheckAndAppendText(Keys.OemQuestion, shift);
			CheckAndAppendText(Keys.OemOpenBrackets, shift);
			CheckAndAppendText(Keys.OemPipe, shift);
			CheckAndAppendText(Keys.OemCloseBrackets, shift);
			CheckAndAppendText(Keys.OemQuotes, shift);

			//Check all the keys
			for (var key = Keys.A; key <= Keys.Z; key++)
			{
				if (CheckKey(key))
				{
					//check if shift is held down
					AppendText(shift ? key.ToString() : key.ToString().ToLower());
				}
			}

			_prev = _current;
		}

		/// <summary>
		/// Checks whether the specified key was just pressed (rising edge).
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <returns><c>true</c> if the key was pressed this frame and not the previous frame.</returns>
		protected bool CheckKey(Keys key)
		{
			return (_current.IsKeyDown(key) && !_prev.IsKeyDown(key));
		}

		/// <summary>
		/// Checks whether either shift key is currently held down.
		/// </summary>
		/// <returns><c>true</c> if left or right shift is held.</returns>
		protected bool CheckShiftKeys()
		{
			return _current.IsKeyDown(Keys.LeftShift) || _current.IsKeyDown(Keys.RightShift);
		}

		/// <summary>
		/// Appends text to the current edit string and updates the display.
		/// </summary>
		/// <param name="appendText">The text to append.</param>
		protected void AppendText(string appendText)
		{
			CorrectText = CorrectText + appendText;
			SetTextWidgetText(false);
		}

		/// <summary>
		/// Checks whether the specified key was just pressed, and if so, appends its character to the edit string.
		/// </summary>
		/// <param name="key">The key to check.</param>
		/// <param name="shift">Whether a shift key is held, affecting the character produced.</param>
		protected void CheckAndAppendText(Keys key, bool shift)
		{
			//check if the key is down
			if (CheckKey(key))
			{
				//append the text for that key to the edit text
				AppendText(KeyChar(key, shift));
			}
		}

		/// <summary>
		/// Checks highlighting and prevents items on underlying screens from being highlighted.
		/// </summary>
		/// <param name="highlight">The highlight event arguments.</param>
		/// <returns><c>true</c> if this screen is active, consuming the highlight.</returns>
		public override bool CheckHighlight(HighlightEventArgs highlight)
		{
			base.CheckHighlight(highlight);

			//don't highlight other items when this screen is on top
			return IsActive;
		}

		/// <summary>
		/// Handles clicks on this screen. Clicking outside the screen items exits the editing screen.
		/// </summary>
		/// <param name="click">The click event arguments.</param>
		/// <returns>Always <c>true</c>, consuming the click.</returns>
		public override bool CheckClick(ClickEventArgs click)
		{
			//check if the user clicked one of the items
			if (!base.CheckClick(click))
			{
				//Once the user clicks anywhere on this screen, it gets popped off
				ExitScreen();
			}

			return true;
		}

		/// <summary>
		/// Commits the edited text back to the widget and exits this screen.
		/// </summary>
		public override void ExitScreen()
		{
			SetTextWidgetText(true);
			base.ExitScreen();
		}

		/// <summary>
		/// Converts a keyboard key to its character string representation, accounting for shift state.
		/// </summary>
		/// <param name="key">The key to convert.</param>
		/// <param name="shiftKey">Whether shift is held, selecting the alternate character.</param>
		/// <returns>The character string for the key, or an empty string if unmapped.</returns>
		private static string KeyChar(Keys key, bool shiftKey)
		{
			if (!shiftKey)
			{
				switch (key)
				{
					case Keys.D0: { return "0"; }
					case Keys.D1: { return "1"; }
					case Keys.D2: { return "2"; }
					case Keys.D3: { return "3"; }
					case Keys.D4: { return "4"; }
					case Keys.D5: { return "5"; }
					case Keys.D6: { return "6"; }
					case Keys.D7: { return "7"; }
					case Keys.D8: { return "8"; }
					case Keys.D9: { return "9"; }
					case Keys.OemSemicolon: { return ";"; }
					case Keys.OemPlus: { return "="; }
					case Keys.OemComma: { return ","; }
					case Keys.OemMinus: { return "-"; }
					case Keys.OemPeriod: { return "."; }
					case Keys.OemQuestion: { return "/"; }
					case Keys.OemOpenBrackets: { return "["; }
					case Keys.OemPipe: { return "\\"; }
					case Keys.OemCloseBrackets: { return "]"; }
					case Keys.OemQuotes: { return "'"; }
					default: { return ""; }
				}
			}
			else
			{
				switch (key)
				{
					case Keys.D0: { return ")"; }
					case Keys.D1: { return "!"; }
					case Keys.D2: { return "@"; }
					case Keys.D3: { return "#"; }
					case Keys.D4: { return "$"; }
					case Keys.D5: { return "%"; }
					case Keys.D6: { return "^"; }
					case Keys.D7: { return "&"; }
					case Keys.D8: { return "*"; }
					case Keys.D9: { return "("; }
					case Keys.OemSemicolon: { return ":"; }
					case Keys.OemPlus: { return "+"; }
					case Keys.OemComma: { return "<"; }
					case Keys.OemMinus: { return "_"; }
					case Keys.OemPeriod: { return ">"; }
					case Keys.OemQuestion: { return "?"; }
					case Keys.OemOpenBrackets: { return "{"; }
					case Keys.OemPipe: { return "|"; }
					case Keys.OemCloseBrackets: { return "}"; }
					case Keys.OemQuotes: { return "\""; }
					default: { return ""; }
				}
			}
		}

		/// <summary>
		/// Updates the text widget's display. While editing, appends a cursor character. On exit, commits the final text.
		/// </summary>
		/// <param name="exiting">Whether the screen is exiting (commits text via SetText) or still editing (displays cursor).</param>
		private void SetTextWidgetText(bool exiting)
		{
			if (exiting)
			{
				TextWidget.SetText(CorrectText);
			}
			else
			{
				TextWidget.Text = CorrectText + "_";
			}
		}

		#endregion //Methods
	}
}
