using System.Collections.Generic;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class ActorInstanceDestroyedEvent : TargetedEvent<IActorInstance>
	{
		public ActorInstanceDestroyedEvent(IActorInstance target)
			: base(target)
		{
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "ID: " + Target.Id;
				yield return "Actor ID: " + Target.ActorId;
			}
		}
	}
}