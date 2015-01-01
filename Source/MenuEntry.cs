using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// Helper class represents a single entry in a MenuScreen. By default this
	/// just draws the entry text string, but it can be customized to display menu
	/// entries in different ways. This also provides an event that will be raised
	/// when the menu entry is selected.
	/// </summary>
	public class MenuEntry
	{
		#region Fields

		/// <summary>
		/// Gets or sets the position of this menu entry.
		/// Only used if MenuScreen.CascadeMenuEntries is set to false.
		/// </summary>
		public Vector2 Position { get; set; }

		/// <summary>
		/// get or set the width of the selection box of this menu entry.
		/// Only used if TextSelectionRect.TextSelectionRect is set to false.
		/// </summary>
		public float Width { get; set; }

		/// <summary>
		/// get or set the Height of the selection box of this menu entry.
		/// Only used if TextSelectionRect.TextSelectionRect is set to false.
		/// </summary>
		public float Height { get; set; }

		/// <summary>
		/// Take everything into consideration: TouchMenus, CascadeMenuEntries, & TextSelectionRect
		/// the menu entry will set this button rect when it is updated.
		/// </summary>
		public Rectangle ButtonRect { get; set; }

		protected float m_fSelectionFade;

		/// <summary>
		/// A description of the function of the menu entry.
		/// </summary>
		public string Description { get; set; }

		#endregion

		#region Properties

		/// <summary>
		/// The text rendered for this entry.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Tracks a fading selection effect on the entry.
		/// </summary>
		/// <remarks>
		/// The entries transition out of the selection effect when they are deselected.
		/// </remarks>
		public float SizeMultiplier { get; set; }

		/// <summary>
		/// Font used to display the menu entry when it is NOT selected.
		/// </summary>
		public ShadowTextBuddy ShadowText { get; private set; }

		/// <summary>
		/// Font used to display the menu entry when it IS selected
		/// </summary>
		/// <value>The pulsate text.</value>
		public PulsateBuddy PulsateText { get; private set; }

		/// <summary>
		/// The color to draw this item when it is not selected
		/// </summary>
		/// <value>The color of the non selected.</value>
		public Color NonSelectedColor { get; set; }

		/// <summary>
		/// Whether or not to play a sound when the user hits left/right on a menu entry
		/// </summary>
		public virtual bool PlayLeftRightSound
		{
			get
			{
				return false;
			}
		}

		/// <summary>
		/// whether or not to use the MenuFont or MessageBoxFont
		/// </summary>
		private bool MessageBoxEntry { get; set; }

		#endregion

		#region Events

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler Left;

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler Right;

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler<PlayerIndexEventArgs> Selected;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		protected internal virtual void OnSelectEntry(PlayerIndex playerIndex)
		{
			if (Selected != null)
			{
				Selected(this, new PlayerIndexEventArgs(playerIndex));
			}
		}

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		protected internal virtual void OnLeftEntry()
		{
			if (Left != null)
			{
				Left(this, new EventArgs());
			}
		}

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		protected internal virtual void OnRightEntry()
		{
			if (Right != null)
			{
				Right(this, new EventArgs());
			}
		}

		#endregion

		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public MenuEntry(string strText, bool messageBoxEntry = false)
		{
			MessageBoxEntry = messageBoxEntry;
			Text = strText;
			SizeMultiplier = 1.0f;
			Width = 768.0f;
			Height = 128.0f;

			NonSelectedColor = Color.White;

			ShadowText = new ShadowTextBuddy();
			PulsateText = new PulsateBuddy();
			if (messageBoxEntry)
			{
				PulsateText.PulsateSize *= 0.25f;
			}
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Updates the menu entry.
		/// </summary>
		public virtual void Update(MenuScreen screen, bool isSelected, GameTime gameTime)
		{
			// When the menu selection changes, entries gradually fade between
			// their selected and deselected appearance, rather than instantly
			// popping to the new state.
			float fadeSpeed = (float)gameTime.ElapsedGameTime.TotalSeconds * 4;

			if (isSelected)
			{
				m_fSelectionFade = Math.Min(m_fSelectionFade + fadeSpeed, 1);
			}
			else
			{
				m_fSelectionFade = Math.Max(m_fSelectionFade - fadeSpeed, 0);
			}
		}

		/// <summary>
		/// Draws the menu entry. This can be overridden to customize the appearance.
		/// </summary>
		public virtual void Draw(MenuScreen screen, Vector2 position, bool isSelected, GameClock gameTime)
		{
			// Draw the selected entry in yellow, otherwise white.
			Color color = isSelected ? Color.Red : NonSelectedColor;

			// Modify the alpha to fade text out during transitions.
			float alpha = (screen.TransitionAlpha * 255.0f);
			color.A = Convert.ToByte(alpha);
			Color backgroundColor = Color.Black;
			backgroundColor.A = Convert.ToByte(alpha);

			//set teh rect around this entry
			//create the rect we need
			if (screen.TextSelectionRect)
			{
				//use the rect of the text
				float width = GetWidth(screen) * 1.1f;
				ButtonRect = new Rectangle(
					(int)(position.X - (width * 0.5f)),
					(int)position.Y,
					(int)width,
					(int)GetHeight(screen));
			}
			else
			{
				//use the w/h stored in this dude
				ButtonRect = new Rectangle(
					(int)(position.X - (Width * 0.5f)),
					(int)position.Y,
					(int)Width,
					(int)GetHeight(screen)); //manually setting the height is a bitch
			}

			//darw the background rectangle if in touch mode
			if (screen.ScreenManager.TouchMenus && screen.IsActive)
			{
				//check if the mouse is over this entry
				float buttonAlpha = screen.TransitionAlpha * 0.5f;
				if (screen.ScreenManager.TouchMenus)
				{
					//get the mouse location
					if (ButtonHighlight(screen))
					{
						buttonAlpha = screen.TransitionAlpha * 0.2f;
					}
				}

				//draw the rect!
				screen.ScreenManager.DrawButtonBackground(buttonAlpha, ButtonRect);
			}

			//get the correct font buddy to draw with.
			ShadowTextBuddy menuFont;
			if (!screen.ScreenManager.TouchMenus)
			{
				//if its selected, do the pulsate text, otherwise use a nice shadow text
				menuFont = (isSelected ? PulsateText : ShadowText);
				menuFont.ShadowOffset = Vector2.Zero;
				menuFont.ShadowSize = 1.0f;
				menuFont.ShadowColor = backgroundColor;
			}
			else
			{
				//if we are doing touch menus, pop out unselected items from the shadow
				menuFont = ShadowText;
				menuFont.ShadowOffset = new Vector2(7.0f, 7.0f);
				menuFont.ShadowSize = 1.0f;
				menuFont.ShadowColor = backgroundColor;

				//set the colors
				if (isSelected)
				{
					menuFont.ShadowColor = NonSelectedColor;
					color = new Color(0, 0, 0, 0);
				}
			}

			menuFont.Font = MenuEntryFont(screen.ScreenManager);

			// Draw text!
			menuFont.Write(Text,
			               position,
			               Justify.Center,
			               SizeMultiplier,
			               color,
			               screen.ScreenManager.SpriteBatch,
			               gameTime.CurrentTime);
		}

		/// <summary>
		/// Queries how much space this menu entry requires.
		/// </summary>
		public float GetHeight(MenuScreen screen)
		{
			return (MenuEntryFont(screen.ScreenManager).LineSpacing * SizeMultiplier);
		}

		/// <summary>
		/// Get the width of this menu entry
		/// </summary>
		/// <returns>The width.</returns>
		/// <param name="screen">Screen.</param>
		public float GetWidth(MenuScreen screen)
		{
			return (MenuEntryFont(screen.ScreenManager).MeasureString(Text).X * SizeMultiplier);
		}

		public bool ButtonHighlight(GameScreen screen)
		{
			//Check if the mouse cursor is inside this menu entry
			if (ButtonRect.Contains(screen.ScreenManager.MousePos))
			{
				return true;
			}

			//check if the user is holding the thouch screen inside this menu entry
			if (null != screen.ScreenManager.Touch)
			{
				foreach (Vector2 touch in screen.ScreenManager.Touch.Touches)
				{
					if (ButtonRect.Contains(touch))
					{
						return true;
					}
				}
			}

			//nothing held in this dude
			return false;
		}

		/// <summary>
		/// Get the correct font, if this is message box or regular menu entry
		/// </summary>
		/// <param name="screenManager"></param>
		/// <returns></returns>
		protected SpriteFont MenuEntryFont(ScreenManager screenManager)
		{
			return (MessageBoxEntry ? screenManager.MessageBoxFont : screenManager.MenuFont);
		}

		#endregion
	}
}