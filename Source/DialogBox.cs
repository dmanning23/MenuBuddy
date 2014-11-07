using Microsoft.Xna.Framework;
using ResolutionBuddy;

namespace MenuBuddy
{
	/// <summary>
	/// This is a special menu screen that doesnt take up the whole screen
	/// It doesn't have a title either, just a list of options to choose from.
	/// </summary>
	public abstract class DialogBox : MenuScreen
	{
		#region Fields

		/// <summary>
		/// The window area of this dialog box.
		/// </summary>
		private Rectangle _window = Resolution.TitleSafeArea;

		#endregion

		#region Properties

		public Rectangle Window
		{
			get
			{
				return _window;
			}
			set
			{
				_window = value;
			}
		}

		#endregion

		#region Update and Draw

		/// <summary>
		/// Get the x position to draw menu entries at, using the transition position
		/// </summary>
		/// <returns>the correct position the place menu entries on X axis</returns>
		public override float MenuEntryPositionX()
		{
			return XPositionWithOffset(Window.Center.X);
		}

		/// <summary>
		/// Get the x position to draw menu entries at, using the transition position
		/// </summary>
		/// <returns>the correct position the place menu entries on Y axis</returns>
		public override float MenuEntryPositionY()
		{
			return Window.Top + ScreenManager.MessageBoxFont.LineSpacing;
		}

		#endregion
	}
}