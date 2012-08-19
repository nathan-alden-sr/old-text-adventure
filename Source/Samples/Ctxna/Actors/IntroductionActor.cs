using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class IntroductionActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("dbe75175-eec3-41f1-abf8-ca9bce9efa73");

		public IntroductionActor()
			: base(ActorId, "Introduction", "", new Character(Symbol.FilledSmiley, Color.Yellow, Color.TransparentBlack))
		{
		}
	}
}