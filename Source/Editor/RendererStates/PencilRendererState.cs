using System;

using TextAdventure.Engine.Common;

namespace TextAdventure.Editor.RendererStates
{
	public class PencilRendererState : IPencilRendererState
	{
		private int _size = 1;

		public Coordinate OriginCoordinate
		{
			get;
			set;
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