using TextAdventure.Engine.Common;

namespace TextAdventure.Editor.Extensions
{
	public static class ColorExtensions
	{
		public static Color ToEngineColor(this System.Drawing.Color color)
		{
			return new Color(color.R, color.G, color.B, color.A);
		}
	}
}