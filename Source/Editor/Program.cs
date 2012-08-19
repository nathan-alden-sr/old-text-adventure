using System;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms;

using TextAdventure.Editor.Forms;

namespace TextAdventure.Editor
{
#if WINDOWS
	internal static class Program
	{
		private static readonly HandleRef _handleRef = default(HandleRef);

		[DllImport("user32.dll")]
		[return:MarshalAs(UnmanagedType.Bool)]
		private static extern bool PeekMessage(out NativeMessage lpMsg, HandleRef hWnd, uint wMsgFilterMin, uint wMsgFilterMax, uint wRemoveMsg);

		[STAThread]
		private static void Main()
		{
			Application.EnableVisualStyles();
			Application.SetCompatibleTextRenderingDefault(true);

			var gameForm = new GameForm();

			Application.Idle += (sender, args) => ApplicationOnIdle(gameForm);
			Application.Run(gameForm);
		}

		private static void ApplicationOnIdle(GameForm gameForm)
		{
			NativeMessage message;

			while (!PeekMessage(out message, _handleRef, 0, 0, 0))
			{
				gameForm.Render();
			}
		}

		[StructLayout(LayoutKind.Sequential)]
		private struct NativeMessage
		{
			private readonly IntPtr handle;
			private readonly uint msg;
			private readonly IntPtr wParam;
			private readonly IntPtr lParam;
			private readonly uint time;
			private readonly Point p;
		}
	}
#endif
}