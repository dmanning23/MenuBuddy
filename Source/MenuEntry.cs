using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using FontBuddyLib;
using GameTimer;

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

		#endregion

		#region Events

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
				Selected(this, new PlayerIndexEventArgs(playerIndex));
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
			// Draw text, centered on the middle of each line.
			ScreenManager screenManager = screen.ScreenManager;
			SpriteBatch spriteBatch = screenManager.SpriteBatch;

			// Draw the selected entry in yellow, otherwise white.
			Color color = isSelected ? Color.Red : Color.White;

			// Modify the alpha to fade text out during transitions.
			color.A = Convert.ToByte(screen.TransitionAlpha * 255.0f);
			Color backgroundColor = Color.Black;
			backgroundColor.A = Convert.ToByte(screen.TransitionAlpha * 255.0f);

			//if its selected, do the pulsate text, otherwise use a nice shadow text
			ShadowTextBuddy menuFont = (isSelected ? new PulsateBuddy() : new ShadowTextBuddy());
			menuFont.ShadowColor = backgroundColor;
			menuFont.Font = screenManager.Font;
			menuFont.ShadowOffset = Vector2.Zero;
			menuFont.ShadowSize = 1.0f;
			menuFont.Write(Text, position, Justify.Center, SizeMultiplier, color, spriteBatch, gameTime.CurrentTime);
		}

		/// <summary>
		/// Queries how much space this menu entry requires.
		/// </summary>
		public virtual float GetHeight(MenuScreen screen)
		{
			return ((float)screen.ScreenManager.Font.LineSpacing * SizeMultiplier);
		}

		#endregion
	}
}