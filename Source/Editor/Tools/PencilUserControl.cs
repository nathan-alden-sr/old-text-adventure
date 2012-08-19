using System;
using System.Drawing;

using Junior.Common;

using TextAdventure.Editor.Extensions;
using TextAdventure.Editor.RendererStates;
using TextAdventure.Engine.Objects;

namespace TextAdventure.Editor.Tools
{
	public partial class PencilUserControl : ToolUserControl
	{
		private readonly PencilRendererState _pencilRendererState;

		public PencilUserControl(PencilRendererState pencilRendererState)
		{
			pencilRendererState.ThrowIfNull("pencilRendererState");

			_pencilRendererState = pencilRendererState;

			InitializeComponent();
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 264);
			}
		}

		private void SetCharacter()
		{
			_pencilRendererState.Character = new Character(
				characterSelectionUserControl.Symbol,
				characterSelectionUserControl.SymbolForegroundColor.ToEngineColor(),
				characterSelectionUserControl.SymbolBackgroundColor.ToEngineColor());
		}

		private void CharacterSelectionUserControlOnSymbolChanged(object sender, EventArgs e)
		{
			SetCharacter();
		}

		private void CharacterSelectionUserControlOnSymbolForegroundColorChanged(object sender, EventArgs e)
		{
			SetCharacter();
		}

		private void CharacterSelectionUserControlOnSymbolBackgroundColorChanged(object sender, EventArgs e)
		{
			SetCharacter();
		}

		private void SizeUserControlOnSelectedSizeChanged(object sender, EventArgs e)
		{
			_pencilRendererState.SelectionSize = sizeUserControl.SelectedSize;
		}
	}
}