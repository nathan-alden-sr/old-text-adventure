using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Commands
{
	public class ActorInstanceMoveLeftCommand : ActorInstanceMoveCommand
	{
		public ActorInstanceMoveLeftCommand(ActorInstance actorInstance)
			: base(actorInstance)
		{
		}

		protected override Coordinate ModifyCoordinate(int x, int y)
		{
			return new Coordinate(x - 1, y);
		}
	}
}