using System;
using System.Collections.Generic;

using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class MessageAnswer : IUnique
	{
		private readonly Guid _id;
		private readonly IEnumerable<IMessagePart> _parts;
		private readonly string _text;

		public MessageAnswer(
			Guid id,
			string text,
			IEnumerable<IMessagePart> parts)
		{
			parts.ThrowIfNull("parts");
			text.ThrowIfNull("text");

			_id = id;
			_text = text;
			_parts = parts;
		}

		public string Text
		{
			get
			{
				return _text;
			}
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