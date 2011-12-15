using Junior.Common;

namespace TextAdventure.Engine.Game.Events
{
	public abstract class TargetedEvent<TTarget> : Event
		where TTarget : class, IUnique
	{
		private readonly TTarget _target;

		protected TargetedEvent(TTarget target)
		{
			target.ThrowIfNull("target");

			_target = target;
		}

		public TTarget Target
		{
			get
			{
				return _target;
			}
		}
	}
}