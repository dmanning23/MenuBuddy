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

		public ErrorScreen(string error) : base("Error Screen")
		{
			_message = error;
		}

		/// <summary>
		/// Constructs an error message box from the specified exception.
		/// </summary>
		public ErrorScreen(Exception exception) : this(exception.Message)
		{
			_error = exception;
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