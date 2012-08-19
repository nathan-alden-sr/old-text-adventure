using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Renderers
{
	public class MessageTextWord
	{
		private readonly bool _prependSpace;
		private readonly Vector2 _size;

		public MessageTextWord(string text, Vector2 size, bool prependSpace)
		{
			Text = text;
			_size = size;
			_prependSpace = prependSpace;
		}

		public string Text
		{
			get;
			set;
		}

		public Vector2 Size
		{
			get
			{
				return _size;
			}
		}

		public bool PrependSpace
		{
			get
			{
				return _prependSpace;
			}
		}
	}
}