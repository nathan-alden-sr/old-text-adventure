using System;
using System.Collections.Generic;

namespace TextAdventure.Engine.Objects
{
	public interface IActorInstanceLayer : ILayer<IActorInstance>
	{
		IEnumerable<IActorInstance> ActorInstances
		{
			get;
		}

		IActorInstance GetActorInstanceById(Guid id);
		IEnumerable<IActorInstance> GetActorInstancesByActorId(Guid actorId);
	}
}