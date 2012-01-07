using Microsoft.Xna.Framework;

using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Components
{
	public abstract class WindowComponentBase : TextAdventureDrawableGameComponent
	{
		private float _alpha = 1f;

		protected WindowComponentBase(GameManager gameManager)
			: base(gameManager)
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
	}
}