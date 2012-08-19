using TextAdventure.Engine.Common;

namespace TextAdventure.Editor.RendererStates
{
	public interface IEraserRendererState
	{
		bool Enabled
		{
			get;
		}
		Coordinate? TopLeftSelectionCoordinate
		{
			get;
		}
		int SelectionSize
		{
			get;
		}
	}
}