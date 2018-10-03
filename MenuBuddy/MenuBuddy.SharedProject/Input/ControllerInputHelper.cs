using InputHelper;
using System.Collections.Generic;

namespace MenuBuddy
{
	public class ControllerInputHelper : IInputHelper
	{
		public List<ClickEventArgs> Clicks { get; set; }

		public List<HighlightEventArgs> Highlights { get; set; }

		public List<DragEventArgs> Drags { get; set; }

		public List<DropEventArgs> Drops { get; set; }

		public ControllerInputHelper()
		{
			Clicks = new List<ClickEventArgs>();
			Highlights = new List<HighlightEventArgs>();
			Drags = new List<DragEventArgs>();
			Drops = new List<DropEventArgs>();
		}
	}
}
