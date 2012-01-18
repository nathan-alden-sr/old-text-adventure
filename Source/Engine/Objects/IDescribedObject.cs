namespace TextAdventure.Engine.Objects
{
	public interface IDescribedObject : IUnique
	{
		string Description
		{
			get;
		}
	}
}