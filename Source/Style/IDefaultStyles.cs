using Microsoft.Xna.Framework;

namespace MenuBuddy
{
	/// <summary>
	/// Static storage of SpriteFont objects and colors for use throughout the game.
	/// </summary>
	public interface IDefaultStyles : IGameComponent
	{
		//Fonts to be used in this game
		string MenuTitleFontName { set; }
		string MenuEntryFontName { set; }
		string MessageBoxFontName { set; }

		//some sound effects to be used in this game
		string MenuSelectSoundName { set; }
		string MenuChangeSoundName { set; }

		//Images to be used in this game
		string MessageBoxBackground { set; }
		string ButtonBackground { set; }

		StyleSheet MainStyle { get; }
		StyleSheet MenuTitleStyle { get; }
		StyleSheet MenuEntryStyle { get; }
		StyleSheet MessageBoxStyle { get; }
	}
}
