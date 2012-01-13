using System;
using System.Windows.Forms;

using TextAdventure.WindowsGame.Forms;
using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame
{
#if WINDOWS
	internal static class Program
	{
		[STAThread]
		private static void Main(string[] args)
		{
			Engine.Objects.World world = GetWorld();
			var gameForm = new GameForm(world, world.StartingPlayer);

			gameForm.Show();

			Application.DoEvents();

			do
			{
				gameForm.GameChanged = false;

				using (var gameHost = new XnaGameHost(gameForm.Game, () => gameForm.CloseRequested || gameForm.GameChanged))
				{
					gameHost.Run();
				}
			} while (gameForm.GameChanged);
		}

		private static Engine.Objects.World GetWorld()
		{
			return WorldLoader.Instance.FromAssembly("TextAdventure.Samples.dll", "TextAdventure.Samples.Introduction.IntroductionWorld");
		}
	}
#endif
}