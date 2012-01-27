using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.Xna.Extensions;
using TextAdventure.Xna.Helpers;

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

			parameters.SpriteBatch.Begin(SpriteSortMode.Deferred, BlendState.NonPremultiplied, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			foreach (ILayer layer in layers)
			{
				IEnumerable<Tile> tiles = layer.Tiles.Where(
					arg => arg.Coordinate.X >= topLeftCoordinate.X &&
					       arg.Coordinate.X <= bottomRightCoordinate.X &&
					       arg.Coordinate.Y >= topLeftCoordinate.Y &&
					       arg.Coordinate.Y <= bottomRightCoordinate.Y);

				foreach (Tile tile in tiles)
				{
					Rectangle destinationRectangle = GetTileDestinationRectangle(topLeftPoint, topLeftCoordinate, tile.Coordinate);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Pixel, destinationRectangle, parameters.TextureContent.Pixel.Bounds, tile.Character.BackgroundColor.ToXnaColor());

					Rectangle symbolSourceRectangle = CharacterTextureHelper.GetSymbolSourceRectangle(tile.Character.Symbol);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Characters, destinationRectangle, symbolSourceRectangle, tile.Character.ForegroundColor.ToXnaColor());
				}
			}

			foreach (BoardExit boardExit in _state.Board.Exits)
			{
				Coordinate coordinate = boardExit.Coordinate;
				byte symbol;

				switch (boardExit.Direction)
				{
					case BoardExitDirection.Up:
						coordinate.Y--;
						symbol = TextAdventure.Xna.Constants.BoardRenderer.BoardExitUpSymbol;
						break;
					case BoardExitDirection.Down:
						coordinate.Y++;
						symbol = TextAdventure.Xna.Constants.BoardRenderer.BoardExitDownSymbol;
						break;
					case BoardExitDirection.Left:
						coordinate.X--;
						symbol = TextAdventure.Xna.Constants.BoardRenderer.BoardExitLeftSymbol;
						break;
					case BoardExitDirection.Right:
						coordinate.X++;
						symbol = TextAdventure.Xna.Constants.BoardRenderer.BoardExitRightSymbol;
						break;
					default:
						throw new Exception(String.Format("Unexpected board exit direction '{0}'.", boardExit.Direction));
				}

				Rectangle destinationRectangle = GetTileDestinationRectangle(topLeftPoint, topLeftCoordinate, coordinate);
				Rectangle symbolSourceRectangle = CharacterTextureHelper.GetSymbolSourceRectangle(symbol);

				parameters.SpriteBatch.Draw(parameters.TextureContent.Characters, destinationRectangle, symbolSourceRectangle, TextAdventure.Xna.Constants.BoardRenderer.BoardExitSymbolColor);
			}

			parameters.SpriteBatch.End();
		}

		private static void GetDrawingParameters(
			Board board,
			Player player,
			out Point topLeftPoint,
			out Coordinate topLeftCoordinate,
			out Coordinate bottomRightCoordinate)
		{
			int drawableTilesToLeft = Math.Min(Constants.GameWindow.TilesToLeftInclusive, player.Coordinate.X);
			int drawableTilesToTop = Math.Min(Constants.GameWindow.TilesToTopInclusive, player.Coordinate.Y);
			int drawableTilesToRight = Math.Min(Constants.GameWindow.TilesToRightExclusive, board.Size.Width - player.Coordinate.X - 1);
			int drawableTilesToBottom = Math.Min(Constants.GameWindow.TilesToBottomExclusive, board.Size.Height - player.Coordinate.Y - 1);

			topLeftPoint = new Point(
				Constants.PlayerRenderer.DestinationRectangle.X - (drawableTilesToLeft * TextAdventure.Xna.Constants.Tile.TileWidth),
				Constants.PlayerRenderer.DestinationRectangle.Y - (drawableTilesToTop * TextAdventure.Xna.Constants.Tile.TileHeight));
			topLeftCoordinate = new Coordinate(player.Coordinate.X - drawableTilesToLeft, player.Coordinate.Y - drawableTilesToTop);
			bottomRightCoordinate = new Coordinate(player.Coordinate.X + drawableTilesToRight, player.Coordinate.Y + drawableTilesToBottom);
		}

		private static Rectangle GetTileDestinationRectangle(Point topLeftPoint, Coordinate topLeftCoordinate, Coordinate tileCoordinate)
		{
			return new Rectangle(
				topLeftPoint.X + ((tileCoordinate.X - topLeftCoordinate.X) * TextAdventure.Xna.Constants.Tile.TileWidth),
				topLeftPoint.Y + ((tileCoordinate.Y - topLeftCoordinate.Y) * TextAdventure.Xna.Constants.Tile.TileHeight),
				TextAdventure.Xna.Constants.Tile.TileWidth,
				TextAdventure.Xna.Constants.Tile.TileHeight);
		}
	}
}