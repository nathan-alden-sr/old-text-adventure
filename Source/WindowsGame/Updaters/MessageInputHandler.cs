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
using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MessageInputHandler : IUpdater
	{
		private readonly KeyboardStateHelper _answerKeyboardStateHelper;
		private readonly Action<IXnaGameTime> _messageClosingDelegate;
		private readonly MessageRendererState _messageRendererState;
		private readonly KeyboardRepeatHelper _scrollKeyboardRepeatHelper = new KeyboardRepeatHelper();
		private readonly KeyboardStateHelper _scrollKeyboardStateHelper;
		private readonly WorldInstance _worldInstance;
		private IXnaGameTime _lastGameTime;

		public MessageInputHandler(WorldInstance worldInstance, MessageRendererState messageRendererState, TimeSpan totalTime, Action<IXnaGameTime> messageClosingDelegate)
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
				TextAdventure.Xna.Constants.MessageRenderer.Input.AcceptKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.NextAnswerKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.PreviousAnswerKey);
			_scrollKeyboardStateHelper = new KeyboardStateHelper(
				_scrollKeyboardRepeatHelper,
				TextAdventure.Xna.Constants.MessageRenderer.Input.ScrollUpKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.ScrollDownKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.HomeKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.EndKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.PageUpKey,
				TextAdventure.Xna.Constants.MessageRenderer.Input.PageDownKey);
			_scrollKeyboardRepeatHelper.InitialInterval = TextAdventure.Xna.Constants.MessageRenderer.Input.ScrollKeyboardInterval;
			_scrollKeyboardRepeatHelper.RepeatingInterval = TextAdventure.Xna.Constants.MessageRenderer.Input.ScrollKeyboardInterval;
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
				case TextAdventure.Xna.Constants.MessageRenderer.Input.ScrollUpKey:
					_messageRendererState.ScrollPosition -= TextAdventure.Xna.Constants.MessageRenderer.ScrollStep;
					break;
				case TextAdventure.Xna.Constants.MessageRenderer.Input.ScrollDownKey:
					_messageRendererState.ScrollPosition += TextAdventure.Xna.Constants.MessageRenderer.ScrollStep;
					break;
				case TextAdventure.Xna.Constants.MessageRenderer.Input.HomeKey:
					_messageRendererState.ScrollPosition = 0f;
					break;
				case TextAdventure.Xna.Constants.MessageRenderer.Input.EndKey:
					_messageRendererState.ScrollPosition = Single.MaxValue;
					break;
				case TextAdventure.Xna.Constants.MessageRenderer.Input.PageUpKey:
					_messageRendererState.ScrollPosition -= _messageRendererState.VisibleHeight;
					break;
				case TextAdventure.Xna.Constants.MessageRenderer.Input.PageDownKey:
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
					case TextAdventure.Xna.Constants.MessageRenderer.Input.AcceptKey:
						MessageAnswer selectedAnswer = _messageRendererState.AnswerSelectionManager.SelectedAnswer;

						_worldInstance.RaiseEvent(selectedAnswer.MessageAnswerSelectedEventHandler, new MessageAnswerSelectedEvent(selectedAnswer));
						if (selectedAnswer.Parts.Any())
						{
							_worldInstance.MessageQueue.EnqueueMessage(selectedAnswer, MessageQueuePosition.First);
						}
						break;
					case TextAdventure.Xna.Constants.MessageRenderer.Input.NextAnswerKey:
						_messageRendererState.AnswerSelectionManager.SelectNextAnswer();
						break;
					case TextAdventure.Xna.Constants.MessageRenderer.Input.PreviousAnswerKey:
						_messageRendererState.AnswerSelectionManager.SelectPreviousAnswer();
						break;
				}
			}
			if (keys == TextAdventure.Xna.Constants.MessageRenderer.Input.AcceptKey)
			{
				_messageClosingDelegate(_lastGameTime);
			}
		}
	}
}