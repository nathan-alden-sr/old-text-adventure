using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class ActorInstanceTouchedActorInstanceEvent : TargetedEvent<IActorInstance>
	{
		private readonly IActorInstance _source;
		private readonly TouchDirection _touchDirection;

		public ActorInstanceTouchedActorInstanceEvent(IActorInstance source, IActorInstance target, TouchDirection touchDirection)
			: base(target)
		{
			source.ThrowIfNull("source");

			_source = source;
			_touchDirection = touchDirection;
		}

		public IActorInstance Source
		{
			get
			{
				return _source;
			}
		}

		public TouchDirection TouchDirection
		{
			get
			{
				return _touchDirection;
			}
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "Source ID: " + _source.Id;
				yield return "Source actor ID: " + _source.ActorId;
				yield return "Target ID: " + Target.Id;
				yield return "Target actor ID: " + Target.ActorId;
				yield return "Touch direction: " + _touchDirection;
			}
		}
	}
}