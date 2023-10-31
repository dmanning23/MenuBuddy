using InputHelper;
using MenuBuddy;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using PrimitiveBuddy;
using ResolutionBuddy;
using System;

namespace MenuBuddySample
{
	/// <summary>
	/// This is the main type for your game
	/// </summary>
	//public class Game1 : ControllerGame

#if __IOS__ || ANDROID || WINDOWS_UAP
	public class Game1 : TouchGame
#else
	public class Game1 : ControllerGame
#endif
	{
		#region Properties

		Primitive titlesafe;

		#endregion //Properties

		#region Methods

		public Game1()
		{
			//FullScreen = true;

			this.Graphics.GraphicsProfile = GraphicsProfile.Reach;

#if DESKTOP
			IsMouseVisible = true;
#endif
			VirtualResolution = new Point(1080, 1920);
			ScreenResolution = new Point(720, 1280);

			var debug = new DebugInputComponent(this, Resolution.TransformationMatrix);

			Fullscreen = false;
			UseDeviceResolution = true;
			Letterbox = false;
		}

		protected override void LoadContent()
		{
			base.LoadContent();

			titlesafe = new Primitive(Graphics.GraphicsDevice, ScreenManager.SpriteBatch);
		}

		protected override void Initialize()
		{
			base.Initialize();
		}

		protected override void InitStyles()
		{
			base.InitStyles();
			//DefaultStyles.Instance().MainStyle.HasOutline = true;
			//DefaultStyles.Instance().MenuEntryStyle.HasOutline = true;
			//DefaultStyles.Instance().MenuTitleStyle.HasOutline = true;
			//DefaultStyles.Instance().MessageBoxStyle.HasOutline = true;
		}

		/// <summary>
		/// Get the set of screens needed for the main menu
		/// </summary>
		/// <returns>The gameplay screen stack.</returns>
		public override IScreen[] GetMainMenuScreenStack()
		{
			return new IScreen[] { new BackgroundScreen(), new MainMenuScreen() };
		}

		protected override void Update(GameTime gameTime)
		{
			try
			{
				base.Update(gameTime);
			}
			catch (Exception ex)
			{
				ScreenManager.ErrorScreen(ex);
			}
		}

		protected override void Draw(GameTime gameTime)
		{
			try
			{
				base.Draw(gameTime);

				ScreenManager.SpriteBatch.Begin(SpriteSortMode.Immediate,
							  BlendState.AlphaBlend,
							  null, null, null, null,
							  Resolution.TransformationMatrix());

				titlesafe.Thickness = 3.0f;
				titlesafe.Rectangle(Resolution.TitleSafeArea, Color.Red);

				titlesafe.Thickness = 4.0f;
				titlesafe.Rectangle(Resolution.ScreenArea, Color.Blue);

				ScreenManager.SpriteBatch.End();
			}
			catch (Exception ex)
			{
				ScreenManager.ErrorScreen(ex);
			}
		}

		#endregion //Methods
	}
}

