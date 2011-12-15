using System;

namespace TextAdventure.WindowsGame.Components
{
	// Do not reorder type members with ReSharper
	public static class ComponentUpdateOrder
	{
		public const int Fps = Int32.MinValue;
		public const int WorldInstance = Int32.MaxValue;
		public const int WorldTime = Int32.MinValue;
	}
}