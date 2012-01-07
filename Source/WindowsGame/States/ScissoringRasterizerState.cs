using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.States
{
	public class ScissoringRasterizerState : RasterizerState
	{
		public ScissoringRasterizerState(Rectangle scissorRectangle)
		{
			CullMode = CullMode.None;
			ScissorTestEnable = true;
		}
	}
}