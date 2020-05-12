using Forms;
using Library;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoinMachine.Library
{
    internal class CoinSlot
    {
        private List<ScreenSaverForm> screens = new List<ScreenSaverForm>();
        private Wallet wallet = new Wallet();
        private CountDownTimer coundowntimer = new CountDownTimer();

        public CoinSlot(Serial serial)
        {
            serial.DataReceived += ProcessData;
        }

        private void ProcessData(byte[] data)
        {
            string utfString = Encoding.UTF8.GetString(data, 0, data.Length);
            //this.Invoke((System.Windows.Forms.MethodInvoker)delegate () { HideScreenSaver(); });
            HideScreenSaver();
            if (coundowntimer.IsRunnign == false)
            {
                coundowntimer.SetTime(Int32.Parse(utfString.Trim()), 0);
            }
            else
            {
                coundowntimer.AddTime(Int32.Parse(utfString.Trim()), 0);
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