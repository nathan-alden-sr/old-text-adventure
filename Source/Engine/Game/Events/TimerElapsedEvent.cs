using System.Collections.Generic;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class TimerElapsedEvent : TargetedEvent<ITimer>
	{
		public TimerElapsedEvent(ITimer target)
			: base(target)
		{
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return "ID: " + Target.Id;
				yield return "Interval: " + Target.Interval.ToString("c");
			}
		}
	}
}