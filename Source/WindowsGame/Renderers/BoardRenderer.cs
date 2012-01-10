using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.Renderers
{
	public class BoardRenderer : IRenderer
	{
		private readonly IBoardRendererState _state;

		public BoardRenderer(IBoardRendererState state)
		{
			state.ThrowIfNull("state");

			_state = state;
		}

		public void Render(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			IEnumerable<ILayer> layers = new ILayer[]
			                             	{
			                             		_state.Board.BackgroundLayer,
			                             		_state.Board.ForegroundLayer,
			                             		_state.Board.ActorInstanceLayer
			                             	};
			Point topLeftPoint;
			Coordinate topLeftCoordinate;
			Coordinate bottomRightCoordinate;

			GetDrawingParameters(
				_state.Board,
				_state.Player,
				out topLeftPoint,
				out topLeftCoordinate,
				out bottomRightCoordinate);

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend);

			foreach (ILayer layer in layers)
			{
				IEnumerable<ITile> tiles = layer.Tiles.Where(
					arg => arg.Coordinate.X >= topLeftCoordinate.X &&
					       arg.Coordinate.X <= bottomRightCoordinate.X &&
					       arg.Coordinate.Y >= topLeftCoordinate.Y &&
					       arg.Coordinate.Y <= bottomRightCoordinate.Y);

				foreach (ITile tile in tiles)
				{
					Rectangle destinationRectangle = GetTileDestinationRectangle(topLeftPoint, topLeftCoordinate, tile.Coordinate);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Pixel, destinationRectangle, parameters.TextureContent.Pixel.Bounds, tile.Character.BackgroundColor.ToXnaColor());

					Rectangle sourceCharacterRectangle = GetCharacterSourceRectangle(tile.Character.Symbol);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Characters, destinationRectangle, sourceCharacterRectangle, tile.Character.ForegroundColor.ToXnaColor());
				}
			}

			foreach (IBoardExit boardExit in _state.Board.Exits)
			{
				Coordinate coordinate = boardExit.Coordinate;
				byte symbol;

				switch (boardExit.Direction)
				{
					case BoardExitDirection.Up:
						coordinate.Y--;
						symbol = Constants.BoardRenderer.BoardExitUpSymbol;
						break;
					case BoardExitDirection.Down:
						coordinate.Y++;
						symbol = Constants.BoardRenderer.BoardExitDownSymbol;
						break;
					case BoardExitDirection.Left:
						coordinate.X--;
						symbol = Constants.BoardRenderer.BoardExitLeftSymbol;
						break;
					case BoardExitDirection.Right:
						coordinate.X++;
						symbol = Constants.BoardRenderer.BoardExitRightSymbol;
						break;
					default:
						throw new Exception(String.Format("Unexpected board exit direction '{0}'.", boardExit.Direction));
				}

				Rectangle destinationRectangle = GetTileDestinationRectangle(topLeftPoint, topLeftCoordinate, coordinate);
				Rectangle sourceCharacterRectangle = GetCharacterSourceRectangle(symbol);

				parameters.SpriteBatch.Draw(parameters.TextureContent.Characters, destinationRectangle, sourceCharacterRectangle, Constants.BoardRenderer.BoardExitSymbolColor);
			}

			parameters.SpriteBatch.End();
		}

		private static void GetDrawingParameters(
			IBoard board,
			IPlayer player,
			out Point topLeftPoint,
			out Coordinate topLeftLayerCoordinate,
			out Coordinate bottomRightCoordinate)
		{
			int drawableTilesToLeft = Math.Min(Constants.GameWindow.TilesToLeftInclusive, player.Coordinate.X);
			int drawableTilesToTop = Math.Min(Constants.GameWindow.TilesToTopInclusive, player.Coordinate.Y);
			int drawableTilesToRight = Math.Min(Constants.GameWindow.TilesToRightExclusive, board.Size.Width - player.Coordinate.X - 1);
			int drawableTilesToBottom = Math.Min(Constants.GameWindow.TilesToBottomExclusive, board.Size.Height - player.Coordinate.Y - 1);

			topLeftPoint = new Point(
				Constants.PlayerRenderer.DestinationRectangle.X - (drawableTilesToLeft * Constants.Tile.TileWidth),
				Constants.PlayerRenderer.DestinationRectangle.Y - (drawableTilesToTop * Constants.Tile.TileHeight));
			topLeftLayerCoordinate = new Coordinate(player.Coordinate.X - drawableTilesToLeft, player.Coordinate.Y - drawableTilesToTop);
			bottomRightCoordinate = new Coordinate(player.Coordinate.X + drawableTilesToRight, player.Coordinate.Y + drawableTilesToBottom);
		}

		private static Rectangle GetCharacterSourceRectangle(byte symbol)
		{
			return new Rectangle(
				(symbol % 16) * Constants.Tile.TileWidth,
				(symbol / 16) * Constants.Tile.TileHeight,
				Constants.Tile.TileWidth,
				Constants.Tile.TileHeight);
		}

		private static Rectangle GetTileDestinationRectangle(Point topLeftPoint, Coordinate topLeftCoordinate, Coordinate tileCoordinate)
		{
			return new Rectangle(
				topLeftPoint.X + ((tileCoordinate.X - topLeftCoordinate.X) * Constants.Tile.TileWidth),
				topLeftPoint.Y + ((tileCoordinate.Y - topLeftCoordinate.Y) * Constants.Tile.TileHeight),
				Constants.Tile.TileWidth,
				Constants.Tile.TileHeight);
		}
	}
}