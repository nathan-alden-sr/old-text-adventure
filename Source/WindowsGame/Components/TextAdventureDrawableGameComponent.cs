using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class TextAdventureDrawableGameComponent : DrawableGameComponent
	{
		private readonly FontContent _fontContent;
		private readonly GameManager _gameManager;
		private readonly TextureContent _textureContent;

		protected TextAdventureDrawableGameComponent(GameManager gameManager)
			: base(gameManager.Game)
		{
			gameManager.ThrowIfNull("gameManager");

			_gameManager = gameManager;
			_textureContent = new TextureContent(gameManager.Game.Content);
			_fontContent = new FontContent(gameManager.Game.Content);
		}

		protected GameManager GameManager
		{
			get
			{
				return _gameManager;
			}
		}

		public TextureContent TextureContent
		{
			get
			{
				return _textureContent;
			}
		}

		public FontContent FontContent
		{
			get
			{
				return _fontContent;
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

		protected SpriteBatch SpriteBatch
		{
			get;
			private set;
		}

		public override void Initialize()
		{
			AddGameComponents();

			base.Initialize();
		}

		protected override void LoadContent()
		{
			SpriteBatch = new SpriteBatch(GraphicsDevice);

			base.LoadContent();
		}

		protected virtual void AddGameComponents()
		{
		}
	}
}