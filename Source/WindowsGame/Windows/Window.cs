using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Windows
{
	public class Window
	{
		private readonly Rectangle _windowRectangle;

		public Window(Rectangle windowRectangle)
		{
			_windowRectangle = windowRectangle;
		}

		public Rectangle WindowRectangle
		{
			get
			{
				return _windowRectangle;
			}
		}
	}
}