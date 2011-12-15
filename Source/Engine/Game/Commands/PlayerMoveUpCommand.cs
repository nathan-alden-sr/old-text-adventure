using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerMoveUpCommand : PlayerMoveCommand
	{
		protected override Coordinate ModifyCoordinate(int x, int y)
		{
			return new Coordinate(x, y - 1);
		}
	}
}