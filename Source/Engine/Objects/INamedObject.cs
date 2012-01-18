namespace TextAdventure.Engine.Objects
{
	public interface INamedObject : IUnique
	{
		string Name
		{
			get;
		}
	}
}