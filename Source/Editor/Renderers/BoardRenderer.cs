using System;
using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.Xna.Extensions;

namespace TextAdventure.Editor.Renderers
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

			Board board = _state.Board;

			if (board == null)
			{
				return;
			}

			IEnumerable<ILayer> layers = new ILayer[]
			                             	{
			                             		_state.Board.BackgroundLayer,
			                             		_state.Board.ForegroundLayer,
			                             		_state.Board.ActorInstanceLayer
			                             	};
			Coordinate topLeftCoordinate = _state.ScrollCoordinate;
			Coordinate bottomRightCoordinate = GetBottomRightCoordinate(parameters.ViewportRectangle);

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone);

			foreach (ILayer layer in layers)
			{
				IEnumerable<Tile> tiles = layer.Tiles.Where(
					arg => arg.Coordinate.X >= topLeftCoordinate.X &&
					       arg.Coordinate.X <= bottomRightCoordinate.X &&
					       arg.Coordinate.Y >= topLeftCoordinate.Y &&
					       arg.Coordinate.Y <= bottomRightCoordinate.Y);

				foreach (Tile tile in tiles)
				{
					Rectangle destinationRectangle = GetTileDestinationRectangle(topLeftCoordinate, tile.Coordinate);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Pixel, destinationRectangle, parameters.TextureContent.Pixel.Bounds, tile.Character.BackgroundColor.ToXnaColor());

					Rectangle sourceCharacterRectangle = GetCharacterSourceRectangle(tile.Character.Symbol);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Characters, destinationRectangle, sourceCharacterRectangle, tile.Character.ForegroundColor.ToXnaColor());
				}
			}

			parameters.SpriteBatch.End();
		}

		private Coordinate GetBottomRightCoordinate(Rectangle viewportRectangle)
		{
			var widthInTiles = (int)Math.Ceiling(viewportRectangle.Width / (double)TextAdventure.Xna.Constants.Tile.TileWidth);
			var heightInTiles = (int)Math.Ceiling(viewportRectangle.Height / (double)TextAdventure.Xna.Constants.Tile.TileHeight);

			return new Coordinate(_state.ScrollCoordinate.X + widthInTiles - 1, _state.ScrollCoordinate.Y + heightInTiles - 1);
		}

		private static Rectangle GetCharacterSourceRectangle(byte symbol)
		{
			return new Rectangle(
				(symbol % 16) * TextAdventure.Xna.Constants.Tile.TileWidth,
				(symbol / 16) * TextAdventure.Xna.Constants.Tile.TileHeight,
				TextAdventure.Xna.Constants.Tile.TileWidth,
				TextAdventure.Xna.Constants.Tile.TileHeight);
		}

		private static Rectangle GetTileDestinationRectangle(Coordinate topLeftCoordinate, Coordinate tileCoordinate)
		{
			return new Rectangle(
				(tileCoordinate.X - topLeftCoordinate.X) * TextAdventure.Xna.Constants.Tile.TileWidth,
				(tileCoordinate.Y - topLeftCoordinate.Y) * TextAdventure.Xna.Constants.Tile.TileHeight,
				TextAdventure.Xna.Constants.Tile.TileWidth,
				TextAdventure.Xna.Constants.Tile.TileHeight);
		}
	}
}