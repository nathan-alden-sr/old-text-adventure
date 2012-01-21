using Microsoft.Xna.Framework;

using TextAdventure.Engine.Common;

namespace TextAdventure.Editor
{
	public interface IEditorView
	{
		Coordinate TopLeftCoordinate
		{
			get;
		}
		Size VisibleSizeInTiles
		{
			get;
		}
		System.Drawing.Size VisibleSizeInPixels
		{
			get;
		}
		Point TopLeftPoint
		{
			get;
		}
	}
}