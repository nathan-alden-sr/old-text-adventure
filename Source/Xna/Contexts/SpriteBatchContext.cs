using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna.Contexts
{
	public class SpriteBatchContext : Context<SpriteBatchContext>
	{
		public static readonly SpriteBatchContext Default = new SpriteBatchContext();
		private readonly Effect _effect;
		private readonly Vector2 _origin;
		private readonly Matrix _transformMatrix;

		public SpriteBatchContext()
			: this(Vector2.Zero, null, Matrix.Identity)
		{
		}

		public SpriteBatchContext(Vector2 origin)
			: this(origin, null, Matrix.Identity)
		{
		}

		public SpriteBatchContext(Vector2 origin, Effect effect)
			: this(origin, effect, Matrix.Identity)
		{
		}

		public SpriteBatchContext(Effect effect)
			: this(Vector2.Zero, effect, Matrix.Identity)
		{
			_effect = effect;
		}

		public SpriteBatchContext(Effect effect, Matrix transformMatrix)
			: this(Vector2.Zero, effect, transformMatrix)
		{
			_effect = effect;
			_transformMatrix = transformMatrix;
		}

		public SpriteBatchContext(Matrix transformMatrix)
			: this(Vector2.Zero, null, transformMatrix)
		{
			_transformMatrix = transformMatrix;
		}

		public SpriteBatchContext(Vector2 origin, Effect effect, Matrix transformMatrix)
		{
			_origin = origin;
			_effect = effect;
			_transformMatrix = transformMatrix;
		}

		public Effect Effect
		{
			get
			{
				return _effect;
			}
		}

		public Vector2 Origin
		{
			get
			{
				return _origin;
			}
		}

		public Matrix TransformMatrix
		{
			get
			{
				return _transformMatrix;
			}
		}
	}
}