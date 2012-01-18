using System.Collections.Generic;

using Junior.Common;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class ActorInstanceTouchedPlayerEvent : TargetedEvent<Player>
	{
		private readonly ActorInstance _source;
		private readonly TouchDirection _touchDirection;

		public ActorInstanceTouchedPlayerEvent(ActorInstance source, Player target, TouchDirection touchDirection)
			: base(target)
		{
			source.ThrowIfNull("source");

			_source = source;
			_touchDirection = touchDirection;
		}

		public ActorInstance Source
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
				yield return FormatNamedObjectDetailText("Actor instance", _source);
				yield return FormatIdDetailText("Actor", _source.ActorId);
				yield return "Touch direction: " + _touchDirection;
			}
		}
	}
}