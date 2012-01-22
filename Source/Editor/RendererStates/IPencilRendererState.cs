using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.RendererStates
{
	public interface IPencilRendererState
	{
		bool Enabled
		{
			get;
		}
		Coordinate? OriginCoordinate
		{
			get;
		}
		Coordinate? TopLeftSelectionCoordinate
		{
			get;
		}
		Character Character
		{
			get;
		}
		int Size
		{
			get;
		}
	}
}