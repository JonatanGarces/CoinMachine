
using ScreenSaver;
using slotmachine;
using slotmachine.Librerias;
using System;
using System.Collections.Generic;

using System.IO.Ports;
using System.Management;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using tragamoneda;

namespace SystemTray
{
    public partial class FormTimer : Form
    {
        #region Form Dragging API Support
        //The SendMessage function sends a message to a window or windows.

        [DllImport("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]

        static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        //ReleaseCapture releases a mouse capture

        [DllImportAttribute("user32.dll", CharSet = CharSet.Auto, SetLastError = false)]

        public static extern bool ReleaseCapture();

        #endregion
        Brain brain;
        public CountDownTimer timer =  new CountDownTimer();


        SerialObserver so;
        public delegate void AddDataDelegate(String myString);
        public AddDataDelegate myDelegate;
        public FormTimer(SerialObserver so)
        {
                       InitializeComponent();
            timer.TimeChanged += () => {
                //form1.Invoke((System.Windows.Forms.MethodInvoker)delegate () { form1.lblcountdown.Text = timer.TimeLeftMsStr; });
                lblcountdown.Text = timer.TimeLeftStr;

                //lblcountdown.Invoke(this.myDelegate, new Object[] { timer.TimeLeftMsStr });

            };
            timer.CountDownFinished += () =>
            {
                //form.notifyIcon1.ShowBalloonTip(100000, "Inserte Moneda", "Precaucion inserte moneda tu tiempo casi se ha agotado, tu sesión se cerara y podrias perder tu información que estas trabajando", ToolTipIcon.Warning);      
                 ShowScreenSaver();
            };
            timer.Start();
            this.so = so;
        }
        public void AddDataMethod(String myString)
        {
            lblcountdown.Text = myString;
        }
      

       
        private void Form1_Move(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();
                notifyIcon1.ShowBalloonTip(1000, "Important notice", "Something Important has come up.Click this to know more", ToolTipIcon.Info);
            }
        }
        private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }
        private void showToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Show();
        }
        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
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

           // this.myDelegate = new AddDataDelegate(AddDataMethod);

           // if (timer == null)
          //  {
                
                //timer.SetTime(0, 0);
                //timer.Start();
          //  }
          //  else if (timer.IsRunnign == false)
          //  {
               // timer.Restart();
         //   }
            //KeyBoardHook keyboard = new KeyBoardHook(true);
            //keyboard.KeyUp += c_ThresholdReached;
              so.DataReceived += ProcessData;

        }

      
        private void ProcessData(byte[] data)
        {
            string utfString = Encoding.UTF8.GetString(data, 0, data.Length);
            Console.WriteLine(utfString);
            this.Invoke((System.Windows.Forms.MethodInvoker)delegate () { HideScreenSaver(); });

            //HideScreenSaver();
            if (timer.IsRunnign == false)
            {
                this.timer.SetTime(Int32.Parse(utfString.Trim()), 0);
                //timer.Restart();
                // form.Invoke((System.Windows.Forms.MethodInvoker)delegate () { this.timer.Start(); HideScreenSaver(); });
            }
            else
            {
                this.timer.AddTime(Int32.Parse(utfString.Trim()), 0);
                //timer.Restart();
                //timer.Start();
            }
        }


        List<ScreenSaverForm> screens = new List<ScreenSaverForm>();

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