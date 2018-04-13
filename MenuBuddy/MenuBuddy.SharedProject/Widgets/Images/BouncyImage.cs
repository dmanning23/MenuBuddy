using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	public class BouncyImage : Image
    {
		#region Properties

		public Vector2 BounceDirection { get; set; }

		public float BounceSpeed { get; set; }

		#endregion //Properties

		#region Methods

		public BouncyImage() : base()
		{
			BounceDirection = Vector2.Zero;
			BounceSpeed = 4f;
		}

		/// <summary>
		/// constructor!
		/// </summary>
		public BouncyImage(Texture2D texture) : base(texture)
		{
			BounceDirection = Vector2.Zero;
			BounceSpeed = 4f;
		}

		protected override Point DrawPosition(IScreen screen)
		{
			var position = base.DrawPosition(screen).ToVector2();

			//multiply the time by the speed
			var currentTime = HighlightClock.CurrentTime * BounceSpeed;

			//Pulsate the size of the text
			var pulsate = 0.5f * (float)(Math.Sin(currentTime) + 1.0f);

			position += BounceDirection * pulsate;

			return position.ToPoint();
		}

		#endregion //Methods
	}
}
