using System;

using Junior.Common;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MessageFadeOutAndScaleUpdater : IUpdater
	{
		private readonly Action _completeDelegate;
		private readonly MessageRendererState _messageRendererState;
		private readonly TimedLerpHelper _timedLerpHelper;

		public MessageFadeOutAndScaleUpdater(MessageRendererState messageRendererState, TimeSpan totalTime, Action completeDelegate)
		{
			messageRendererState.ThrowIfNull("messageRendererState");
			completeDelegate.ThrowIfNull("completeDelegate");
			if (totalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("totalTime");
			}

			_messageRendererState = messageRendererState;
			_completeDelegate = completeDelegate;
			_timedLerpHelper = new TimedLerpHelper(totalTime, TextAdventure.Xna.Constants.MessageRenderer.FadeOutDuration, 1f, 0f);
		}

		public void Update(UpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			_timedLerpHelper.Update(parameters.GameTime.TotalGameTime);

			_messageRendererState.Alpha = _timedLerpHelper.Value;
			_messageRendererState.Scale = _timedLerpHelper.Value;

			if (new FloatToInt(_timedLerpHelper.Value) == new FloatToInt(0f))
			{
				_completeDelegate();
			}
		}
	}
}