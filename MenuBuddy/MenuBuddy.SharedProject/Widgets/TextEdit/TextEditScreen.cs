using InputHelper;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System.Threading.Tasks;

namespace MenuBuddy
{
	public class TextEditScreen : WidgetScreen, IHighlightable
	{
		#region Properties

		private KeyboardState _prev;
		private KeyboardState _current;

		/// <summary>
		/// The dropdown that created this screen
		/// </summary>
		public ITextEdit TextWidget { get; set; }

		private ITransitionObject TransitionObject { get; set; }

		private string CorrectText { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="widget"></param>
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

		public override async Task LoadContent()
		{
			await base.LoadContent();

			SetTextWidgetText(false);
		}

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

		protected bool CheckKey(Keys key)
		{
			return (_current.IsKeyDown(key) && !_prev.IsKeyDown(key));
		}

		protected bool CheckShiftKeys()
		{
			return _current.IsKeyDown(Keys.LeftShift) || _current.IsKeyDown(Keys.RightShift);
		}

		protected void AppendText(string appendText)
		{
			CorrectText = CorrectText + appendText;
			SetTextWidgetText(false);
		}

		protected void CheckAndAppendText(Keys key, bool shift)
		{
			//check if the key is down
			if (CheckKey(key))
			{
				//append the text for that key to the edit text
				AppendText(KeyChar(key, shift));
			}
		}

		public override bool CheckHighlight(HighlightEventArgs highlight)
		{
			base.CheckHighlight(highlight);

			//don't highlight other items when this dude is on top
			return IsActive;
		}

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

		public override void ExitScreen()
		{
			SetTextWidgetText(true);
			base.ExitScreen();
		}

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
