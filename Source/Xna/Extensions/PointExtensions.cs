using Microsoft.Xna.Framework;

namespace TextAdventure.Xna.Extensions
{
	public static class PointExtensions
	{
		public static Vector2 ToVector2(this Point point)
		{
			return new Vector2(point.X, point.Y);
		}
	}
}