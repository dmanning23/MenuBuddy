using GameTimer;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Vector2Extensions;

namespace MenuBuddy
{
	/// <summary>
	/// An image that is displayed onteh screen
	/// </summary>
	public class Image : Widget
	{
		#region Fields

		private Texture2D _texture;

		#endregion //Fields

		#region Properties

		public Texture2D Texture
		{
			get { return _texture; }
			set
			{
				_texture = value;
				Rect = new Rectangle(Rect.X, Rect.Y, _texture.Width, _texture.Height);
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor!
		/// </summary>
		/// <param name="style"></param>
		public Image(StyleSheet style)
			: base(style)
		{
		}

		/// <summary>
		/// constructor!
		/// </summary>
		public Image(StyleSheet style, Texture2D texture)
			: this(style)
		{
			Texture = texture;
		}

		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			//get the transition color
			var color = screen.Transition.AlphaColor(Color.White);

			//Get the transition location
			var pos = DrawPosition(screen);

			//draw the item with all the correct parameters
			screen.ScreenManager.SpriteBatch.Draw(_texture, new Rectangle((int)pos.X, (int)pos.Y, Rect.Width, Rect.Height), color);
		}

		#endregion //Methods
	}
}