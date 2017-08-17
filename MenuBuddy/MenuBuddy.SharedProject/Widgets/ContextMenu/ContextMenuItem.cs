using Microsoft.Xna.Framework.Graphics;

namespace MenuBuddy
{
	public class ContextMenuItem
	{
		public Texture2D Icon { get; set; }
		public string IconText { get; set; }
		public ClickDelegate ClickEvent { get; set; }

		public ContextMenuItem(Texture2D icon, string iconText, ClickDelegate clickEvent)
		{
			Icon = icon;
			IconText = iconText;
			ClickEvent = clickEvent;
		}
	}
}
