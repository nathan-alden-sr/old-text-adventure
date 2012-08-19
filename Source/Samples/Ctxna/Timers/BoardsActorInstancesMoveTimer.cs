using System;
using System.Collections.Generic;
using System.Linq;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.Samples.Ctxna.Actors;

namespace TextAdventure.Samples.Ctxna.Timers
{
	public class BoardsActorInstancesMoveTimer : Timer
	{
		public static readonly Guid TimerId = Guid.Parse("baaf3527-9f29-4c6c-92fd-66631d08c3f9");

		public BoardsActorInstancesMoveTimer()
			: base(TimerId, "BoardsActorInstances", "", TimeSpan.FromSeconds(1))
		{
		}

		protected override EventResult OnElapsed(EventContext context, TimerElapsedEvent @event)
		{
			IEnumerable<ActorInstance> actorInstances = context.CurrentBoard.ActorInstanceLayer.ActorInstances.Where(arg => arg.ActorId == BoardsActor.ActorId);

			foreach (ActorInstanceRandomMoveCommand command in actorInstances.Select(arg => Commands.ActorInstanceRandomMove(arg)))
			{
				context.EnqueueCommand(command);
			}

			return EventResult.Completed;
		}
	}
}