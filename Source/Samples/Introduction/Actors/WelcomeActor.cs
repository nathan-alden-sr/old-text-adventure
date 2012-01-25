using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Introduction.Actors
{
	public class WelcomeActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("dbe75175-eec3-41f1-abf8-ca9bce9efa73");

		public WelcomeActor()
			: base(ActorId, "Welcome", "", new Character(Symbol.FilledSmiley, Color.Yellow, Color.TransparentBlack))
		{
		}
	}
}