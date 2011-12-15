using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

using Color = Microsoft.Xna.Framework.Color;

namespace TextAdventure.WindowsGame.Components
{
	public class BoardComponent : TextAdventureDrawableGameComponent
	{
		private readonly WorldInstance _worldInstance;

		public BoardComponent(GameManager gameManager, WorldInstance worldInstance)
			: base(gameManager)
		{
			worldInstance.ThrowIfNull("worldInstance");

			_worldInstance = worldInstance;

			DrawOrder = ComponentDrawOrder.Board;
		}

		public override void Draw(GameTime gameTime)
		{
			Board board = _worldInstance.CurrentBoard;
			IEnumerable<ILayer> layers = new ILayer[]
			                             	{
			                             		board.BackgroundLayer,
			                             		board.ForegroundLayer,
			                             		board.ActorInstanceLayer
			                             	};
			Point topLeftPoint;
			Coordinate topLeftCoordinate;
			Coordinate bottomRightCoordinate;

			BoardDrawingHelper.Instance.GetDrawingParameters(
				board,
				_worldInstance.Player,
				out topLeftPoint,
				out topLeftCoordinate,
				out bottomRightCoordinate);

			SpriteBatch.Begin();

			foreach (ILayer layer in layers)
			{
				foreach (Tile tile in layer.Tiles.Where(arg => arg.Coordinate.X >= topLeftCoordinate.X &&
				                                               arg.Coordinate.X <= bottomRightCoordinate.X &&
				                                               arg.Coordinate.Y >= topLeftCoordinate.Y &&
				                                               arg.Coordinate.Y <= bottomRightCoordinate.Y))
				{
					Rectangle destinationRectangle = BoardDrawingHelper.Instance.GetTileDestinationRectangle(topLeftPoint, topLeftCoordinate, tile.Coordinate);

					SpriteBatch.Draw(TextureContent.Pixel, destinationRectangle, TextureContent.Pixel.Bounds, tile.Character.BackgroundColor.ToXnaColor());

					Rectangle sourceCharacterRectangle = BoardDrawingHelper.Instance.GetCharacterSourceRectangle(tile.Character.Symbol);

					SpriteBatch.Draw(TextureContent.Characters, destinationRectangle, sourceCharacterRectangle, tile.Character.ForegroundColor.ToXnaColor());
				}
			}

			SpriteBatch.GraphicsDevice.BlendState = BlendState.AlphaBlend;

			foreach (BoardExit boardExit in board.Exits)
			{
				Coordinate coordinate = boardExit.Coordinate;
				byte symbol;

				switch (boardExit.Direction)
				{
					case BoardExitDirection.Up:
						coordinate.Y--;
						symbol = 30;
						break;
					case BoardExitDirection.Down:
						coordinate.Y++;
						symbol = 31;
						break;
					case BoardExitDirection.Left:
						coordinate.X--;
						symbol = 16;
						break;
					case BoardExitDirection.Right:
						coordinate.X++;
						symbol = 17;
						break;
					default:
						throw new Exception(String.Format("Unexpected board exit direction '{0}'.", boardExit.Direction));
				}

				Rectangle destinationRectangle = BoardDrawingHelper.Instance.GetTileDestinationRectangle(topLeftPoint, topLeftCoordinate, coordinate);
				Rectangle sourceCharacterRectangle = BoardDrawingHelper.Instance.GetCharacterSourceRectangle(symbol);

				SpriteBatch.Draw(TextureContent.Characters, destinationRectangle, sourceCharacterRectangle, Color.White * 0.25f);
			}

			SpriteBatch.End();

			base.Draw(gameTime);
		}

		protected override void AddGameComponents()
		{
			var playerComponent = new PlayerComponent(GameManager, _worldInstance);

			Game.Components.Add(playerComponent);

			InputFocusManager.ClaimFocus(playerComponent);
		}
	}
}