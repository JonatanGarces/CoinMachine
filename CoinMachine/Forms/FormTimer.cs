using CoinMachine;
using Library;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Forms
{
    public partial class FormTimer : Form
    {
        #region Form Dragging API Support

        //The SendMessage function sends a message to a window or windows.

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        private static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        //ReleaseCapture releases a mouse capture

        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]
        public static extern bool ReleaseCapture();

        #endregion Form Dragging API Support

        public CountDownTimer timer = new CountDownTimer();
        private List<ScreenSaverForm> screens = new List<ScreenSaverForm>();
        private SerialObserver so;
        private ConfigManager configmanager = new ConfigManager();

        public FormTimer(SerialObserver so)
        {
            InitializeComponent();
            int notificationminute = Int32.Parse(configmanager.ReadSetting("NotificationMinute"));
            this.timer.SetNotificationTime(notificationminute, 0); ;

            timer.TimeChanged += () =>
            {
                lblcountdown.Text = timer.TimeLeftStr;
            };
            timer.CountDownFinished += () =>
            {
                ShowScreenSaver();
            };
            timer.Notification += () =>
            {//"Precaucion inserte moneda tu tiempo casi se ha agotado, tu sesión se cerrara y podrias perder tu información que estas trabajando
                this.notifyIcon1.ShowBalloonTip(100000, configmanager.ReadSetting("NotificationTitle"), configmanager.ReadSetting("NotificationMessage"), ToolTipIcon.Warning);
            };
            timer.Start();
            this.so = so;

            if (timer.IsRunnign == false)
            {
                this.timer.SetTime(1, 0);
            }
            else
            {
                this.timer.AddTime(1, 0);
            }
            lblcountdown.Cursor = Cursors.SizeAll;
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
            so.DataReceived += ProcessData;
        }

        private void ProcessData(byte[] data)
        {
            string utfString = Encoding.UTF8.GetString(data, 0, data.Length);
            Console.WriteLine(utfString);
            this.Invoke((System.Windows.Forms.MethodInvoker)delegate () { HideScreenSaver(); });

            if (timer.IsRunnign == false)
            {
                this.timer.SetTime(Int32.Parse(utfString.Trim()), 0);
            }
            else
            {
                this.timer.AddTime(Int32.Parse(utfString.Trim()), 0);
            }
        }

        public void ShowScreenSaver()
        {
            if (!(screens.Count > 0))
            {
                foreach (Screen screen in Screen.AllScreens)
                {
                    ScreenSaverForm screensaver = new ScreenSaverForm(screen.Bounds);
                    screensaver.Show();
                    screens.Add(screensaver);
                }
            }
        }

        public void HideScreenSaver()
        {
            foreach (ScreenSaverForm screensaver in screens)
            {
                screensaver.Close();
            }
            screens.Clear();
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/addition-operator
//https://www.codeproject.com/Articles/7294/Processing-Global-Mouse-and-Keyboard-Hooks-in-C
//https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
//https://stackoverflow.com/questions/14943036/blank-screen-after-x-minutes