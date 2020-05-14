using CoinMachine;
using CoinMachine.Library;
using Library;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Forms
{
    public partial class FormCountDownTimer1 : Form
    {
        #region Form Dragging API Support

        //The SendMessage function sends a message to a window or windows.

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        //ReleaseCapture releases a mouse capture

        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern bool ReleaseCapture();

        #endregion Form Dragging API Support

        public FormCountDownTimer1()
        {
            InitializeComponent();
            lblcountdown.Cursor = Cursors.SizeAll;
        }

        public void setlblcountdown(String TimeLeftStr)
        {
            lblcountdown.Text = TimeLeftStr;
            //lblcountdown.Refresh();
        }

        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void FormTimer_Load(object sender, EventArgs e)
        {
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/addition-operator
//https://www.codeproject.com/Articles/7294/Processing-Global-Mouse-and-Keyboard-Hooks-in-C
//https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
//https://stackoverflow.com/questions/14943036/blank-screen-after-x-minutes