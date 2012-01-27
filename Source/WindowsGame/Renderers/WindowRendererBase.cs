using Microsoft.Xna.Framework;

namespace TextAdventure.WindowsGame.Renderers
{
	public abstract class WindowRendererBase : IRenderer
	{
		private float _alpha = 1f;

		protected WindowRendererBase()
		{
			BackgroundColor = Color.Transparent;
		}

		protected float Alpha
		{
			get
			{
				return _alpha;
			}
			set
			{
				_alpha = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		protected Color BackgroundColor
		{
			get;
			set;
		}

		public abstract void Render(RendererParameters parameters);
	}
}