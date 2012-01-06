namespace TextAdventure.Engine.Objects
{
	public class MessageText : IMessageText
	{
		private readonly string _text;

		public MessageText(string text)
		{
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