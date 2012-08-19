using TextAdventure.Engine.Objects;

namespace TextAdventure.WindowsGame.RendererStates
{
	public interface IBoardRendererState
	{
		Board Board
		{
			get;
		}
		Player Player
		{
			get;
		}
	}
}