using System;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.RendererStates
{
	public class MessageRendererState : IMessageRendererState
	{
		private float _alpha;
		private float _maximumScrollPosition;
		private float _scale;
		private float _scrollPosition;
		private int _visibleHeight;

		public IMessage Message
		{
			get;
			set;
		}

		public MessageAnswerSelectionManager AnswerSelectionManager
		{
			get;
			set;
		}

		public float Alpha
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

		public float Scale
		{
			get
			{
				return _scale;
			}
			set
			{
				_scale = MathHelper.Clamp(value, 0f, 1f);
			}
		}

		public float ScrollPosition
		{
			get
			{
				return _scrollPosition;
			}
			set
			{
				_scrollPosition = MathHelper.Clamp(value, 0f, _maximumScrollPosition);
			}
		}

		public float MaximumScrollPosition
		{
			get
			{
				return _maximumScrollPosition;
			}
			set
			{
				_maximumScrollPosition = Math.Max(value, _scrollPosition);
			}
		}

		public int VisibleHeight
		{
			get
			{
				return _visibleHeight;
			}
			set
			{
				_visibleHeight = Math.Max(0, value);
			}
		}
	}
}