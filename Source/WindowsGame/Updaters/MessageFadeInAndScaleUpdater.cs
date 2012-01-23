using System;

using Junior.Common;

using TextAdventure.WindowsGame.Helpers;
using TextAdventure.WindowsGame.RendererStates;
using TextAdventure.Xna;

namespace TextAdventure.WindowsGame.Updaters
{
	public class MessageFadeInAndScaleUpdater : IUpdater
	{
		private readonly Action<IXnaGameTime> _completeDelegate;
		private readonly MessageRendererState _messageRendererState;
		private readonly TimedLerpHelper _timedLerpHelper;

		public MessageFadeInAndScaleUpdater(MessageRendererState messageRendererState, TimeSpan totalTime, Action<IXnaGameTime> completeDelegate)
		{
			messageRendererState.ThrowIfNull("messageRendererState");
			completeDelegate.ThrowIfNull("completeDelegate");
			if (totalTime < TimeSpan.Zero)
			{
				throw new ArgumentOutOfRangeException("totalTime");
			}

			_messageRendererState = messageRendererState;
			_completeDelegate = completeDelegate;
			_timedLerpHelper = new TimedLerpHelper(totalTime, TextAdventure.Xna.Constants.MessageRenderer.FadeInDuration, 0f, 1f);
		}

		public void Update(IUpdaterParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			_timedLerpHelper.Update(parameters.GameTime.TotalGameTime);

			_messageRendererState.Alpha = _timedLerpHelper.Value;
			_messageRendererState.Scale = _timedLerpHelper.Value;

			if (new FloatToInt(_timedLerpHelper.Value) == new FloatToInt(1f))
			{
				_completeDelegate(parameters.GameTime);
			}
		}
	}
}