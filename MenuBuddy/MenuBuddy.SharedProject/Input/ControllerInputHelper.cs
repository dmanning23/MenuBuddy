using InputHelper;
using Microsoft.Xna.Framework;
using System.Collections.Generic;

namespace MenuBuddy
{
	public class ControllerInputHelper : IInputHelper
	{
		public List<ClickEventArgs> Clicks { get; set; }

		public List<HighlightEventArgs> Highlights { get; set; }

		public List<DragEventArgs> Drags { get; set; }

		public List<DropEventArgs> Drops { get; set; }

		public List<FlickEventArgs> Flicks { get; set; }

		public List<PinchEventArgs> Pinches { get; set; }

		public List<HoldEventArgs> Holds { get; set; }

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
