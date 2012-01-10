using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame
{
	public class ScissoringRasterizerState : RasterizerState
	{
		public ScissoringRasterizerState()
		{
			CullMode = CullMode.None;
			ScissorTestEnable = true;
		}
	}
}