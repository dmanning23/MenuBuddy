using FontBuddyLib;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ResolutionBuddy;
using System;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Specialized message box subclass, used to display network error messages.
	/// </summary>
	public class ErrorScreen : WidgetScreen
	{
		#region Fields

		private string _message;

		private Exception _error;

		#endregion //Fields

		#region Methods

		/// <summary>
		/// Constructs an error screen displaying the given message string.
		/// </summary>
		/// <param name="error">The error message to display.</param>
		public ErrorScreen(string error) : base("Error Screen")
		{
			_message = error;
		}

		/// <summary>
		/// Constructs an error message box from the specified exception.
		/// </summary>
		public ErrorScreen(Exception exception) : this(BuildDisplayMessage(exception))
		{
			_error = exception;

			//Dump the full exception, including all inner exceptions and stack traces, to the console.
			Console.WriteLine(exception.ToString());
		}

		/// <summary>
		/// Builds a short, human-readable message for the given exception. Since the useful
		/// information is often buried in an inner exception rather than the top-level one,
		/// this includes the innermost exception's type and message as well.
		/// </summary>
		/// <param name="exception">The exception to describe.</param>
		private static string BuildDisplayMessage(Exception exception)
		{
			var innermost = exception;
			while (null != innermost.InnerException)
			{
				innermost = innermost.InnerException;
			}

			if (innermost == exception)
			{
				return $"{exception.GetType().Name}: {exception.Message}";
			}

			return $"{exception.GetType().Name}: {exception.Message}\n\nCaused by {innermost.GetType().Name}: {innermost.Message}";
		}

		/// <summary>
		/// Dont load any content for this screen, because a lot of the time the missing content will be the gradient texture.
		/// </summary>
		public override async Task LoadContent()
		{
			await base.LoadContent();

			//Pop up a message box with the error message
			await ScreenManager.AddScreen(new OkScreen(_message));

			AddCancelButton();

			AddItem(new Label(_message, Content, FontSize.Small)
			{
				Position = new Point(Resolution.TitleSafeArea.Left, Resolution.TitleSafeArea.Top),
				Horizontal = HorizontalAlignment.Left,
				Vertical = VerticalAlignment.Top
			});
		}

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			ScreenManager.SpriteBatchBegin();
			FadeBackground();
			ScreenManager.SpriteBatchEnd();

			base.Draw(gameTime);
		}

		#endregion //Methods
	}
}