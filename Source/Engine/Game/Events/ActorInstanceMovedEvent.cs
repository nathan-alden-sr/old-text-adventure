using System.Collections.Generic;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class ActorInstanceMovedEvent : TargetedEvent<ActorInstance>
	{
		private readonly Coordinate _newCoordinate;

		public ActorInstanceMovedEvent(ActorInstance target, Coordinate newCoordinate)
			: base(target)
		{
			_newCoordinate = newCoordinate;
		}

		public Coordinate NewCoordinate
		{
			get
			{
				return _newCoordinate;
			}
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Actor instance", Target);
				yield return FormatIdDetailText("Actor", Target.ActorId);
				yield return "Old coordinate: " + Target.Coordinate;
				yield return "New coordinate: " + _newCoordinate;
			}
		}
	}
}