using System;

using Junior.Common;

using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.ToolActions
{
	public class EraserAction
	{
		private readonly IBoardRendererState _boardRendererState;
		private readonly IEraserRendererState _eraserRendererState;

		public EraserAction(IBoardRendererState boardRendererState, IEraserRendererState eraserRendererState)
		{
			boardRendererState.ThrowIfNull("boardRendererState");
			eraserRendererState.ThrowIfNull("eraserRendererState");

			_boardRendererState = boardRendererState;
			_eraserRendererState = eraserRendererState;
		}

		public void Draw()
		{
			Board board = _boardRendererState.Board;

			if (board == null || _eraserRendererState.TopLeftSelectionCoordinate == null)
			{
				return;
			}

			Coordinate topLeftSelectionCoordinate = _eraserRendererState.TopLeftSelectionCoordinate.Value;

			switch (_boardRendererState.ActiveLayer)
			{
				case Layer.Background:
					for (int x = topLeftSelectionCoordinate.X; x < topLeftSelectionCoordinate.X + _eraserRendererState.SelectionSize; x++)
					{
						for (int y = topLeftSelectionCoordinate.Y; y < topLeftSelectionCoordinate.Y + _eraserRendererState.SelectionSize; y++)
						{
							if (x < 0 || x >= board.Size.Width || y < 0 || y >= board.Size.Height)
							{
								continue;
							}

							var coordinate = new Coordinate(x, y);

							board.BackgroundLayer.RemoveTile(coordinate);
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