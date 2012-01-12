using System;

namespace TextAdventure.Engine.Helpers
{
	public static class MathHelper
	{
		public static float Clamp(float value, float minimum, float maximum)
		{
			return Math.Max(minimum, Math.Min(value, maximum));
		}
	}
}