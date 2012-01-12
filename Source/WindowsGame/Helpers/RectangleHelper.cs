using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Extensions;

namespace TextAdventure.WindowsGame.Helpers
{
	public class RectangleHelper
	{
		public static readonly RectangleHelper Instance = new RectangleHelper();

		private RectangleHelper()
		{
		}

		public Rectangle Lerp(Rectangle rectangle1, Rectangle rectangle2, float amount)
		{
			return new Rectangle(
				MathHelper.Lerp(rectangle1.X, rectangle2.X, amount).Round(),
				MathHelper.Lerp(rectangle1.Y, rectangle2.Y, amount).Round(),
				MathHelper.Lerp(rectangle1.Width, rectangle2.Width, amount).Round(),
				MathHelper.Lerp(rectangle1.Height, rectangle2.Height, amount).Round());
		}
	}
}