using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Objects;

namespace TextAdventure.SampleWorld.EventHandlers
{
	public class PlayerTouchedActorInstanceEventHandler : Engine.Game.Events.EventHandler<PlayerTouchedActorInstanceEvent>
	{
		public override void HandleEvent(IEventContext context, PlayerTouchedActorInstanceEvent @event)
		{
			Message message = Message.Build(new Color(0f, 0f, 0.5f))
				.Question(
					"Where do you want to go today?",
					Color.Cyan,
					Color.White,
					Color.Yellow,
					new Color(0.5f, 0f, 0f),
					MessageAnswer.Build("Bill Gates' House"),
					MessageAnswer.Build("Sergey Brin's House"),
					MessageAnswer.Build("My House"));
			var command = new MessageCommand(message);

			context.EnqueueCommand(command);
		}
	}
}