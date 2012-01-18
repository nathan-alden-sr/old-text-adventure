using System;
using System.Runtime.InteropServices;
using System.Security;
using System.Windows.Forms;

using TextAdventure.WindowsGame.Forms;

namespace TextAdventure.WindowsGame
{
#if WINDOWS
	internal static class Program
	{
		[return:MarshalAs(UnmanagedType.Bool)]
		[SuppressUnmanagedCodeSecurity]
		[DllImport("user32.dll", CharSet = CharSet.Auto)]
		private static extern bool PeekMessage(out Message msg, IntPtr hWnd, uint messageFilterMin, uint messageFilterMax, uint flags);

		[STAThread]
		private static void Main()
		{
			Engine.Objects.World world = GetWorld();

			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);

			var _gameForm = new GameForm(world, world.StartingPlayer);

			Application.Idle += (sender, args) => ApplicationOnIdle(_gameForm);
			Application.Run(_gameForm);
		}

		private static void ApplicationOnIdle(GameForm gameForm)
		{
			Message message;

			while (!PeekMessage(out message, IntPtr.Zero, 0, 0, 0))
			{
				gameForm.Render();
			}
		}

		private static Engine.Objects.World GetWorld()
		{
			return WorldLoader.Instance.FromAssembly("TextAdventure.Samples.dll", "TextAdventure.Samples.Introduction.IntroductionWorld");
		}
	}
#endif
}