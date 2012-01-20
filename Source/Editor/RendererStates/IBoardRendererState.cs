using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.RendererStates
{
	public interface IBoardRendererState
	{
		Board Board
		{
			get;
		}
		Coordinate ScrollCoordinate
		{
			get;
		}
		bool BackgroundLayerVisible
		{
			get;
		}
		bool ForegroundLayerVisible
		{
			get;
		}
		bool ActorInstanceLayerVisible
		{
			get;
		}
		Layer ActiveLayer
		{
			get;
		}
	}
}