using TextAdventure.Engine.Common;

namespace TextAdventure.Engine.Objects
{
	public interface ICharacter
	{
		byte Symbol
		{
			get;
		}
		Color ForegroundColor
		{
			get;
		}
		Color BackgroundColor
		{
			get;
		}
	}
}