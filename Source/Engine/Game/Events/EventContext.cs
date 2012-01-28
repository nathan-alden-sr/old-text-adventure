using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Events
{
	public class EventContext : Context
	{
		public EventContext(WorldInstance worldInstance, CommandQueue commandQueue)
			: base(worldInstance, commandQueue)
		{
		}
	}
}