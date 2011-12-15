using Junior.Common;

using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class TextAdventureGameComponent : GameComponent
	{
		private readonly GameManager _gameManager;

		protected TextAdventureGameComponent(GameManager gameManager)
			: base(gameManager.Game)
		{
			gameManager.ThrowIfNull("gameManager");

			_gameManager = gameManager;
		}

		protected GameManager GameManager
		{
			get
			{
				return _gameManager;
			}
		}

		protected InputFocusManager InputFocusManager
		{
			get
			{
				return _gameManager.InputFocusManager;
			}
		}

		protected bool Focused
		{
			get
			{
				return _gameManager.InputFocusManager.FocusedComponent == this;
			}
		}

		public override void Initialize()
		{
			AddGameComponents();

			base.Initialize();
		}

		protected virtual void AddGameComponents()
		{
		}
	}
}