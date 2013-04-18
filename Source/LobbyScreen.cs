using System;
using HadoukInput;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Net;
using Microsoft.Xna.Framework.GamerServices;

namespace MenuBuddy
{
	/// <summary>
	/// The lobby screen provides a place for gamers to congregate before starting
	/// the actual gameplay. It displays a list of all the gamers in the session,
	/// and indicates which ones are currently talking. Each gamer can press a button
	/// to mark themselves as ready: gameplay will begin after everyone has done this.
	/// </summary>
	class LobbyScreen : GameScreen
	{
		#region Fields

		NetworkSession networkSession;

		Texture2D isReadyTexture;
		Texture2D hasVoiceTexture;
		Texture2D isTalkingTexture;
		Texture2D voiceMutedTexture;

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new lobby screen.
		/// </summary>
		public LobbyScreen(NetworkSession networkSession)
		{
			this.networkSession = networkSession;

			TransitionOnTime = TimeSpan.FromSeconds(0.5);
			TransitionOffTime = TimeSpan.FromSeconds(0.5);
		}

		/// <summary>
		/// Loads graphics content used by the lobby screen.
		/// </summary>
		public override void LoadContent()
		{
			base.LoadContent();
			ContentManager content = ScreenManager.Game.Content;

			//TODO: these should be added to a content project

			isReadyTexture = content.Load<Texture2D>(@"Textures\chat_ready");
			hasVoiceTexture = content.Load<Texture2D>(@"Textures\chat_able");
			isTalkingTexture = content.Load<Texture2D>(@"Textures\chat_talking");
			voiceMutedTexture = content.Load<Texture2D>(@"Textures\chat_mute");
		}

		#endregion

		#region Update

		/// <summary>
		/// Updates the lobby screen.
		/// </summary>
		public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
		{
			base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

			if (!IsExiting)
			{
				if (networkSession.SessionState == NetworkSessionState.Playing)
				{
					// Check if we should leave the lobby and begin gameplay.
					// We pass null as the controlling player, because the networked
					// gameplay screen accepts input from any local players who
					// are in the session, not just a single controlling player.
					LoadingScreen.Load(ScreenManager,
						true,
						null,
						new CBlockScreen(),
						new CGameplayScreen(networkSession));
				}
				else if (networkSession.IsHost && networkSession.IsEveryoneReady && (networkSession.AllGamers.Count >= 2))
				{
					// The host checks whether everyone has marked themselves as ready, 
					//and starts the game in response.
					networkSession.StartGame();
				}
			}
		}

		/// <summary>
		/// Handles user input for all the local gamers in the session. Unlike most
		/// screens, which use the InputState class to combine input data from all
		/// gamepads, the lobby needs to individually mark specific players as ready,
		/// so it loops over all the local gamers and reads their inputs individually.
		/// </summary>
		public override void HandleInput(InputState input, GameTime rGameTime)
		{
			foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
			{
				PlayerIndex playerIndex = gamer.SignedInGamer.PlayerIndex;
				PlayerIndex unwantedOutput;

				if (input.IsMenuSelect(playerIndex, out unwantedOutput))
				{
					HandleMenuSelect(gamer);
					return;
				}
				else if (input.IsMenuCancel(playerIndex, out unwantedOutput))
				{
					HandleMenuCancel(gamer);
					return;
				}
			}

			//if this is a local game and only one gamer in session, check if anyone else wants to join
			if ((NetworkSessionType.Local == networkSession.SessionType) &&
				(networkSession.AllGamers.Count == 1))
			{
				PlayerIndex ePlayer;
				if (input.IsNewButtonPress(Buttons.A, null, out ePlayer))
				{
					//check if that player is logged in already
					bool bSessionGamer = false;
					foreach (LocalNetworkGamer gamer in networkSession.LocalGamers)
					{
						if (gamer.SignedInGamer.PlayerIndex == ePlayer)
						{
							bSessionGamer = true;
							break;
						}
					}

					if (!bSessionGamer)
					{
						//ok, this gamer is not part of the session

						//check if he is already signed in
						if (Gamer.SignedInGamers[ePlayer] != null)
						{
							//add the gamer to the session!
							networkSession.AddLocalGamer(Gamer.SignedInGamers[ePlayer]);
						}
						else
						{
							try
							{
								if (!Guide.IsVisible)
								{
									//ask the gamer to sign in
									Guide.ShowSignIn(1, false);
								}
							}
							catch (Exception exception)
							{
								NetworkErrorScreen errorScreen = new NetworkErrorScreen(exception);
								ScreenManager.AddScreen(errorScreen, ControllingPlayer);
							}
						}
					}
				}
			}
		}

		/// <summary>
		/// Handle MenuSelect inputs by marking ourselves as ready.
		/// </summary>
		void HandleMenuSelect(LocalNetworkGamer gamer)
		{
			if (!gamer.IsReady)
			{
				gamer.IsReady = true;
			}
			else if (gamer.IsHost)
			{
				// The host has an option to force starting the game, even if not
				// everyone has marked themselves ready. If they press select twice
				// in a row, the first time marks the host ready, then the second
				// time we ask if they want to force start.
				MessageBoxScreen messageBox = new MessageBoxScreen(
					"Are you sure you want to start the game,\neven though not all players are ready?");
				messageBox.Accepted += ConfirmStartGameMessageBoxAccepted;
				ScreenManager.AddScreen(messageBox, gamer.SignedInGamer.PlayerIndex);
			}
		}

		/// <summary>
		/// Event handler for when the host selects ok on the "are you sure
		/// you want to start even though not everyone is ready" message box.
		/// </summary>
		void ConfirmStartGameMessageBoxAccepted(object sender, PlayerIndexEventArgs e)
		{
			if (networkSession.SessionState == NetworkSessionState.Lobby)
			{
				networkSession.StartGame();
			}
		}

		/// <summary>
		/// Handle MenuCancel inputs by clearing our ready status, or if it is
		/// already clear, prompting if the user wants to leave the session.
		/// </summary>
		void HandleMenuCancel(LocalNetworkGamer gamer)
		{
			if (gamer.IsReady)
			{
				gamer.IsReady = false;
			}
			else
			{
				PlayerIndex playerIndex = gamer.SignedInGamer.PlayerIndex;
				NetworkSessionComponent.LeaveSession(ScreenManager, playerIndex);
			}
		}

		#endregion

		#region Draw

		/// <summary>
		/// Draws the lobby screen.
		/// </summary>
		public override void Draw(GameTime gameTime)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			SpriteFont font = ScreenManager.Font;

			// Make the lobby slide into place during transitions.
			float transitionOffset = (float)Math.Pow(TransitionPosition, 2);

			float fMenuPositionX = ScreenRect.Center.X;
			if (ScreenState == EScreenState.TransitionOn)
			{
				fMenuPositionX -= transitionOffset * 256;
			}
			else
			{
				fMenuPositionX += transitionOffset * 512;
			}

			spriteBatch.Begin();

			// Draw all the gamers in the session.
			for (int i = 0; i < networkSession.AllGamers.Count; i++)
			{
				NetworkGamer gamer = networkSession.AllGamers[i];

				//Get the menu entry Y position
				float fMenuPositionY = ScreenRect.Center.Y + (i * font.MeasureString(gamer.Gamertag).Y);
				DrawGamer(gamer, new Vector2(fMenuPositionX, fMenuPositionY));
			}

			//if this is local multiplayer and only 1 player signed in, display message
			if ((NetworkSessionType.Local == networkSession.SessionType) &&
				(networkSession.AllGamers.Count == 1))
			{
				string strText = "Press (a) to join";
				int gamertagHeight = (int)font.MeasureString(strText).Y;
				int gamertagWidth = (int)font.MeasureString(strText).X;

				float fMenuPositionY = ScreenRect.Center.Y + (1 * font.MeasureString(strText).Y);

				spriteBatch.DrawString(
				font,
				strText,
				new Vector2(fMenuPositionX - (gamertagWidth / 2.0f) + (gamertagHeight / 2.0f), fMenuPositionY),
				FadeAlphaDuringTransition(Color.Yellow));
			}

			DrawMenuTitle("Lobby", 1.0f);

			spriteBatch.End();
		}

		/// <summary>
		/// Helper draws the gamertag and status icons for a single NetworkGamer.
		/// </summary>
		void DrawGamer(NetworkGamer gamer, Vector2 position)
		{
			SpriteBatch spriteBatch = ScreenManager.SpriteBatch;
			SpriteFont font = ScreenManager.Font;

			//get teh text for this gamer dude
			string text = gamer.Gamertag;
			if (gamer.IsHost)
			{
				text += " (host)";
			}

			//the dimensions of teh gamertag string
			int gamertagHeight = (int)font.MeasureString(text).Y;
			int gamertagWidth = (int)font.MeasureString(text).X;

			//create the rectangle for drawing the icon
			Rectangle iconRect = new Rectangle(
				(int)(position.X - (gamertagWidth / 2.0f) - (gamertagHeight * .75f)),
				(int)(position.Y + (gamertagHeight * .25f)),
				gamertagHeight / 2,
				gamertagHeight / 2);

			// Draw the "is ready" icon.
			if (gamer.IsReady)
			{
				spriteBatch.Draw(isReadyTexture, iconRect, FadeAlphaDuringTransition(Color.Lime));
			}
			else if (gamer.IsMutedByLocalUser)
			{
				// Draw the "is muted", "is talking", or "has voice" icon.
				spriteBatch.Draw(voiceMutedTexture, iconRect, FadeAlphaDuringTransition(Color.Red));
			}
			else if (gamer.IsTalking)
			{
				spriteBatch.Draw(isTalkingTexture, iconRect, FadeAlphaDuringTransition(Color.Yellow));
			}
			else if (gamer.HasVoice)
			{
				spriteBatch.Draw(hasVoiceTexture, iconRect, FadeAlphaDuringTransition(Color.White));
			}

			// Draw the gamertag, normally in white, but yellow for local players.
			Color color = (gamer.IsLocal) ? Color.Yellow : Color.White;

			spriteBatch.DrawString(
				font,
				text,
				new Vector2(position.X - (gamertagWidth / 2.0f) + (gamertagHeight / 2.0f), position.Y),
				FadeAlphaDuringTransition(color));
		}

		#endregion
	}
}
