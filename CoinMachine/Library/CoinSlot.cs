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
        private FormCountDownTimer1 formcountdowntimer;

        public CoinSlot(Serial serial)
        {
            serial.DataReceived += DataReceived;
            wallet.Earned += Earned;
            wallet.Spend += Spend;
            countdowntimer.Started += Started;
            countdowntimer.TimeChanged += TimeChanged;
            countdowntimer.CountDownFinished += CountDownFinished;
           // int notificationminute = Int32.Parse(configmanager.ReadSetting("NotificationMinute"));
            //countdowntimer.SetNotificationTime(1, 0);
           // countdowntimer.Notification += Notification;
            countdowntimer.Start();
        }

        private void DataReceived(byte[] serial)
        {
            //this.Invoke((System.Windows.Forms.MethodInvoker)delegate () { HideScreenSaver(); });
            string utfString = Encoding.UTF8.GetString(serial, 0, serial.Length);
            wallet.EarnMoney(float.Parse(Encoding.UTF8.GetString(serial, 0, serial.Length).Trim(), CultureInfo.InvariantCulture.NumberFormat));
        }

        private void Earned(float debit)
        {
            Console.WriteLine(conversion.getMinutes(debit));
            countdowntimer.AddTime(conversion.getMinutes(debit));
        }

        private void Spend(float debit)
        {
            countdowntimer.SetTime(conversion.getMinutes(debit));
        }

        private void Started()
        {
            HideScreenSaver();
            //keyboard.EnableTaskManager();
            keyboard.Dispose();
            if (formcountdowntimer == null) { formcountdowntimer = new FormCountDownTimer1(); };
            formcountdowntimer.Show();
        }

        private void TimeChanged()
        {
            //time changed it means it decreased decrease
            if (formcountdowntimer == null) { formcountdowntimer = new FormCountDownTimer1(); };
            formcountdowntimer.lblcountdown.Text = countdowntimer.TimeLeftStr;

            formcountdowntimer.label1.Text = conversion.getMoney(countdowntimer.MinutesLeft).ToString();
            //remove money from wallet
            //doo time to money conversion and update wallet or.. call  substract the conversion of 1 second to the wallet



        }

        private void CountDownFinished()
        {
            ShowScreenSaver();
            formcountdowntimer.Close();
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
    }
}