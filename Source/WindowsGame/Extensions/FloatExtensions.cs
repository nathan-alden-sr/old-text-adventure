namespace TextAdventure.WindowsGame.Extensions
{
	public static class FloatExtensions
	{
		public static int Round(this float value)
		{
			return (int)(value > 0 ? value + 0.5f : value - 0.5f);
		}
	}
}