using System;
using System.Diagnostics;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Net;

namespace MenuBuddy
{
	/// <summary>
	/// Specialized message box subclass, used to display network error messages.
	/// </summary>
	public class NetworkErrorScreen : MessageBoxScreen
	{
		#region Methods

		/// <summary>
		/// Constructs an error message box from the specified exception.
		/// </summary>
		public NetworkErrorScreen(Exception exception) : base(GetErrorMessage(exception), false) { }

		/// <summary>
		/// Converts a network exception into a user friendly error message.
		/// </summary>
		static string GetErrorMessage(Exception exception)
		{
			//Trace.WriteLine("Network operation threw " + exception);

			// Is this a GamerPrivilegeException?
			if (exception is GamerPrivilegeException)
			{
				if (Guide.IsTrialMode)
					return "This functionality is not available in trial mode";
				else
					return "You must sign in a suitable gamer profile \nin order to access this functionality";
			}

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

			// Otherwise just a generic error message.
			return "An unknown error occurred:\n" + exception.Message;
		}

		#endregion
	}
}
