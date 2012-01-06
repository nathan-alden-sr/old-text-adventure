using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface IMessageColor : IMessagePart
	{
		Color Color
		{
			get;
		}
	}
}