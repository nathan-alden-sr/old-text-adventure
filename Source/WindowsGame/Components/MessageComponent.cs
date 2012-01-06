using System;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Extensions;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.Components
{
	public class MessageComponent : BorderedWindowComponent
	{
		private const Keys AcceptKey = Keys.Enter;
		private const Keys NextAnswerKey = Keys.Right;
		private const Keys PreviousAnswerKey = Keys.Left;
		private static readonly TimeSpan _fadeInDuration = TimeSpan.FromMilliseconds(60);
		private static readonly TimeSpan _fadeOutDuration = TimeSpan.FromMilliseconds(30);
		private readonly KeyboardStateHelper _keyboardStateHelper;
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
			_keyboardStateHelper = new KeyboardStateHelper(KeyDown, null, null, AcceptKey, NextAnswerKey, PreviousAnswerKey);
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
			_keyboardStateHelper.Update();
			if (_timerHelper != null)
			{
				_timerHelper.Update(gameTime.TotalGameTime);
			}

			if (MessageAvailable)
			{
				OpenMessage(gameTime);
			}
			if (ClosingStarted)
			{
				TimeSpan fadeOutDuration = TimeSpan.FromMilliseconds(_fadeInHelper.Alpha * _fadeOutDuration.TotalMilliseconds);

				_fadeOutHelper = new FadeHelper(gameTime.TotalGameTime, fadeOutDuration, _fadeInHelper.Alpha, 0f);
				_fadeInHelper = null;
				_timerHelper = new TimerHelper(fadeOutDuration, gameTime.TotalGameTime, MessageClosed);
			}
			UpdateAlpha(gameTime);

			Visible = _visible;

			base.Update(gameTime);
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

			float maximumClientWidth = Game.Window.ClientBounds.Width * 0.75f;
			float maximumClientHeight = Game.Window.ClientBounds.Width * 0.75f;

			_messageTextComponent = new MessageTextComponent(GameManager, message, maximumClientWidth);

			int clientWidth = _messageTextComponent.MaximumLineWidthAfterFormatting.Round();
			int clientHeight = Math.Min(maximumClientHeight, _messageTextComponent.TotalHeightAfterFormatting).Round();

			SetWindowRectangle(Alignment.Center, clientWidth, clientHeight);
			_messageTextComponent.SetWindowRectangle(Window.AbsoluteClientRectangle, Padding.None);

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