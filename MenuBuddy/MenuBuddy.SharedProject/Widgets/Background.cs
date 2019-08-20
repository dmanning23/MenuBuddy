﻿using InputHelper;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace MenuBuddy
{
	public class Background : IHasContent
	{
		#region Properties

		public Texture2D BackgroundImage { get; set; }

		#endregion //Properties

		#region Methods

		public Task LoadContent(IScreen screen)
		{
			if (null != screen.ScreenManager)
			{
				BackgroundImage = screen.Content.Load<Texture2D>(StyleSheet.ButtonBackgroundImageResource);
			}

			return Task.CompletedTask;
		}

		public virtual void UnloadContent()
		{
		}

		protected void DrawBackground(IBackgroundable item, IScreen screen)
		{
			var color = StyleSheet.NeutralBackgroundColor;

			var highlightable = item as IHighlightable;
			if (null != highlightable && highlightable.IsHighlighted)
			{
				color = StyleSheet.HighlightedBackgroundColor;
			}

			var transitionable = item as ITransitionable;
			if (null != transitionable)
			{
				screen.ScreenManager.DrawHelper.DrawRect(screen, color, item.Rect, transitionable.TransitionObject, BackgroundImage);
			}
			else
			{
				screen.ScreenManager.DrawHelper.DrawRect(color, item.Rect, BackgroundImage);
			}
		}

		protected void DrawOutline(IBackgroundable item, IScreen screen)
		{
			var color = StyleSheet.NeutralOutlineColor;

			var highlightable = item as IHighlightable;
			if (null != highlightable && highlightable.IsHighlighted)
			{
				color = StyleSheet.HighlightedOutlineColor;
			}

			var transitionable = item as ITransitionable;
			if (null != transitionable)
			{
				screen.ScreenManager.DrawHelper.DrawOutline(screen, color, item.Rect, transitionable.TransitionObject);
			}
			else
			{
				screen.ScreenManager.DrawHelper.DrawOutline(color, item.Rect);
			}
		}

		public void Draw(IBackgroundable item, IScreen screen)
		{
			//draw the rect!
			if (item.HasBackground)
			{
				DrawBackground(item, screen);
			}

			//draw the outline!
			if (item.HasOutline)
			{
				DrawOutline(item, screen);
			}
		}

		#endregion //Methods
	}
}
