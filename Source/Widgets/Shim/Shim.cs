using GameTimer;
using Microsoft.Xna.Framework;

using Vector2Extensions;
namespace MenuBuddy
{
	public class Shim : Widget, IShim
	{
		#region Properties

		public override Rectangle Rect
		{
			get
			{
				return base.Rect;
			}

			set
			{
				//if the user implicitly sets the rect, the padding and scale are reset
				base.Padding = Vector2.Zero;
				base.Scale = 1f;
				base.Rect = value;
			}
		}

		public override Vector2 Padding
		{
			get
			{
				return base.Padding;
			}

			set
			{
				var oldPadding = (Padding * 2) * Scale;
				var size = new Vector2(Rect.Width - oldPadding.X, Rect.Height - oldPadding.Y);

				base.Padding = value;
				size += (Padding * 2) * Scale;
				base.Rect = new Rectangle(Rect.X, Rect.Y, (int)size.X, (int)size.Y);
			}
		}

		public override float Scale
		{
			get
			{
				return base.Scale;
			}

			set
			{
				var oldPadding = (Padding * 2) * Scale;
				var size = new Vector2(Rect.Width - oldPadding.X, Rect.Height - oldPadding.Y);

				base.Scale = value;
				size *= Scale;
				size += (Padding * 2) * Scale;
				base.Rect = new Rectangle(Rect.X, Rect.Y, (int)size.X, (int)size.Y);
			}
		}

		#endregion //Properties

		#region Methods

		/// <summary>
		/// constructor
		/// </summary>
		public Shim()
		{
		}

		public override void Update(IScreen screen, GameClock gameTime)
		{
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
		}

		#endregion //Methods
	}
}