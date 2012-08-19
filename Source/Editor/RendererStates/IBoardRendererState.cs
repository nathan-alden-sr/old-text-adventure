using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.RendererStates
{
	public interface IBoardRendererState
	{
		Board Board
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