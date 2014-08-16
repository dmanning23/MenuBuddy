using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
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
		/// Only used if TextSelectionRect.CascadeMenuEntries is set to false.
		/// </summary>
		public float Width { get; set; }

		/// <summary>
		/// get or set the Height of the selection box of this menu entry.
		/// Only used if TextSelectionRect.CascadeMenuEntries is set to false.
		/// </summary>
		public float Height { get; set; }

		protected float m_fSelectionFade;

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
		public MenuEntry(string strText)
		{
			Text = strText;
			SizeMultiplier = 1.0f;

			NonSelectedColor = Color.White;

			ShadowText = new ShadowTextBuddy();
			PulsateText = new PulsateBuddy();
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
		public void Draw(MenuScreen screen, Vector2 position, bool isSelected, GameClock gameTime)
		{
			// Draw the selected entry in yellow, otherwise white.
			Color color = isSelected ? Color.Red : NonSelectedColor;

			// Modify the alpha to fade text out during transitions.
			float alpha = (screen.TransitionAlpha * 255.0f);
			color.A = Convert.ToByte(alpha);
			Color backgroundColor = Color.Black;
			backgroundColor.A = Convert.ToByte(alpha);

			//darw the background rectangle if in touch mode
			if (screen.ScreenManager.TouchMenus && screen.IsActive)
			{
				//create the rect we need
				Rectangle rect;
				if (screen.TextSelectionRect)
				{
					//use the rect of the text
					Vector2 textSize = screen.ScreenManager.MenuFont.MeasureString(Text);
					textSize.X *= 1.1f;
					rect = new Rectangle(
						(int)(position.X - (textSize.X * 0.5f)), 
						(int)position.Y, 
						(int)textSize.X,
						(int)GetHeight(screen));
				}
				else
				{
					//use the w/h stored in this dude
					rect = new Rectangle(
						(int)(position.X - (Width * 0.5f)),
						(int)position.Y,
						(int)Width,
						(int)Height);
				}

				//draw the rect!
				screen.ScreenManager.DrawButtonBackground(screen.TransitionAlpha * 0.5f, rect);
			}

			//get the correct font buddy to draw with.
			ShadowTextBuddy menuFont;
			if (!screen.ScreenManager.TouchMenus)
			{
				//if its selected, do the pulsate text, otherwise use a nice shadow text
				menuFont = (isSelected ? PulsateText : ShadowText);
				menuFont.ShadowOffset = Vector2.Zero;
				menuFont.ShadowSize = 1.0f;
			}
			else
			{
				//if we are doing touch menus, pop out unselected items from the shadow
				menuFont = ShadowText;
				menuFont.ShadowOffset = (isSelected ? Vector2.Zero : new Vector2(5.0f, 5.0f));
				menuFont.ShadowSize = 1.0f;
			}
			
			menuFont.ShadowColor = backgroundColor;
			menuFont.Font = screen.ScreenManager.MenuFont;

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
			return (screen.ScreenManager.MenuFont.LineSpacing * SizeMultiplier);
		}

		/// <summary>
		/// Get the width of this menu entry
		/// </summary>
		/// <returns>The width.</returns>
		/// <param name="screen">Screen.</param>
		public float GetWidth(MenuScreen screen)
		{
			return (screen.ScreenManager.MenuFont.MeasureString(Text).X * SizeMultiplier);
		}

		#endregion
	}
}