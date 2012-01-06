namespace TextAdventure.Engine.Game.Events
{
	public interface IEventContext : IContext
	{
		bool Cancel
		{
			get;
			set;
		}
	}
}