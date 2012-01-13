using System.Windows.Forms;

using TextAdventure.WindowsGame.Forms;
using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame
{
#if WINDOWS
	internal static class Program
	{
		private static void Main(string[] args)
		{
			Engine.Objects.World world = GetWorld();
			var gameForm = new GameForm(world, world.StartingPlayer);

			gameForm.Show();

			Application.DoEvents();

			using (var gameHost = new XnaGameHost(gameForm.Game, () => gameForm.CloseRequested))
			{
				gameHost.Run();
			}
		}

		private static Engine.Objects.World GetWorld()
		{
			return WorldLoader.FromAssembly("TextAdventure.SampleWorld.dll", "TextAdventure.SampleWorld.SampleWorld");
		}
	}
#endif
}