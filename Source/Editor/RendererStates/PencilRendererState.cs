using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.RendererStates
{
	public class PencilRendererState : IPencilRendererState
	{
		private Character _character;
		private int _selectionSize = 1;

		public PencilRendererState()
		{
			_character = new Character(0, Color.White, Color.TransparentBlack);
		}

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

		public Character Character
		{
			get
			{
				return _character;
			}
			set
			{
				value.ThrowIfNull("value");

				_character = value;
			}
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