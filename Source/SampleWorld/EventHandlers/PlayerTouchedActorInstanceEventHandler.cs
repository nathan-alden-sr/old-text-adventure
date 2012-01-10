using TextAdventure.Engine.Game.Events;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler : EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(IEventContext context, PlayerTouchedActorInstanceEvent @event)
		{
		}
	}
}