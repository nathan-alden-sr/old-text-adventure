using System;

using TextAdventure.Engine.Game.Events;

namespace TextAdventure.Engine.Objects
{
	public interface ITimer : IUnique
	{
		TimeSpan Interval
		{
			get;
		}
		TimerState State
		{
			get;
		}
		TimeSpan Elapsed
		{
			get;
		}
		IEventHandler<TimerElapsedEvent> TimerElapsedEventHandler
		{
			get;
		}
	}
}