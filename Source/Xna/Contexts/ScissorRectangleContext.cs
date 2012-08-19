using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna.Contexts
{
	public class ScissorRectangleContext : Context<ScissorRectangleContext>
	{
		private readonly GraphicsDevice _graphicsDevice;
		private readonly Rectangle _originalScissorRectangle;

		public ScissorRectangleContext(GraphicsDevice graphicsDevice, Rectangle scissorRectangle)
		{
			graphicsDevice.ThrowIfNull("graphicsDevice");

			_graphicsDevice = graphicsDevice;
			_originalScissorRectangle = graphicsDevice.ScissorRectangle;
			graphicsDevice.ScissorRectangle = scissorRectangle;
		}

		protected override void Dispose(bool disposing)
		{
			_graphicsDevice.ScissorRectangle = _originalScissorRectangle;

			base.Dispose(disposing);
		}
	}
}