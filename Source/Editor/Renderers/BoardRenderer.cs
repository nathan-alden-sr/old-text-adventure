using System.Collections.Generic;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;
using TextAdventure.Xna.Extensions;
using TextAdventure.Xna.Helpers;

namespace TextAdventure.Editor.Renderers
{
	public class BoardRenderer : IRenderer
	{
		private readonly IEditorView _editorView;
		private readonly IBoardRendererState _state;

		public BoardRenderer(IBoardRendererState state, IEditorView editorView)
		{
			state.ThrowIfNull("state");
			editorView.ThrowIfNull("editorView");

			_state = state;
			_editorView = editorView;
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
			Coordinate topLeftCoordinate = _editorView.TopLeftCoordinate;
			var bottomRightCoordinate = new Coordinate(topLeftCoordinate.X + _editorView.ClientSizeInTiles.Width - 1, topLeftCoordinate.Y + _editorView.ClientSizeInTiles.Height - 1);

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
					Rectangle destinationRectangle = GetTileDestinationRectangle(_editorView.TopLeftPoint, topLeftCoordinate, tile.Coordinate);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Pixel, destinationRectangle, parameters.TextureContent.Pixel.Bounds, tile.Character.BackgroundColor.ToXnaColor());

					Rectangle symbolSourceRectangle = CharacterTextureHelper.GetSymbolSourceRectangle(tile.Character.Symbol);

					parameters.SpriteBatch.Draw(parameters.TextureContent.Characters, destinationRectangle, symbolSourceRectangle, tile.Character.ForegroundColor.ToXnaColor());
				}
			}

			parameters.SpriteBatch.End();
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