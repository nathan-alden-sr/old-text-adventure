using Junior.Common;

namespace TextAdventure.Engine.Objects
{
	public class MessageText : IMessagePart
	{
		private readonly string _text;

		public MessageText(string text)
		{
			text.ThrowIfNull("text");

			_text = text;
		}

		public string Text
		{
			get
			{
				return _text;
			}
		}
	}
}