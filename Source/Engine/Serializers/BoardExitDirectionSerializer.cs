using Junior.Common;

using TextAdventure.Engine.Objects;

namespace TextAdventure.Engine.Serializers
{
	public class BoardExitDirectionSerializer
	{
		public static readonly BoardExitDirectionSerializer Instance = new BoardExitDirectionSerializer();

		private BoardExitDirectionSerializer()
		{
		}

		public string Serialize(BoardExitDirection boardExitDirection)
		{
			return boardExitDirection.ToString();
		}

		public BoardExitDirection Deserialize(string value)
		{
			return Enum<BoardExitDirection>.Parse(value);
		}
	}
}