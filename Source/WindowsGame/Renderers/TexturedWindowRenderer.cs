using System;

using Junior.Common;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

using TextAdventure.Xna;
using TextAdventure.Xna.Contexts;

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

		protected override void RenderBackground(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			WindowTexture windowTexture = _getWindowTextureDelegate(parameters.TextureContent);

			if (windowTexture == null)
			{
				throw new Exception("Must specify a valid window texture.");
			}

			RenderBackground(parameters, windowTexture);
		}

		protected override void RenderBorder(RendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");

			WindowTexture windowTexture = _getWindowTextureDelegate(parameters.TextureContent);

			if (windowTexture == null)
			{
				throw new Exception("Must specify a valid window texture.");
			}

			RenderBorder(parameters, windowTexture);
		}

		private void RenderBackground(RendererParameters parameters, WindowTexture windowTexture)
		{
			Color backgroundColor = BackgroundColor * Alpha;
			RasterizerState rasterizerState = ScissorRectangleContext.Current != null ? new ScissoringRasterizerState() : RasterizerState.CullNone;
			SpriteBatchContext spriteBatchContext = SpriteBatchContext.Current ?? SpriteBatchContext.Default;
			Vector2 origin = spriteBatchContext.Origin;

			parameters.SpriteBatch.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				SamplerState.PointWrap,
				DepthStencilState.None,
				rasterizerState,
				spriteBatchContext.Effect,
				spriteBatchContext.TransformMatrix);

			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopLeftCornerRectangle, windowTexture.BackgroundTopLeftRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRightCornerRectangle, windowTexture.BackgroundTopRightRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomLeftCornerRectangle, windowTexture.BackgroundBottomLeftRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRightCornerRectangle, windowTexture.BackgroundBottomRightRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.LeftRectangle, windowTexture.BackgroundLeftRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.RightRectangle, windowTexture.BackgroundRightRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRectangle, windowTexture.BackgroundTopRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRectangle, windowTexture.BackgroundBottomRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.CenterRectangle, windowTexture.BackgroundCenterRectangle, backgroundColor, 0f, origin, SpriteEffects.None, 0f);

			parameters.SpriteBatch.End();
		}

		private void RenderBorder(RendererParameters parameters, WindowTexture windowTexture)
		{
			Color borderColor = BorderColor * Alpha;
			RasterizerState rasterizerState = ScissorRectangleContext.Current != null ? new ScissoringRasterizerState() : RasterizerState.CullNone;
			SpriteBatchContext spriteBatchContext = SpriteBatchContext.Current ?? SpriteBatchContext.Default;
			Vector2 origin = spriteBatchContext.Origin;

			parameters.SpriteBatch.Begin(
				SpriteSortMode.Deferred,
				BlendState.AlphaBlend,
				SamplerState.PointClamp,
				DepthStencilState.None,
				rasterizerState,
				spriteBatchContext.Effect,
				spriteBatchContext.TransformMatrix);

			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopLeftCornerRectangle, windowTexture.BorderTopLeftRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRightCornerRectangle, windowTexture.BorderTopRightRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomLeftCornerRectangle, windowTexture.BorderBottomLeftRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRightCornerRectangle, windowTexture.BorderBottomRightRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.LeftRectangle, windowTexture.BorderTextureLeftRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.RightRectangle, windowTexture.BorderRightRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.TopRectangle, windowTexture.BorderTopRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);
			parameters.SpriteBatch.Draw(windowTexture.Texture, Window.BottomRectangle, windowTexture.BorderBottomRectangle, borderColor, 0f, origin, SpriteEffects.None, 0f);

			parameters.SpriteBatch.End();
		}
	}
}