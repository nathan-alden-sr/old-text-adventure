using System;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.InputHandlers
{
	public class MessageInputHandler : IInputHandler
	{
		private readonly KeyboardStateHelper _answerKeyboardStateHelper;
		private readonly Action _messageClosedDelegate;
		private readonly MessageRendererState _messageRendererState;
		private readonly KeyboardRepeatHelper _scrollKeyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _scrollKeyboardStateHelper;
		private readonly IWorldInstance _worldInstance;
		private bool _closing;
		private FadeHelper _fadeInHelper;
		private FadeHelper _fadeOutHelper;
		private TimerHelper _timerHelper;

		public MessageInputHandler(IWorldInstance worldInstance, MessageRendererState messageRendererState, TimeSpan totalGameTime, Action messageClosedDelegate)
		{
			worldInstance.ThrowIfNull("worldInstance");
			messageRendererState.ThrowIfNull("messageRendererState");
			messageClosedDelegate.ThrowIfNull("messageClosedDelegate");

			_worldInstance = worldInstance;
			_messageRendererState = messageRendererState;
			_messageClosedDelegate = messageClosedDelegate;
			_answerKeyboardStateHelper = new KeyboardStateHelper(
				KeyDown,
				null,
				null,
				Constants.MessageRenderer.Input.AcceptKey,
				Constants.MessageRenderer.Input.NextAnswerKey,
				Constants.MessageRenderer.Input.PreviousAnswerKey);
			_scrollKeyboardStateHelper = new KeyboardStateHelper(
				_scrollKeyboardRepeatHelper,
				Constants.MessageRenderer.Input.ScrollUpKey,
				Constants.MessageRenderer.Input.ScrollDownKey,
				Constants.MessageRenderer.Input.HomeKey,
				Constants.MessageRenderer.Input.EndKey,
				Constants.MessageRenderer.Input.PageUpKey,
				Constants.MessageRenderer.Input.PageDownKey);
			_scrollKeyboardRepeatHelper.InitialInterval = Constants.MessageRenderer.Input.ScrollKeyboardInterval;
			_scrollKeyboardRepeatHelper.RepeatingInterval = Constants.MessageRenderer.Input.ScrollKeyboardInterval;
			_fadeInHelper = new FadeHelper(totalGameTime, Constants.MessageRenderer.FadeInDuration, 0f, 1f);
		}

		private bool Opening
		{
			get
			{
				return _fadeInHelper != null && _fadeInHelper.Alpha < 1f;
			}
		}

		private bool Closing
		{
			get
			{
				return _fadeOutHelper != null;
			}
		}

		private bool ClosingStarted
		{
			get
			{
				return _closing && _timerHelper == null;
			}
		}

		public void Update(GameTime gameTime, Focus focus)
		{
			if (focus != Focus.Message)
			{
				return;
			}

			_answerKeyboardStateHelper.Update();
			_scrollKeyboardStateHelper.Update();
			UpdateAlpha(gameTime);
			if (_timerHelper != null)
			{
				_timerHelper.Update(gameTime.TotalGameTime);
			}

			if (ClosingStarted)
			{
				TimeSpan fadeOutDuration = TimeSpan.FromMilliseconds(_fadeInHelper.Alpha * Constants.MessageRenderer.FadeOutDuration.TotalMilliseconds);

				_fadeOutHelper = new FadeHelper(gameTime.TotalGameTime, fadeOutDuration, _fadeInHelper.Alpha, 0f);
				_fadeInHelper = null;
				_timerHelper = new TimerHelper(fadeOutDuration, gameTime.TotalGameTime, _messageClosedDelegate);
			}
			if (!Opening && !Closing && _scrollKeyboardRepeatHelper.IntervalElapsed(gameTime.TotalGameTime))
			{
				switch (_scrollKeyboardStateHelper.LastKeyDown)
				{
					case Constants.MessageRenderer.Input.ScrollUpKey:
						_messageRendererState.ScrollPosition -= Constants.MessageRenderer.ScrollStep;
						break;
					case Constants.MessageRenderer.Input.ScrollDownKey:
						_messageRendererState.ScrollPosition += Constants.MessageRenderer.ScrollStep;
						break;
					case Constants.MessageRenderer.Input.HomeKey:
						_messageRendererState.ScrollPosition = 0f;
						break;
					case Constants.MessageRenderer.Input.EndKey:
						_messageRendererState.ScrollPosition = Single.MaxValue;
						break;
					case Constants.MessageRenderer.Input.PageUpKey:
						_messageRendererState.ScrollPosition -= _messageRendererState.VisibleHeight;
						break;
					case Constants.MessageRenderer.Input.PageDownKey:
						_messageRendererState.ScrollPosition += _messageRendererState.VisibleHeight;
						break;
				}
			}
		}

		private void UpdateAlpha(GameTime gameTime)
		{
			if (_fadeInHelper != null)
			{
				_fadeInHelper.Update(gameTime.TotalGameTime);
				_messageRendererState.Alpha = _fadeInHelper.Alpha;
			}
			if (_fadeOutHelper != null)
			{
				_fadeOutHelper.Update(gameTime.TotalGameTime);
				_messageRendererState.Alpha = _fadeOutHelper.Alpha;
			}
		}

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			if (Opening || Closing)
			{
				return;
			}

			IMessageAnswer[] answers = _messageRendererState.AnswerSelectionManager.IfNotNull(arg => arg.Answers.ToArray()) ?? new IMessageAnswer[0];

			if (answers.Length > 0)
			{
				switch (keys)
				{
					case Constants.MessageRenderer.Input.AcceptKey:
						IMessageAnswer selectedAnswer = _messageRendererState.AnswerSelectionManager.SelectedAnswer;

						_worldInstance.RaiseEvent(_worldInstance.World.AnswerSelectedEventHandler, new AnswerSelectedEvent(selectedAnswer.Id));
						if (selectedAnswer.Parts.Any())
						{
							_worldInstance.MessageQueue.EnqueueMessage(selectedAnswer, MessageQueuePosition.First);
						}
						break;
					case Constants.MessageRenderer.Input.NextAnswerKey:
						_messageRendererState.AnswerSelectionManager.SelectNextAnswer();
						break;
					case Constants.MessageRenderer.Input.PreviousAnswerKey:
						_messageRendererState.AnswerSelectionManager.SelectPreviousAnswer();
						break;
				}
			}
			if (keys == Constants.MessageRenderer.Input.AcceptKey)
			{
				_closing = true;
			}
		}
	}
}