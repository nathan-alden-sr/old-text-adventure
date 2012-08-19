using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Commands
{
	public class CommandContext : Context
	{
		public CommandContext(WorldInstance worldInstance, CommandQueue commandQueue)
			: base(worldInstance, commandQueue)
		{
		}
	}
}