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

		protected override Coordinate ModifyCoordinate(int x, int y)
		{
			return new Coordinate(x, y + 1);
		}
	}
}