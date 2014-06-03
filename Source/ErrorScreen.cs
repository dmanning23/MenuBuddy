using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;

#if NETWORKING
using Microsoft.Xna.Framework.Net;
#endif

namespace MenuBuddy
{
	/// <summary>
	/// Specialized message box subclass, used to display network error messages.
	/// </summary>
	public class ErrorScreen : MessageBoxScreen
	{
		#region Methods

		/// <summary>
		/// Constructs an error message box from the specified exception.
		/// </summary>
		public ErrorScreen(Exception exception) : base(GetErrorMessage(exception), false)
		{
		}

		/// <summary>
		/// Converts a network exception into a user friendly error message.
		/// </summary>
		private static string GetErrorMessage(Exception exception)
		{
			//Trace.WriteLine("Network operation threw " + exception);

			// Is this a GamerPrivilegeException?
			if (exception is GamerPrivilegeException)
			{
				if (Guide.IsTrialMode)
				{
					return "This functionality is not available in trial mode";
				}
				else
				{
					return "You must sign in a suitable gamer profile \nin order to access this functionality";
				}
			}

#if NETWORKING

	// Is it a NetworkSessionJoinException?
			NetworkSessionJoinException joinException = exception as NetworkSessionJoinException;

			if (joinException != null)
			{
				switch (joinException.JoinError)
				{
					case NetworkSessionJoinError.SessionFull:
					return "This session is already full";

					case NetworkSessionJoinError.SessionNotFound:
					return "Session not found. It may have ended, \nor there may be no network connectivity \nbetween the local machine and session host";

					case NetworkSessionJoinError.SessionNotJoinable:
					return "You must wait for the host to return to \nthe lobby before you can join this session";
				}
			}

			// Is this a NetworkNotAvailableException?
			if (exception is NetworkNotAvailableException)
			{
				return "Networking is turned \noff or not connected";
			}

			// Is this a NetworkException?
			if (exception is NetworkException)
			{
				return "There was an error while \naccessing the network";
			}
#endif

			// Otherwise just a generic error message.
			return "An unknown error occurred:\n" + exception.ToString();
		}

		/// <summary>
		/// Dont load any content for this screen, because a lot of the time the missing content will be the gradient texture.
		/// </summary>
		public override void LoadContent()
		{
		}

		/// <summary>
		/// Draws the message box.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			//Draw at the upper left to try and fit as much text as possible on teh screen
			Vector2 textPosition = new Vector2(ScreenRect.Left, ScreenRect.Top);

			ScreenManager.SpriteBatchBegin();

			// Darken down any other screens that were drawn beneath the popup.
			ScreenManager.FadeBackBufferToBlack(TransitionAlpha * 2.0f / 3.0f);

			// Draw the message box text.
			ScreenManager.SpriteBatch.DrawString(ScreenManager.MessageBoxFont, Message, textPosition, Color.White);

			ScreenManager.SpriteBatchEnd();
		}

		#endregion
	}
}