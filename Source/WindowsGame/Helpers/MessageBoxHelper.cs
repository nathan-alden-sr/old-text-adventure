using System.Windows.Forms;

namespace TextAdventure.WindowsGame.Helpers
{
	public class MessageBoxHelper
	{
		public static readonly MessageBoxHelper Instance = new MessageBoxHelper();

		private MessageBoxHelper()
		{
		}

		public void ShowError(IWin32Window owner, string text, string caption = "Text Adventure")
		{
			MessageBox.Show(owner, text, caption, MessageBoxButtons.OK, MessageBoxIcon.Error);
		}
	}
}