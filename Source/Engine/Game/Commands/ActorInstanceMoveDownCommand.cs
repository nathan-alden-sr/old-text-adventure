using Junior.Common;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceMoveDownCommand : ActorInstanceMoveCommand
	{
		public ActorInstanceMoveDownCommand(ActorInstance actorInstance)
			: base(actorInstance)
		{
		}

		protected override Coordinate? ModifyCoordinate(Board board, Player player, int x, int y)
		{
			board.ThrowIfNull("board");
			player.ThrowIfNull("player");

			return new Coordinate(x, y + 1);
		}
	}
}