using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class TimerActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("3e3d8da9-2931-43a6-88af-2fd2763906d1");

		public TimerActor()
			: base(ActorId, "Timer", "", new Character(Symbol.VerticalBar, Color.LightGreen, Color.TransparentBlack))
		{
		}
	}
}