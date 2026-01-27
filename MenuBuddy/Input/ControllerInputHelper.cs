using InputHelper;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MenuBuddy
{
	/// <summary>
	/// Input helper for controller-based games. Stores input events for processing by the screen manager.
	/// </summary>
	public class ControllerInputHelper : IInputHelper
	{
		/// <summary>
		/// Gets or sets the list of click events to process.
		/// </summary>
		public List<ClickEventArgs> Clicks { get; set; }

		/// <summary>
		/// Gets or sets the list of highlight events to process.
		/// </summary>
		public List<HighlightEventArgs> Highlights { get; set; }

		/// <summary>
		/// Gets or sets the list of drag events to process.
		/// </summary>
		public List<DragEventArgs> Drags { get; set; }

		/// <summary>
		/// Gets or sets the list of drop events to process.
		/// </summary>
		public List<DropEventArgs> Drops { get; set; }

		/// <summary>
		/// Gets or sets the list of flick gesture events to process.
		/// </summary>
		public List<FlickEventArgs> Flicks { get; set; }

		/// <summary>
		/// Gets or sets the list of pinch gesture events to process.
		/// </summary>
		public List<PinchEventArgs> Pinches { get; set; }

		/// <summary>
		/// Gets or sets the list of hold gesture events to process.
		/// </summary>
		public List<HoldEventArgs> Holds { get; set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ControllerInputHelper"/> class
		/// and registers it as the <see cref="IInputHelper"/> service.
		/// </summary>
		/// <param name="game">The game instance.</param>
		public ControllerInputHelper(Game game)
		{
			Clicks = new List<ClickEventArgs>();
			Highlights = new List<HighlightEventArgs>();
			Drags = new List<DragEventArgs>();
			Drops = new List<DropEventArgs>();
			Flicks = new List<FlickEventArgs>();
			Pinches = new List<PinchEventArgs>();
			Holds = new List<HoldEventArgs>();

			game.Services.AddService(typeof(IInputHelper), this);
		}
	}
}
