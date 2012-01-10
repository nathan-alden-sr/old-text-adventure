using System;
using System.Linq;

namespace TextAdventure.WindowsGame
{
#if WINDOWS
	internal static class Program
	{
		private static void Main(string[] args)
		{
			Engine.Objects.World world;

			if (!args.Any())
			{
				world = WorldLoader.FromAssembly("TextAdventure.SampleWorld.dll", "TextAdventure.SampleWorld.SampleWorld");
			}
			else
			{
				throw new NotImplementedException();
			}

			using (var game = new TextAdventureGame(world, world.StartingPlayer))
			{
				game.Run();
			}
		}
	}
#endif
}