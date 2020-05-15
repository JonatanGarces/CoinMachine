using CoinMachine;
using CoinMachine.Library;
using Library;
using System;
using System.Collections.Generic;
using System.Globalization;
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

        private Wallet wallet = new Wallet();
        private CountDownTimer countdowntimer = new CountDownTimer();
        private Conversion conversion = new Conversion();
        private KeyBoardHook keyboard = new KeyBoardHook(true);
        private List<ScreenSaverForm> screens = new List<ScreenSaverForm>();
        public ConfigManager configmanager = new ConfigManager();

        public FormCountDownTimer1(Serial serial)
        {
            InitializeComponent();
            lblcountdown.Cursor = Cursors.SizeAll;
            serial.DataReceived += DataReceived;
            wallet.Earned += Earned;
            // wallet.Spend += Spend;
            countdowntimer.Started += Started;
            countdowntimer.TimeChanged += TimeChanged;
            countdowntimer.CountDownFinished += CountDownFinished;
            countdowntimer.Notification += Notification;
            countdowntimer.Start();
        }

        private void DataReceived(byte[] serial)
        {
            //Console.WriteLine("DataReceived");
            string utfString = Encoding.UTF8.GetString(serial, 0, serial.Length);
            wallet.EarnMoney(float.Parse(Encoding.UTF8.GetString(serial, 0, serial.Length).Trim(), CultureInfo.InvariantCulture.NumberFormat));
        }

        private void Earned(float debit)
        {
            Console.WriteLine("Earned");
            countdowntimer.SetTime(conversion.getMinutes(debit));
        }

        //private void Spend(float debit)
        //{
        //   countdowntimer.SetTime(conversion.getMinutes(debit));
        // }

        private void Started()
        {
            Console.WriteLine("Started");

            //HideScreenSaver();
            //keyboard.EnableTaskManager();
            //keyboard.Dispose();
            this.Invoke((System.Windows.Forms.MethodInvoker)delegate ()
            {
                this.Show();
            });
        }

        private void TimeChanged()
        {
            Console.WriteLine("TimeChanged");

            wallet.Debit = conversion.getMoney(countdowntimer.MinutesLeft);
            this.Invoke((System.Windows.Forms.MethodInvoker)delegate ()
            {
                setlblcountdown(countdowntimer.TimeLeftStr);
            });

            //  ShowFormCountDownTimer().lblcountdown.Refresh();

            //Console.WriteLine(conversion.getMoney(countdowntimer.MinutesLeft));
            // formcountdowntimer.label1.Text = conversion.getMoney(countdowntimer.MinutesLeft).ToString("F1");
            //formcountdowntimer.label1.Refresh();
        }

        private void CountDownFinished()
        {
            Console.WriteLine("CountDownFinished");
            this.Invoke((System.Windows.Forms.MethodInvoker)delegate ()
            {
                this.Hide();
            });
            Console.WriteLine("formcountdowntimer.Hide");

            // ShowScreenSaver();
        }

        private void Notification()
        {
            this.notifyIcon1.ShowBalloonTip(100000, configmanager.ReadSetting("NotificationTitle"), configmanager.ReadSetting("NotificationMessage"), ToolTipIcon.Warning);
        }

        public void HideScreenSaver()
        {
            foreach (ScreenSaverForm screensaver in screens)
            {
                screensaver.Close();
            }
            screens.Clear();
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

        private void lblcountdown_Click(object sender, EventArgs e)
        {
        }

        private void label1_Click(object sender, EventArgs e)
        {
        }
    }
}

//https://docs.microsoft.com/en-us/dotnet/csharp/language-reference/operators/addition-operator
//https://www.codeproject.com/Articles/7294/Processing-Global-Mouse-and-Keyboard-Hooks-in-C
//https://stackoverflow.com/questions/604410/global-keyboard-capture-in-c-sharp-application
//https://stackoverflow.com/questions/14943036/blank-screen-after-x-minutes