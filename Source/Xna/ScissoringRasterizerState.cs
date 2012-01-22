using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.Xna
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