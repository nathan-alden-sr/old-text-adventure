using System.Drawing;
using System.Windows.Forms;

namespace TextAdventure.Editor.Tools
{
	public partial class ToolUserControl : UserControl
	{
		public ToolUserControl()
		{
			InitializeComponent();
		}

		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 200);
			}
		}
	}
}