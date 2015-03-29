using FontBuddyLib;
using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using Microsoft.Xna.Framework.Audio;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button that can be clicked on
	/// </summary>
	public class Button : Widget, IButton
	{
		#region Properties

		public bool IsHighlighted { protected get; set; }

		public override Rectangle Rect
		{
			get 
			{
				var rect = base.Rect;
				rect.Width = 768;
				rect.Height = 128;
				return rect;
			}
		}

		/// <summary>
		/// A description of the function of the menu entry.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// The text rendered for this entry.
		/// </summary>
		public string Text { get; set; }

		/// <summary>
		/// Event raised when the menu entry is selected.
		/// </summary>
		public event EventHandler<PlayerIndexEventArgs> Selected;

		/// <summary>
		/// Method for raising the Selected event.
		/// </summary>
		public virtual void OnSelect(PlayerIndex? playerIndex)
		{
			if (Selected != null)
			{
				Selected(this, new PlayerIndexEventArgs(playerIndex));
			}

			//play the sound effect
			if (!Style.IsQuiet && (null != Style.SelectedSoundEffect))
			{
				Style.SelectedSoundEffect.Play();
			}
		}

		/// <summary>
		/// This item is currently highlighted
		/// </summary>
		public void OnHighlighted()
		{
			IsHighlighted = true;
		}

		/// <summary>
		/// This item is currently NOT highlighted
		/// </summary>
		public void OnNotHighlighted()
		{
			IsHighlighted = false;
		}

		#endregion //Properties

		#region Initialization

		/// <summary>
		/// Constructs a new button with the specified text.
		/// </summary>
		public Button(StyleSheet style, string text)
			: base(style)
		{
			Text = text;
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Updates the menu entry.
		/// </summary>
		public override void Update(IScreen screen, GameClock gameTime)
		{
			//TODO: button pressed/held/released
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			//Get the font to use
			var font = IsHighlighted ? Style.SelectedFont : Style.UnselectedFont;

			//get the color to use
			var color = IsHighlighted ? Style.SelectedTextColor : Style.UnselectedTextColor;

			//get teh position
			var pos = new Vector2(Rect.Center.X, Rect.Top);

			//Write the text
			font.Write(Text,
				pos,
				Justify.Center,
				1.0f,
				color,
				screen.ScreenManager.SpriteBatch,
				gameTime);
		}

		public override string ToString()
		{
			return Text;
		}

		#endregion
	}
}