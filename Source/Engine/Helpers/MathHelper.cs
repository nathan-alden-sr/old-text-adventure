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

		public float Lerp(float value1, float value2, float scale)
		{
			return value1 + ((value2 - value1) * scale);
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