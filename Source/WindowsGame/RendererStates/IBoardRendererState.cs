using TextAdventure.Engine.Objects;

namespace TextAdventure.WindowsGame.RendererStates
{
	public interface IBoardRendererState
	{
		IBoard Board
		{
			get;
		}
		IPlayer Player
		{
			get;
		}
	}
}