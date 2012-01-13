using System;
using System.Linq;

using Junior.Common;

using Microsoft.Xna.Framework.Input;

using TextAdventure.Engine.Game.Events;
using TextAdventure.Engine.Game.Messages;
using TextAdventure.Engine.Game.World;
using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.WindowsGame.Xna;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MessageInputHandler : IUpdater
	{
		private readonly KeyboardStateHelper _answerKeyboardStateHelper;
		private readonly Action<XnaGameTime> _messageClosingDelegate;
		private readonly MessageRendererState _messageRendererState;
		private readonly KeyboardRepeatHelper _scrollKeyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _scrollKeyboardStateHelper;
		private readonly WorldInstance _worldInstance;
		private XnaGameTime _lastGameTime;

		public MessageInputHandler(WorldInstance worldInstance, MessageRendererState messageRendererState, TimeSpan totalTime, Action<XnaGameTime> messageClosingDelegate)
		{
			worldInstance.ThrowIfNull("worldInstance");
			messageRendererState.ThrowIfNull("messageRendererState");
			messageClosingDelegate.ThrowIfNull("messageClosingDelegate");

			_worldInstance = worldInstance;
			_messageRendererState = messageRendererState;
			_messageClosingDelegate = messageClosingDelegate;
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
		}

		public void Update(IUpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			_lastGameTime = parameters.GameTime;

			if (parameters.Focus != Focus.Message)
			{
				return;
			}

			_answerKeyboardStateHelper.Update();
			_scrollKeyboardStateHelper.Update();

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

		private void KeyDown(KeyboardState keyboardState, Keys keys)
		{
			MessageAnswer[] answers = _messageRendererState.AnswerSelectionManager.IfNotNull(arg => arg.Answers.ToArray()) ?? new MessageAnswer[0];

			if (answers.Length > 0)
			{
				switch (keys)
				{
					case Constants.MessageRenderer.Input.AcceptKey:
						MessageAnswer selectedAnswer = _messageRendererState.AnswerSelectionManager.SelectedAnswer;

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
				_messageClosingDelegate(_lastGameTime);
			}
		}
	}
}