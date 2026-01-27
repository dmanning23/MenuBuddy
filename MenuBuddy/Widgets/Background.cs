using InputHelper;
using Microsoft.Xna.Framework.Graphics;
using System.Threading.Tasks;

namespace MenuBuddy
{
	/// <summary>
	/// Handles drawing background rectangles and outlines for widgets that support <see cref="IBackgroundable"/>.
	/// </summary>
	public class Background : IHasContent
	{
		#region Properties

		/// <summary>
		/// The texture used to fill the background area of a widget.
		/// </summary>
		public Texture2D BackgroundImage { get; set; }

		#endregion //Properties

		#region Methods

		/// <summary>
		/// Loads the background image texture from the style sheet.
		/// </summary>
		/// <param name="screen">The screen whose content manager is used to load the texture.</param>
		/// <returns>A completed task.</returns>
		public Task LoadContent(IScreen screen)
		{
			if (null != screen.ScreenManager)
			{
				BackgroundImage = screen.Content.Load<Texture2D>(StyleSheet.ButtonBackgroundImageResource);
			}

			return Task.CompletedTask;
		}

		/// <summary>
		/// Unloads background content. Override to release resources.
		/// </summary>
		public virtual void UnloadContent()
		{
		}

		/// <summary>
		/// Draws a filled background rectangle for the specified item, using highlight or neutral colors.
		/// </summary>
		/// <param name="item">The backgroundable item whose rectangle is drawn.</param>
		/// <param name="screen">The screen used for drawing and transition context.</param>
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

		/// <summary>
		/// Draws an outline rectangle around the specified item, using highlight or neutral colors.
		/// </summary>
		/// <param name="item">The backgroundable item whose outline is drawn.</param>
		/// <param name="screen">The screen used for drawing and transition context.</param>
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

		/// <summary>
		/// Draws both the background fill and outline for the specified item, if enabled.
		/// </summary>
		/// <param name="item">The backgroundable item to draw.</param>
		/// <param name="screen">The screen used for drawing and transition context.</param>
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
