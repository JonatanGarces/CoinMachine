using Forms;
using Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinMachine.Library
{
    internal class CoinSlot
    {
        private Wallet wallet = new Wallet();
        private CountDownTimer countdowntimer = new CountDownTimer();
        private Conversion conversion = new Conversion();
        private KeyBoardHook keyboard = new KeyBoardHook(true);
        private List<ScreenSaverForm> screens = new List<ScreenSaverForm>();
        private FormCountDownTimer1 formcountdowntimer = new FormCountDownTimer1();
        public ConfigManager configmanager = new ConfigManager();

        public CoinSlot(Serial serial)
        {
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

            formcountdowntimer.Show();
            formcountdowntimer.Refresh();
        }

        private void TimeChanged()
        {
            Console.WriteLine("TimeChanged");

            wallet.Debit = conversion.getMoney(countdowntimer.MinutesLeft);
            formcountdowntimer.Invoke((System.Windows.Forms.MethodInvoker)delegate ()
            {
                formcountdowntimer.setlblcountdown(countdowntimer.TimeLeftStr);
            });

            //  ShowFormCountDownTimer().lblcountdown.Refresh();

            //Console.WriteLine(conversion.getMoney(countdowntimer.MinutesLeft));
            // formcountdowntimer.label1.Text = conversion.getMoney(countdowntimer.MinutesLeft).ToString("F1");
            //formcountdowntimer.label1.Refresh();
        }

        private void CountDownFinished()
        {
            Console.WriteLine("CountDownFinished");
            formcountdowntimer.Hide();
            Console.WriteLine("formcountdowntimer.Hide");

            // ShowScreenSaver();
        }

        private void Notification()
        {
            //Console.WriteLine("Notification");
            formcountdowntimer.notifyIcon1.ShowBalloonTip(100000, configmanager.ReadSetting("NotificationTitle"), configmanager.ReadSetting("NotificationMessage"), ToolTipIcon.Warning);
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

        /*
        public FormCountDownTimer1 ShowFormCountDownTimer()
        {
            if (formcountdowntimer == null)
            {
                formcountdowntimer = new FormCountDownTimer1();
                Console.WriteLine("formcountdowntimer.null");
            }
            else if (formcountdowntimer.IsDisposed)
            {
                formcountdowntimer = new FormCountDownTimer1();
                Console.WriteLine("formcountdowntimer.IsDisposed");
            }

            return formcountdowntimer;
        }
        */
    }
}