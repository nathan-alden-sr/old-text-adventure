using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerMoveRightCommand : PlayerMoveCommand
	{
		protected override Coordinate ModifyCoordinate(int x, int y)
		{
			return new Coordinate(x + 1, y);
		}
	}
}