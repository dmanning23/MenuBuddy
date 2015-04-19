using GameTimer;
using System.Diagnostics;
using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// This is a button thhat contains a relaitve layout
	/// </summary>
	public class RelativeLayoutButton : BaseButton
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
				base.Rect = value;

				var layout = Layout as RelativeLayout;
				if (null != layout)
				{
					layout.Rectangle = Rect;
				}
			}
		}

		#endregion //Properties
		#region Initialization

		/// <summary>
		/// Constructs a new menu entry with the specified text.
		/// </summary>
		public RelativeLayoutButton(StyleSheet style)
			: base(style)
		{
		}

		public override void LoadContent(IScreen screen)
		{
			base.LoadContent(screen);
			SetLayout();
		}

		protected override void SetLayout()
		{
			RelativeLayout layout = new RelativeLayout();
			layout.Position = Position;
			layout.Rectangle = Rect;
			Layout = layout;
		}

		#endregion

		#region Update and Draw

		public override void Update(IScreen screen, GameClock gameTime)
		{
			Debug.Assert(null != Layout);
			Layout.Update(screen, gameTime);
		}

		public override void Draw(IScreen screen, GameClock gameTime)
		{
			Debug.Assert(null != Layout);
			Layout.Draw(screen, gameTime);
		}

		#endregion

		
	}
}