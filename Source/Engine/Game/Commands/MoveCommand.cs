using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Game.Commands
{
	public abstract class MoveCommand : Command
	{
		protected TouchDirection? GetTouchDirection(Coordinate currentCoordinate, Coordinate newCoordinate)
		{
			if (currentCoordinate.X < newCoordinate.X)
			{
				return TouchDirection.FromLeft;
			}
			if (currentCoordinate.X > newCoordinate.X)
			{
				return TouchDirection.FromRight;
			}
			if (currentCoordinate.Y < newCoordinate.Y)
			{
				return TouchDirection.FromAbove;
			}
			if (currentCoordinate.Y > newCoordinate.Y)
			{
				return TouchDirection.FromBelow;
			}

			return null;
		}
	}
}