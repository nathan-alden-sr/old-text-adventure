using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.RendererStates
{
	public class BoardRendererState : IBoardRendererState
	{
		public Board Board
		{
			get;
			set;
		}

		public bool BackgroundLayerVisible
		{
			get;
			set;
		}

		public bool ForegroundLayerVisible
		{
			get;
			set;
		}

		public bool ActorInstanceLayerVisible
		{
			get;
			set;
		}

		public Layer ActiveLayer
		{
			get;
			set;
		}
	}
}