using System.Drawing;

using TextAdventure.Engine.Common;

using Size = TextAdventure.Engine.Common.Size;

namespace TextAdventure.Editor
{
	public interface IEditorView
	{
		Point ScrollPositionInPixels
		{
			get;
		}
		Size ClientSizeInTiles
		{
			get;
		}
		System.Drawing.Size ClientSizeInPixels
		{
			get;
		}
		Size BoardSizeInTiles
		{
			get;
		}
		System.Drawing.Size BoardSizeInPixels
		{
			get;
		}
		Microsoft.Xna.Framework.Point TopLeftPoint
		{
			get;
		}
		Coordinate TopLeftCoordinate
		{
			get;
		}
		Coordinate BottomRightCoordinate
		{
			get;
		}
		System.Drawing.Size VisibleBoardSizeInPixels
		{
			get;
		}
	}
}