using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.Timers
{
	public class BoardsActorMoveTimer : Timer
	{
		public static readonly Guid TimerId = Guid.Parse("baaf3527-9f29-4c6c-92fd-66631d08c3f9");

		public BoardsActorMoveTimer()
			: base(TimerId, "BoardsActor", "", TimeSpan.FromMilliseconds(500))
		{
		}

		protected override EventResult OnElapsed(EventContext context, TimerElapsedEvent @event)
		{
			IEnumerable<ActorInstance> actorInstances = context.CurrentBoard.ActorInstanceLayer.ActorInstances;

			foreach (ActorInstanceRandomMoveCommand command in actorInstances.Select(arg => Commands.ActorInstanceRandomMove(arg)))
			{
				context.EnqueueCommand(command);
			}

			context.EnqueueCommand(Commands.StartTimer(@event.Target));

			return EventResult.Complete;
		}
	}
}