using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Samples.Ctxna
{
	/// <remarks>
	/// Be sure to call ToArray() on any LINQ expressions to keep a single copy of objects persistent in memory.
	/// </remarks>
	public class CtxnaWorld : World
	{
		public static readonly Guid WorldId = Guid.Parse("b4320172-092e-4388-b876-99eb6b0b7960");

		public CtxnaWorld()
			: base(
				WorldId,
				1,
				"CTXNA.org",
				new StartingPlayer(),
				GetObjectsDerivedFrom<Board>(),
				GetObjectsDerivedFrom<Actor>(),
				GetObjectsDerivedFrom<Message>(),
				GetObjectsDerivedFrom<Timer>(),
				GetObjectsDerivedFrom<SoundEffect>(),
				GetObjectsDerivedFrom<Song>())
		{
		}

		private static T[] GetObjectsDerivedFrom<T>()
		{
			Assembly assembly = Assembly.GetExecutingAssembly();
			IEnumerable<Type> objectTypes = assembly.GetTypes().Where(arg => arg.IsSubclassOf(typeof(T)));

			return objectTypes
				.Select(arg => (T)Activator.CreateInstance(arg))
				.ToArray();
		}
	}
}