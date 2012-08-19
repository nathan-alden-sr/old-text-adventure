using Microsoft.Xna.Framework;

namespace TextAdventure.Xna.Helpers
{
	public class CharacterTextureHelper
	{
		public static readonly CharacterTextureHelper Instance = new CharacterTextureHelper();

		private CharacterTextureHelper()
		{
		}

		public static Rectangle GetSymbolSourceRectangle(byte symbol)
		{
			return new Rectangle((symbol % 16) * Constants.Tile.TileWidth, (symbol / 16) * Constants.Tile.TileHeight, Constants.Tile.TileWidth, Constants.Tile.TileHeight);
		}
	}
}