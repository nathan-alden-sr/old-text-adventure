using System.Linq;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler : EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(EventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			StartTimerCommand command = Commands.StartTimer(context.Timers.Single());

			context.EnqueueCommand(command);
		}
	}
}