using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class PlayerActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("0ff0e995-6e5f-4903-922e-ff2eff173605");

		public PlayerActor()
			: base(ActorId, "Player", "", new Character(Symbol.FilledSmiley, Color.White, Color.DarkBlue))
		{
		}
	}
}