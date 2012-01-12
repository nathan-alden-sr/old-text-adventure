using System;

namespace TextAdventure.Engine.Helpers
{
	public class MathHelper
	{
		public static readonly MathHelper Instance = new MathHelper();

		private MathHelper()
		{
		}

		public float Clamp(float value, float minimum, float maximum)
		{
			return Math.Max(minimum, Math.Min(value, maximum));
		}
	}
}