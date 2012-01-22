using System;

using Junior.Common;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.ToolActions
{
	public class PencilAction
	{
		private readonly IBoardRendererState _boardRendererState;
		private readonly IPencilRendererState _pencilRendererState;

		public PencilAction(IBoardRendererState boardRendererState, IPencilRendererState pencilRendererState)
		{
			boardRendererState.ThrowIfNull("boardRendererState");
			pencilRendererState.ThrowIfNull("pencilRendererState");

			_boardRendererState = boardRendererState;
			_pencilRendererState = pencilRendererState;
		}

		public void Draw()
		{
			Board board = _boardRendererState.Board;

			if (board == null || _pencilRendererState.TopLeftSelectionCoordinate == null)
			{
				return;
			}

			Coordinate topLeftSelectionCoordinate = _pencilRendererState.TopLeftSelectionCoordinate.Value;

			switch (_boardRendererState.ActiveLayer)
			{
				case Layer.Background:
					for (int x = topLeftSelectionCoordinate.X; x < topLeftSelectionCoordinate.X + _pencilRendererState.SelectionSize; x++)
					{
						for (int y = topLeftSelectionCoordinate.Y; y < topLeftSelectionCoordinate.Y + _pencilRendererState.SelectionSize; y++)
						{
							if (x < 0 || x >= board.Size.Width || y < 0 || y >= board.Size.Height)
							{
								continue;
							}

							var coordinate = new Coordinate(x, y);
							var sprite = new Sprite(coordinate, _pencilRendererState.Character);

							board.BackgroundLayer.SetTile(coordinate, sprite);
						}
					}
					break;
				case Layer.Foreground:
					break;
				case Layer.ActorInstance:
					break;
				default:
					throw new Exception(String.Format("Unexpected active layer '{0}'.", _boardRendererState.ActiveLayer));
			}
		}
	}
}