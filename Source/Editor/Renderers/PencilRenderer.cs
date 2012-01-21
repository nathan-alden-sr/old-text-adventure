using Junior.Common;

using TextAdventure.Editor.RendererStates;

namespace TextAdventure.Editor.Renderers
{
	public class PencilRenderer : IRenderer
	{
		private readonly IEditorView _editorView;
		private readonly IPencilRendererState _state;

		public PencilRenderer(IPencilRendererState state, IEditorView editorView)
		{
			state.ThrowIfNull("state");
			editorView.ThrowIfNull("editorView");

			_state = state;
			_editorView = editorView;
		}

		public void Render(IRendererParameters parameters)
		{
			parameters.ThrowIfNull("parameters");
		}
	}
}