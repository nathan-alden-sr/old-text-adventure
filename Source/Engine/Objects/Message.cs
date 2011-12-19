using System;
using System.Collections.Generic;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class Message : IUnique
	{
		private readonly Guid _id;
		private readonly IEnumerable<IMessagePart> _parts;

		public Message(Guid id, IEnumerable<IMessagePart> parts)
		{
			parts.ThrowIfNull("parts");

			_id = id;
			_parts = parts;
		}

		public IEnumerable<IMessagePart> Parts
		{
			get
			{
				return _parts;
			}
		}

		public Guid Id
		{
			get
			{
				return _id;
			}
		}
	}
}