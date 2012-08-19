using Microsoft.Xna.Framework;

namespace TextAdventure.Xna.Extensions
{
	public static class Vector2Extensions
	{
		public static Vector2 Round(this Vector2 vector)
		{
			return new Vector2(vector.X.Round(), vector.Y.Round());
		}
	}
}