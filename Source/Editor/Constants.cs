using Microsoft.Xna.Framework;

namespace TextAdventure.Editor
{
	public static class Constants
	{
		public static class HatchRenderer
		{
			public static readonly Color Color;

			static HatchRenderer()
			{
				Color = Color.White * 0.25f;
			}
		}
	}
}