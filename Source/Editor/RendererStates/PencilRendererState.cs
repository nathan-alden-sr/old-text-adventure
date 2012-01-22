using System;

using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.RendererStates
{
	public class PencilRendererState : IPencilRendererState
	{
		private Character _character;
		private int _size = 1;

		public PencilRendererState()
		{
			_character = new Character(0, Color.White, Color.TransparentBlack);
		}

		public bool Enabled
		{
			get;
			set;
		}

		public Coordinate? OriginCoordinate
		{
			get;
			set;
		}

		public Coordinate? TopLeftSelectionCoordinate
		{
			get;
			set;
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

		public int Size
		{
			get
			{
				return _size;
			}
			set
			{
				_size = Math.Max(1, value);
			}
		}
	}
}