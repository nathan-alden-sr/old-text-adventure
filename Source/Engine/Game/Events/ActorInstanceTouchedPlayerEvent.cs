using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class ActorInstanceTouchedPlayerEvent : TargetedEvent<IPlayer>
	{
		private readonly IActorInstance _source;
		private readonly TouchDirection _touchDirection;

		public ActorInstanceTouchedPlayerEvent(IActorInstance source, IPlayer target, TouchDirection touchDirection)
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
				yield return "ID: " + _source.Id;
				yield return "Actor ID: " + _source.ActorId;
				yield return "Touch direction: " + _touchDirection;
			}
		}
	}
}