using System;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;
using TextAdventure.WindowsGame.Windows;

namespace TextAdventure.WindowsGame.Components
{
	public class MessageComponent : TexturedWindowComponent
	{
		private const Keys AcceptKey = Keys.Enter;
		private const Keys NextAnswerKey = Keys.Right;
		private const Keys PreviousAnswerKey = Keys.Left;
		private const Keys ScrollDown = Keys.Down;
		private const int ScrollIntervalInMilliseconds = 0;
		private const int ScrollStep = 5;
		private const Keys ScrollUp = Keys.Up;
		private const Keys Home = Keys.Home;
		private const Keys End = Keys.End;
		private const Keys PageUp = Keys.PageUp;
		private const Keys PageDown = Keys.PageDown;
		private const float MaximumLineWidthFactor = 0.75f;
		private const int VerticalOffset = 30;
		private const int VerticalPadding = 10;
		private const int ArrowPadding = 2;
		private const float DisabledArrowAlpha = 0.5f;
		private static readonly TimeSpan _fadeInDuration = TimeSpan.FromMilliseconds(60);
		private static readonly TimeSpan _fadeOutDuration = TimeSpan.FromMilliseconds(30);
		private readonly KeyboardStateHelper _answerKeyboardStateHelper;
		private readonly KeyboardRepeatHelper _scrollKeyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _scrollKeyboardStateHelper;
		private readonly IWorldInstance _worldInstance;
		private bool _closing;
		private FadeHelper _fadeInHelper;
		private FadeHelper _fadeOutHelper;
		private MessageTextComponent _messageTextComponent;
		private TimerHelper _timerHelper;
		private bool _visible;

		public MessageComponent(GameManager gameManager, IWorldInstance worldInstance)
			: base(gameManager, textureContent => textureContent.Windows.InnerBevel1)
		{
			worldInstance.ThrowIfNull("worldInstance");

			_worldInstance = worldInstance;
			_answerKeyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, AcceptKey, NextAnswerKey, PreviousAnswerKey);
			_scrollKeyboardStateHelper = new KeyboardStateHelper(_scrollKeyboardRepeatHelper, ScrollUp, ScrollDown, Home, End, PageUp, PageDown);
			_scrollKeyboardRepeatHelper.InitialInterval = TimeSpan.FromMilliseconds(ScrollIntervalInMilliseconds);
			_scrollKeyboardRepeatHelper.RepeatingInterval = TimeSpan.FromMilliseconds(ScrollIntervalInMilliseconds);
			_visible = false;

			DrawOrder = ComponentDrawOrder.Message;
		}

		private bool MessageAvailable
		{
			get
			{
				return _messageTextComponent == null && !_worldInstance.WorldTime.Paused && _worldInstance.MessageQueue.Count > 0;
			}
		}

		private bool ClosingStarted
		{
			get
			{
				return _closing && _timerHelper == null;
			}
		}

		public override void Update(GameTime gameTime)
		{
			if (MessageAvailable)
			{
				OpenMessage(gameTime);
			}

			if (_visible)
			{
				_answerKeyboardStateHelper.Update();
				_scrollKeyboardStateHelper.Update();
				if (_timerHelper != null)
				{
					_timerHelper.Update(gameTime.TotalGameTime);
				}

				if (ClosingStarted)
				{
					TimeSpan fadeOutDuration = TimeSpan.FromMilliseconds(_fadeInHelper.Alpha * _fadeOutDuration.TotalMilliseconds);

					_fadeOutHelper = new FadeHelper(gameTime.TotalGameTime, fadeOutDuration, _fadeInHelper.Alpha, 0f);
					_fadeInHelper = null;
					_timerHelper = new TimerHelper(fadeOutDuration, gameTime.TotalGameTime, MessageClosed);
				}
				else if (_scrollKeyboardRepeatHelper.UpdateRequired(gameTime))
				{
					switch (_scrollKeyboardStateHelper.LastKeyDown)
					{
						case ScrollUp:
							_messageTextComponent.ScrollPosition -= ScrollStep;
							break;
						case ScrollDown:
							_messageTextComponent.ScrollPosition += ScrollStep;
							break;
						case Home:
							_messageTextComponent.ScrollPosition = 0f;
							break;
						case End:
							_messageTextComponent.ScrollPosition = Single.MaxValue;
							break;
						case PageUp:
							_messageTextComponent.ScrollPosition -= _messageTextComponent.VisibleHeight;
							break;
						case PageDown:
							_messageTextComponent.ScrollPosition += _messageTextComponent.VisibleHeight;
							break;
					}
				}
				UpdateAlpha(gameTime);
			}

			Visible = _visible;

			base.Update(gameTime);
		}

		public override void Draw(GameTime gameTime)
		{
			base.Draw(gameTime);

			if (_messageTextComponent == null || (new FloatToInt(_messageTextComponent.ScrollPosition) == 0 && new FloatToInt(_messageTextComponent.MaximumScrollPosition) == 0))
			{
				return;
			}

			SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, RasterizerState.CullNone, null);

			int x = Window.AbsoluteClientRectangle.Right - TextureContent.Windows.InnerBevel1.SpriteWidth;
			var scrollPosition = new FloatToInt(_messageTextComponent.ScrollPosition);

			SpriteBatch.Draw(
				TextureContent.Windows.InnerBevel1.Texture,
				new Vector2(x, Window.AbsoluteClientRectangle.Y),
				TextureContent.Windows.InnerBevel1.UpArrowRectangle,
				scrollPosition == 0 ? Color.White * DisabledArrowAlpha : Color.White);
			SpriteBatch.Draw(
				TextureContent.Windows.InnerBevel1.Texture,
				new Vector2(x, Window.AbsoluteClientRectangle.Bottom - TextureContent.Windows.InnerBevel1.SpriteHeight),
				TextureContent.Windows.InnerBevel1.DownArrowRectangle,
				scrollPosition.FloatValue >= _messageTextComponent.MaximumScrollPosition ? Color.White * DisabledArrowAlpha : Color.White);

			SpriteBatch.End();
		}

		private void OpenMessage(GameTime gameTime)
		{
			_fadeInHelper = new FadeHelper(gameTime.TotalGameTime, _fadeInDuration, 0f, 1f);
			_visible = true;

			IMessage message = _worldInstance.MessageQueue.DequeueMessage();
			var messageWithBackgroundColor = message as IMessageWithBackgroundColor;

			InputFocusManager.ClaimFocus(this);
			if (messageWithBackgroundColor != null)
			{
				BackgroundColor = messageWithBackgroundColor.BackgroundColor.ToXnaColor();
			}

			Rectangle destinationRectangle = DrawingConstants.GameWindow.DestinationRectangle;
			float maximumLineWidth = destinationRectangle.Width * MaximumLineWidthFactor;
			float maximumClientHeight = destinationRectangle.Height - (destinationRectangle.Center.Y + VerticalOffset) - (VerticalPadding * 2) - (TextureContent.Windows.InnerBevel1.SpriteHeight * 2);

			_messageTextComponent = new MessageTextComponent(GameManager, message, maximumLineWidth);

			int padding = ArrowPadding + TextureContent.Windows.InnerBevel1.SpriteWidth;
			int clientWidth = _messageTextComponent.MaximumLineWidthAfterFormatting.Round() + (padding * 2);
			int clientHeight = Math.Min(maximumClientHeight, _messageTextComponent.TotalHeightAfterFormatting).Round();

			SetWindowRectangleUsingWindowYAndClientSize(WindowHorizontalAlignment.Center, destinationRectangle.Center.Y + 30, clientWidth, clientHeight);

			Rectangle messageTextComponentWindowRectangle = Window.AbsoluteClientRectangle;

			messageTextComponentWindowRectangle.Inflate(-padding, 0);

			_messageTextComponent.SetWindowRectangle(messageTextComponentWindowRectangle);

			Game.Components.Add(_messageTextComponent);
		}

		private void UpdateAlpha(GameTime gameTime)
		{
			if (_fadeInHelper != null)
			{
				_fadeInHelper.Update(gameTime.TotalGameTime);
				Alpha = _fadeInHelper.Alpha;
				_messageTextComponent.SetAlpha(_fadeInHelper.Alpha);
			}
			if (_fadeOutHelper != null)
			{
				_fadeOutHelper.Update(gameTime.TotalGameTime);
				Alpha = _fadeOutHelper.Alpha;
				_messageTextComponent.SetAlpha(_fadeOutHelper.Alpha);
			}
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			if (_messageTextComponent == null)
			{
				return;
			}

			IMessageAnswer[] answers = _messageTextComponent.AnswerSelectionManager.IfNotNull(arg => arg.Answers.ToArray()) ?? new IMessageAnswer[0];

			if (answers.Length > 0)
			{
				switch (keys)
				{
					case AcceptKey:
						IMessageAnswer selectedAnswer = _messageTextComponent.AnswerSelectionManager.SelectedAnswer;

						_worldInstance.RaiseEvent(_worldInstance.World.AnswerSelectedEventHandler, new AnswerSelectedEvent(selectedAnswer.Id));
						if (selectedAnswer.Parts.Any())
						{
							_worldInstance.MessageQueue.EnqueueMessage(selectedAnswer, MessageQueuePosition.First);
						}
						break;
					case NextAnswerKey:
						_messageTextComponent.AnswerSelectionManager.SelectNextAnswer();
						break;
					case PreviousAnswerKey:
						_messageTextComponent.AnswerSelectionManager.SelectPreviousAnswer();
						break;
				}
			}
			if (keys == AcceptKey)
			{
				_closing = true;
			}
		}

		private void MessageClosed()
		{
			Game.Components.Remove(_messageTextComponent);
			_messageTextComponent = null;
			_closing = false;
			_visible = false;
			_fadeOutHelper = null;
			_timerHelper = null;
			if (!MessageAvailable)
			{
				InputFocusManager.RelinquishFocus();
			}
		}
	}
}