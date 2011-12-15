using System;

namespace TextAdventure.WindowsGame.Components
{
	// Do not reorder type members with ReSharper
	public static class ComponentDrawOrder
	{
		public const int Background = 0;
		public const int Board = 10;
		public const int Fps = Int32.MaxValue;
		public const int Log = Int32.MaxValue;
		public const int Player = 20;
		public const int WorldTime = Int32.MaxValue;
	}
}