using System.Collections.Generic;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Game.Events
{
	public class TimerElapsedEvent : TargetedEvent<Timer>
	{
		public TimerElapsedEvent(Timer target)
			: base(target)
		{
		}

		public override IEnumerable<string> Details
		{
			get
			{
				yield return FormatNamedObjectDetailText("Timer", Target);
				yield return "Interval: " + Target.Interval.ToString("c");
			}
		}
	}
}