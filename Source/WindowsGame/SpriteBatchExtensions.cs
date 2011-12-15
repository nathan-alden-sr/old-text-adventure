using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public static class SpriteBatchExtensions
	{
		public static void DrawStringWithShadow(this SpriteBatch spriteBatch, SpriteFont spriteFont, string text, Vector2 position, Color textColor, Color shadowColor, Vector2 shadowOffset)
		{
			spriteBatch.ThrowIfNull("spriteBatch");

			spriteBatch.DrawString(spriteFont, text, new Vector2(position.X + shadowOffset.X, position.Y + shadowOffset.Y), shadowColor);
			spriteBatch.DrawString(spriteFont, text, position, textColor);
		}
	}
}