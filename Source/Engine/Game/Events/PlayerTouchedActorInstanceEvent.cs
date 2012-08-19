using System.Collections.Generic;

using TextAdventure.Engine.Game.Commands;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class PlayerTouchedActorInstanceEvent : TargetedEvent<ActorInstance>
	{
		private readonly TouchDirection _touchDirection;

		public PlayerTouchedActorInstanceEvent(ActorInstance target, TouchDirection touchDirection)
			: base(target)
		{
			_touchDirection = touchDirection;
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
				yield return FormatNamedObjectDetailText("Actor instance", Target);
				yield return FormatIdDetailText("Actor", Target.ActorId);
				yield return "Touch direction: " + _touchDirection;
			}
		}
	}
}