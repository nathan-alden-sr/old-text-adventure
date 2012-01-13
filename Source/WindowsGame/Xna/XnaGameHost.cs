using System;
using System.Windows.Forms;

using Junior.Common;

namespace TextAdventure.WindowsGame.Xna
{
	public class XnaGameHost : IDisposable
	{
		private readonly XnaGame _game;
		private readonly Func<bool> _shouldExitDelegate;

		public XnaGameHost(XnaGame game, Func<bool> shouldExitDelegate)
		{
			game.ThrowIfNull("game");
			shouldExitDelegate.ThrowIfNull("shouldExitDelegate");

			_game = game;
			_shouldExitDelegate = shouldExitDelegate;
		}

		public void Dispose()
		{
			OnDispose(true);
			GC.SuppressFinalize(this);
		}

		~XnaGameHost()
		{
			OnDispose(false);
		}

		public void Run()
		{
			_game.Run();

			while (!_shouldExitDelegate())
			{
				_game.Tick();

				Application.DoEvents();
			}
		}

		protected void OnDispose(bool disposing)
		{
			if (disposing)
			{
				_game.Dispose();
			}
		}
	}
}