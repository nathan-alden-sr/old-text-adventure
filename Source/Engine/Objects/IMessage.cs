using System.Collections.Generic;

namespace TextAdventure.Engine.Objects
{
	public interface IMessage : IUnique
	{
		IEnumerable<IMessagePart> Parts
		{
			get;
		}
	}
}