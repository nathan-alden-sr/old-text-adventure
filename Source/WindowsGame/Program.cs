using System;
using System.Configuration;
using System.Linq;
using System.Windows.Forms;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Configuration;
using TextAdventure.WindowsGame.Forms;

namespace TextAdventure.WindowsGame
{
#if WINDOWS
	internal static class Program
	{
		private static readonly FpsConfigurationSection _fpsConfigurationSection = (FpsConfigurationSection)ConfigurationManager.GetSection("fps");
		private static readonly LogConfigurationSection _logConfigurationSection = (LogConfigurationSection)ConfigurationManager.GetSection("log");
		private static readonly WorldTimeConfigurationSection _worldTimeConfigurationSection = (WorldTimeConfigurationSection)ConfigurationManager.GetSection("worldTime");

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

			var gameForm = new GameForm();
			bool exiting = false;

			gameForm.FormClosing += (sender, eventArgs) => exiting = true;
			gameForm.Show();

			Application.DoEvents();

			using (var game = new TextAdventureGame(gameForm.GraphicsDevice, world, world.StartingPlayer, _fpsConfigurationSection, _logConfigurationSection, _worldTimeConfigurationSection))
			{
				game.Start();

				while (!exiting)
				{
					game.GraphicsDevice.Clear(Color.Black);
					game.Tick();
					game.GraphicsDevice.Present();

					Application.DoEvents();
				}
			}
		}
	}
#endif
}