using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class GameBackgroundComponent : TextAdventureDrawableGameComponent
	{
		public GameBackgroundComponent(GameManager gameManager)
			: base(gameManager)
		{
			DrawOrder = ComponentDrawOrder.Background;
		}

		public override void Draw(GameTime gameTime)
		{
			SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone);

			SpriteBatch.Draw(TextureContent.GameBackground, DrawingConstants.GameWindow.DestinationRectangle, Color.White);

			SpriteBatch.End();

			base.Draw(gameTime);
		}
	}
}