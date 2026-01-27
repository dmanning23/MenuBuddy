using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace MenuBuddy
{
	/// <summary>
	/// An image widget that oscillates its draw position in a specified direction, creating a bouncing animation.
	/// </summary>
	public class BouncyImage : Image
    {
		#region Properties

		/// <summary>
		/// The direction and magnitude of the bounce offset.
		/// </summary>
		public Vector2 BounceDirection { get; set; }

		/// <summary>
		/// The speed of the bounce oscillation. Higher values produce faster bouncing.
		/// </summary>
		public float BounceSpeed { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Initializes a new <see cref="BouncyImage"/> with no texture and default bounce settings.
		/// </summary>
		public BouncyImage() : base()
		{
			BounceDirection = Vector2.Zero;
			BounceSpeed = 4f;
		}

		/// <summary>
		/// Initializes a new <see cref="BouncyImage"/> with the specified texture and default bounce settings.
		/// </summary>
		/// <param name="texture">The texture to display.</param>
		public BouncyImage(Texture2D texture) : base(texture)
		{
			BounceDirection = Vector2.Zero;
			BounceSpeed = 4f;
		}

		/// <summary>
		/// Computes the draw position with an oscillating bounce offset applied.
		/// </summary>
		/// <param name="screen">The screen used to compute the transition offset.</param>
		/// <returns>The adjusted position including the bounce offset.</returns>
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
