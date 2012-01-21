using TextAdventure.Engine.Common;

namespace TextAdventure.Editor.RendererStates
{
	public interface IPencilRendererState
	{
		Coordinate OriginCoordinate
		{
			get;
		}
		int Size
		{
			get;
		}
	}
}