namespace TextAdventure.WindowsGame
{
	public struct Padding
	{
		public static readonly Padding None = new Padding(0);

		private readonly int _bottom;
		private readonly int _left;
		private readonly int _right;
		private readonly int _top;

		public Padding(int padding)
			: this(padding, padding, padding, padding)
		{
		}

		public Padding(int x, int y)
			: this(x, x, y, y)
		{
		}

		public Padding(int left, int right, int top, int bottom)
		{
			_left = left;
			_right = right;
			_top = top;
			_bottom = bottom;
		}

		public int Left
		{
			get
			{
				return _left;
			}
		}

		public int Right
		{
			get
			{
				return _right;
			}
		}

		public int Top
		{
			get
			{
				return _top;
			}
		}

		public int Bottom
		{
			get
			{
				return _bottom;
			}
		}

		public int X
		{
			get
			{
				return _left + _right;
			}
		}

		public int Y
		{
			get
			{
				return _top + _bottom;
			}
		}
	}
}