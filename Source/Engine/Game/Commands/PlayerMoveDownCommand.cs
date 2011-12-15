using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public class PlayerMoveDownCommand : PlayerMoveCommand
	{
		protected override Coordinate ModifyCoordinate(int x, int y)
		{
			return new Coordinate(x, y + 1);
		}
	}
}