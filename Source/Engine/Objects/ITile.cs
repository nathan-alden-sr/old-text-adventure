using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface ITile
	{
		Coordinate Coordinate
		{
			get;
		}
		Character Character
		{
			get;
		}
	}
}