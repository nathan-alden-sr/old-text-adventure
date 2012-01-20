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

		public int QuantizationRound(int value, int multiple)
		{
			return (int)(Math.Round(value / (double)multiple, 0) * multiple);
		}

		public int QuantizationCeiling(int value, int multiple)
		{
			return (int)(Math.Ceiling(value / (double)multiple) * multiple);
		}

		public int QuantizationFloor(int value, int multiple)
		{
			return (int)(Math.Floor(value / (double)multiple) * multiple);
		}
	}
}