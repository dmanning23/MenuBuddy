using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

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
		/// The text rendered for this entry.
		/// </summary>
		protected string m_strText;

		/// <summary>
		/// Tracks a fading selection effect on the entry.
		/// </summary>
		/// <remarks>
		/// The entries transition out of the selection effect when they are deselected.
		/// </remarks>
		protected float m_fSelectionFade;

		/// <summary>
		/// how much to resize the menu entry.
		/// </summary>
		protected float m_fSizeMultiplier;

		#endregion

		#region Properties

		/// <summary>
		/// Gets or sets the text of this menu entry.
		/// </summary>
		public string Text
		{
			get { return m_strText; }
			set { m_strText = value; }
		}

		/// <summary>
		/// get or set the size multiplier of this menu entry
		/// </summary>
		public float SizeMultiplier
		{
			get { return m_fSizeMultiplier; }
			set { m_fSizeMultiplier = value; }
		}

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
			m_strText = strText;
			m_fSizeMultiplier = 1.0f;
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
		public virtual void Draw(MenuScreen screen, Vector2 position, bool isSelected, GameTime gameTime)
		{
			// Draw text, centered on the middle of each line.
			ScreenManager screenManager = screen.ScreenManager;
			SpriteBatch spriteBatch = screenManager.SpriteBatch;
			SpriteFont font = screenManager.Font;

			// Pulsate the size of the selected menu entry.
			double time = gameTime.TotalGameTime.TotalSeconds;
			float pulsate = (float)(Math.Sin(time * 4.0) + 1.0);
			float scale = 1 + pulsate * 0.15f * m_fSelectionFade;

			// Draw the selected entry in yellow, otherwise white.
			Color color = isSelected ? Color.Red : Color.White;

			// Modify the alpha to fade text out during transitions.
			Vector3 myColor = color.ToVector3();
			color = new Color(myColor.X, myColor.Y, myColor.Z, screen.TransitionAlpha);
			myColor = Color.Black.ToVector3();
			Color backgroundColor = new Color(myColor.X, myColor.Y, myColor.Z, screen.TransitionAlpha);

			Vector2 origin = new Vector2(0, font.LineSpacing / 2);

			//First draw the menu item in black, which will add a nice shadow effect to selected items
			spriteBatch.DrawString(font, m_strText, position, backgroundColor, 0, origin, SizeMultiplier, SpriteEffects.None, 0);

			//adjust the position to account for the pulsating
			float fAdjust = ((font.MeasureString(Text).X * SizeMultiplier * scale) - (font.MeasureString(Text).X * SizeMultiplier)) / 2.0f;
			position.X -= fAdjust;

			//Draw the menu item
			spriteBatch.DrawString(font, m_strText, position, color, 0, origin, scale * SizeMultiplier, SpriteEffects.None, 0);
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