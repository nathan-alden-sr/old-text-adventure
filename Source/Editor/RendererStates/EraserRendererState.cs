using System;

using TextAdventure.Engine.Common;

namespace TextAdventure.Editor.RendererStates
{
	public class EraserRendererState : IEraserRendererState
	{
		private int _selectionSize = 1;

		public bool Enabled
		{
			get;
			set;
		}

		public Coordinate? TopLeftSelectionCoordinate
		{
			get;
			private set;
		}

		public int SelectionSize
		{
			get
			{
				return _selectionSize;
			}
			set
			{
				_selectionSize = Math.Max(1, value);
			}
		}

		public void SetSelection(Coordinate originCoordinate)
		{
			var offset = (int)Math.Ceiling((SelectionSize / 2f) - 1);

			TopLeftSelectionCoordinate = new Coordinate(originCoordinate.X - offset, originCoordinate.Y - offset);
		}

		public void ClearSelection()
		{
			TopLeftSelectionCoordinate = null;
		}
	}
}