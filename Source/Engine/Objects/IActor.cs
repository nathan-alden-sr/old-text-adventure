namespace TextAdventure.Engine.Objects
{
	public interface IActor : IUnique
	{
		Character Character
		{
			get;
		}
	}
}