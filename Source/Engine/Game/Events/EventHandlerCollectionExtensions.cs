using TextAdventure.Engine.Game.World;

namespace TextAdventure.Engine.Game.Events
{
	internal static class EventHandlerCollectionExtensions
	{
		public static EventResult SafeInvoke<TEvent>(this EventHandlerCollection eventHandlerCollection, EventContext context, TEvent @event)
			where TEvent : Event
		{
			return eventHandlerCollection != null ? eventHandlerCollection.Invoke(context, @event) : EventResult.None;
		}
	}
}