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
        KeyBoardHook keyboard = new KeyBoardHook(true);
        private List<ScreenSaverForm> screens = new List<ScreenSaverForm>();
        FormCountDownTimer1 formcountdowntimer ;
        
        public CoinSlot(Serial serial)
        {
            serial.DataReceived += DataReceived;
            wallet.Earned += Earned;
            wallet.Spend += Spend;
            countdowntimer.Started += Started;
            countdowntimer.TimeChanged += TimeChanged;
            countdowntimer.CountDownFinished += CountDownFinished;
        }

        private void DataReceived(byte[] serial)
        {
            //this.Invoke((System.Windows.Forms.MethodInvoker)delegate () { HideScreenSaver(); });
                wallet.EarnMoney(float.Parse(Encoding.UTF8.GetString(serial, 0, serial.Length).Trim(), CultureInfo.InvariantCulture.NumberFormat));
        }
        private void Earned(float debit)
        {
            countdowntimer.AddTime(conversion.getSeconds(debit));
        }
        private void Spend(float debit)
        {
            countdowntimer.SetTime(conversion.getSeconds(debit));
        }
        private void Started()
        {
            HideScreenSaver();
            keyboard.EnableTaskManager();
            keyboard.Dispose();
            formcountdowntimer = new FormCountDownTimer1();
            formcountdowntimer.Show();
        }

        private void TimeChanged()
        {
            formcountdowntimer.lblcountdown.Text = countdowntimer.TimeLeftStr;

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