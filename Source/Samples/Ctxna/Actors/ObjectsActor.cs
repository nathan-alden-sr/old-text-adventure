using System;

using TextAdventure.Engine.Common;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna.Actors
{
	public class ObjectsActor : Actor
	{
		public static readonly Guid ActorId = Guid.Parse("607e9f11-93e0-4b69-91ca-f74d08cb5214");

		public ObjectsActor()
			: base(ActorId, "Objects", "", new Character(Symbol.FilledSmiley, Color.LightGreen, Color.TransparentBlack))
		{
		}
	}
}