using Microsoft.Xna.Framework;

namespace TextAdventure.Xna.Extensions
{
	public static class ColorExtensions
	{
		public static Color ToXnaColor(this Engine.Common.Color color)
		{
			return new Color(color.R, color.G, color.B, color.A);
		}

		public static Color WithAlpha(this Color color, float a)
		{
			return new Color(color.R, color.G, color.B, a);
		}
	}
}