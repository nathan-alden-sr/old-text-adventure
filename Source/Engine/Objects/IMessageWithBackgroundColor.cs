using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface IMessageWithBackgroundColor : IMessage
	{
		Color BackgroundColor
		{
			get;
		}
	}
}