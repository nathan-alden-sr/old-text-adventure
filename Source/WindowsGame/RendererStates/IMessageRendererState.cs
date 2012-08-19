using TextAdventure.Engine.Objects;
using TextAdventure.WindowsGame.Managers;

namespace TextAdventure.WindowsGame.RendererStates
{
	public interface IMessageRendererState
	{
		IMessage Message
		{
			get;
		}
		MessageAnswerSelectionManager AnswerSelectionManager
		{
			get;
			set;
		}
		float Alpha
		{
			get;
		}
		float Scale
		{
			get;
		}
		float ScrollPosition
		{
			get;
		}
		float MaximumScrollPosition
		{
			get;
			set;
		}
		int VisibleHeight
		{
			get;
			set;
		}
	}
}