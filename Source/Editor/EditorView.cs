using Microsoft.Xna.Framework;

using TextAdventure.Engine.Common;

namespace TextAdventure.Editor
{
	public class EditorView : IEditorView
	{
		public Coordinate TopLeftCoordinate
		{
			get;
			set;
		}

		public Size VisibleSizeInTiles
		{
			get;
			set;
		}

		public System.Drawing.Size VisibleSizeInPixels
		{
			get;
			set;
		}

		public Point TopLeftPoint
		{
			get;
			set;
		}
	}
}