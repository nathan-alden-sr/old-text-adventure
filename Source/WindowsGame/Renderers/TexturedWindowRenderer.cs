using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace TextAdventure.WindowsGame.Renderers
{
	public class TexturedWindowRenderer : BorderedWindowRenderer
	{
		private readonly Func<TextureContent, WindowTexture> _getWindowTextureDelegate;

		protected TexturedWindowRenderer(Func<TextureContent, WindowTexture> getWindowTextureDelegate)
		{
			getWindowTextureDelegate.ThrowIfNull("getWindowTextureDelegate");

			_getWindowTextureDelegate = getWindowTextureDelegate;
			BackgroundColor = Color.White;
			BorderColor = Color.White;
		}

		protected override void RenderBackground(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			WindowTexture windowTexture = _getWindowTextureDelegate(parameters.TextureContent);

			if (windowTexture == null)
			{
				throw new Exception("Must specify a valid window texture.");
			}

			RenderBackground(parameters, windowTexture);
		}

		protected override void RenderBorder(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			WindowTexture windowTexture = _getWindowTextureDelegate(parameters.TextureContent);

			if (windowTexture == null)
			{
				throw new Exception("Must specify a valid window texture.");
			}

			RenderBorder(parameters, windowTexture);
		}

		private void RenderBackground(IRendererParameters parameters, WindowTexture windowTexture)
		{
			Color backgroundColor = BackgroundColor * Alpha;
			RasterizerState rasterizerState = parameters.ScissorRectangle != null ? new ScissoringRasterizerState() : RasterizerState.CullNone;

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointWrap, DepthStencilState.None, rasterizerState, null, parameters.TransformMatrix);

			if (parameters.ScissorRectangle != null)
			{
				parameters.SpriteBatch.GraphicsDevice.ScissorRectangle = parameters.ScissorRectangle.Value;
			}

			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopLeftCornerRectangle, windowTexture.BackgroundTopLeftRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRightCornerRectangle, windowTexture.BackgroundTopRightRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomLeftCornerRectangle, windowTexture.BackgroundBottomLeftRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRightCornerRectangle, windowTexture.BackgroundBottomRightRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.LeftRectangle, windowTexture.BackgroundLeftRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.RightRectangle, windowTexture.BackgroundRightRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRectangle, windowTexture.BackgroundTopRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRectangle, windowTexture.BackgroundBottomRectangle, backgroundColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.CenterRectangle, windowTexture.BackgroundCenterRectangle, backgroundColor);

			parameters.SpriteBatch.End();
		}

		private void RenderBorder(IRendererParameters parameters, WindowTexture windowTexture)
		{
			Color borderColor = BorderColor * Alpha;
			RasterizerState rasterizerState = parameters.ScissorRectangle != null ? new ScissoringRasterizerState() : RasterizerState.CullNone;

			parameters.SpriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, SamplerState.PointClamp, DepthStencilState.None, rasterizerState, null, parameters.TransformMatrix);

			if (parameters.ScissorRectangle != null)
			{
				parameters.SpriteBatch.GraphicsDevice.ScissorRectangle = parameters.ScissorRectangle.Value;
			}

			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopLeftCornerRectangle, windowTexture.BorderTopLeftRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRightCornerRectangle, windowTexture.BorderTopRightRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomLeftCornerRectangle, windowTexture.BorderBottomLeftRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRightCornerRectangle, windowTexture.BorderBottomRightRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.LeftRectangle, windowTexture.BorderTextureLeftRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.RightRectangle, windowTexture.BorderRightRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRectangle, windowTexture.BorderTopRectangle, borderColor);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRectangle, windowTexture.BorderBottomRectangle, borderColor);

			parameters.SpriteBatch.End();
		}
	}
}