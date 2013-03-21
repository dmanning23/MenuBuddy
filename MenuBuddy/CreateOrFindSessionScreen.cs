using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace MenuBuddy
{
	/// <summary>
	/// This menu screen lets the user choose whether to create a new
	/// network session, or search for an existing session to join.
	/// </summary>
	class CreateOrFindSessionScreen : MenuScreen
	{
		#region Fields

		NetworkSessionType sessionType;

		#endregion

		#region Initialization

		/// <summary>
		/// Constructor fills in the menu contents.
		/// </summary>
		public CreateOrFindSessionScreen(NetworkSessionType sessionType) : base(GetMultiplayerType(sessionType))
		{
			this.sessionType = sessionType;

			// Create our menu entries.
			MenuEntry createSessionMenuEntry = new MenuEntry("Create Session");
			MenuEntry findSessionsMenuEntry = new MenuEntry("Find Sessions");
			MenuEntry backMenuEntry = new MenuEntry("Back");

			// Hook up menu event handlers.
			createSessionMenuEntry.Selected += CreateSessionMenuEntrySelected;
			findSessionsMenuEntry.Selected += FindSessionsMenuEntrySelected;
			backMenuEntry.Selected += OnCancel;

			// Add entries to the menu.
			MenuEntries.Add(createSessionMenuEntry);
			MenuEntries.Add(findSessionsMenuEntry);
			MenuEntries.Add(backMenuEntry);
		}

		/// <summary>
		/// Helper chooses an appropriate menu title for the specified session type.
		/// </summary>
		static string GetMultiplayerType(NetworkSessionType sessionType)
		{
			switch (sessionType)
			{
				case NetworkSessionType.PlayerMatch:
				return "LIVE";

				case NetworkSessionType.SystemLink:
				return "System Link";

				case NetworkSessionType.Local:
				return "Local Multiplayer";

				default:
				throw new NotSupportedException();
			}
		}

		#endregion

		#region Event Handlers

		/// <summary>
		/// Event handler for when the Create Session menu entry is selected.
		/// </summary>
		void CreateSessionMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			// First, we need to make sure a suitable gamer profile is signed in.
			ProfileSignInScreen profileSignIn = new ProfileSignInScreen(sessionType);

			// Hook up an event so once the ProfileSignInScreen is happy,
			// it will activate the CreateOrFindSessionScreen.
			profileSignIn.ProfileSignedIn += delegate
			{
				CreateSessionOperation(e);
			};

			// Activate the ProfileSignInScreen.
			ScreenManager.AddScreen(profileSignIn, e.PlayerIndex);
		}

		void CreateSessionOperation(PlayerIndexEventArgs e)
		{
			try
			{
				// Which local profiles should we include in this session?
				List<SignedInGamer> onlyGamer = new List<SignedInGamer>(1);
				onlyGamer.Add(Gamer.SignedInGamers[e.PlayerIndex]);

				// Begin an asynchronous create network session operation.
				IAsyncResult asyncResult = NetworkSession.BeginCreate(
													sessionType,
													onlyGamer,
													2,
													0, null, null, null);

				// Activate the network busy screen, which will display
				// an animation until this operation has completed.
				NetworkBusyScreen busyScreen = new NetworkBusyScreen(asyncResult);

				busyScreen.OperationCompleted += CreateSessionOperationCompleted;

				ScreenManager.AddScreen(busyScreen, ControllingPlayer);
			}
			catch (Exception exception)
			{
				NetworkErrorScreen errorScreen = new NetworkErrorScreen(exception);
				ScreenManager.AddScreen(errorScreen, ControllingPlayer);
			}
		}

		/// <summary>
		/// Event handler for when the asynchronous create network session operation has completed.
		/// </summary>
		void CreateSessionOperationCompleted(object sender, OperationCompletedEventArgs e)
		{
			try
			{
				// End the asynchronous create network session operation.
				NetworkSession networkSession = NetworkSession.EndCreate(e.AsyncResult);

				// Create a component that will manage the session we just created.
				NetworkSessionComponent.Create(ScreenManager, networkSession);

				// Go to the lobby screen. We pass null as the controlling player,
				// because the lobby screen accepts input from all local players
				// who are in the session, not just a single controlling player.
				ScreenManager.AddScreen(new LobbyScreen(networkSession), null);
			}
			catch (Exception exception)
			{
				NetworkErrorScreen errorScreen = new NetworkErrorScreen(exception);

				ScreenManager.AddScreen(errorScreen, ControllingPlayer);
			}
		}

		/// <summary>
		/// Event handler for when the Find Sessions menu entry is selected.
		/// </summary>
		void FindSessionsMenuEntrySelected(object sender, PlayerIndexEventArgs e)
		{
			try
			{
				// Which local profiles should we include in this session?
				IEnumerable<SignedInGamer> localGamers = NetworkSessionComponent.ChooseGamers(sessionType, ControllingPlayer.Value);

				// Begin an asynchronous find network sessions operation.
				IAsyncResult asyncResult = NetworkSession.BeginFind(sessionType, localGamers, null, null, null);

				// Activate the network busy screen, which will display an animation until this operation has completed.
				NetworkBusyScreen busyScreen = new NetworkBusyScreen(asyncResult);

				busyScreen.OperationCompleted += FindSessionsOperationCompleted;

				ScreenManager.AddScreen(busyScreen, ControllingPlayer);
			}
			catch (Exception exception)
			{
				NetworkErrorScreen errorScreen = new NetworkErrorScreen(exception);
				ScreenManager.AddScreen(errorScreen, ControllingPlayer);
			}
		}

		/// <summary>
		/// Event handler for when the asynchronous find network sessions
		/// operation has completed.
		/// </summary>
		void FindSessionsOperationCompleted(object sender, OperationCompletedEventArgs e)
		{
			GameScreen nextScreen;

			try
			{
				// End the asynchronous find network sessions operation.
				AvailableNetworkSessionCollection availableSessions = NetworkSession.EndFind(e.AsyncResult);

				if (availableSessions.Count == 0)
				{
					// If we didn't find any sessions, display an error.
					availableSessions.Dispose();
					nextScreen = new MessageBoxScreen("No sessions found", false);
				}
				else
				{
					// If we did find some sessions, proceed to the JoinSessionScreen.
					nextScreen = new JoinSessionScreen(availableSessions);
				}
			}
			catch (Exception exception)
			{
				nextScreen = new NetworkErrorScreen(exception);
			}

			ScreenManager.AddScreen(nextScreen, ControllingPlayer);
		}

		#endregion
	}
}
