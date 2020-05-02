/*
 * ScreenSaverForm.cs
 * By Frank McCown
 * Summer 2010
 *
 * Feel free to modify this code.
 */

using System;
using System.Drawing;

using System.Windows.Forms;
using System.Runtime.InteropServices;
using Library;
using CoinMachine;
using CoinMachine.Library;

namespace Forms
{
    public partial class ScreenSaverForm : Form
    {
        #region Win32 API functions

        [DllImport("user32.dll")]
        private static extern IntPtr SetParent(IntPtr hWndChild, IntPtr hWndNewParent);

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [DllImport("user32.dll", SetLastError = true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);

        [DllImport("user32.dll")]
        private static extern bool GetClientRect(IntPtr hWnd, out Rectangle lpRect);

        #endregion Win32 API functions

        private bool previewMode = false;
        private Random rand = new Random();
        private ConfigManager configmanager = new ConfigManager();

        public ScreenSaverForm(Rectangle Bounds)
        {
            InitializeComponent();
            this.Bounds = Bounds;
            Global.Instance.KeyEnabled = false;
            KeyBoardHook keyboard = new KeyBoardHook(true);
            keyboard.KeyDown += c_ThresholdReached;
        }

        private static void c_ThresholdReached(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            Console.WriteLine("Down: " + key);
            if (Shift && Ctrl && Alt && key.ToString().Trim().ToLower() == "q")
            {
                Console.WriteLine("keyenabled razaaa");
                Global.Instance.KeyEnabled = true;
            }
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            //picBackgroundImage.Image.
            // this.BackColor =

            this.BackColor = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundColor")));
            textLabel.ForeColor = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundMessageColor")));
            textLabel.BackColor = System.Drawing.Color.Transparent;

            picBackgroundImage.Image = Image.FromFile(configmanager.ReadSetting("BackgroundImage"));
            textLabel.Text = configmanager.ReadSetting("BackgroundMessage");
            Cursor.Hide();
            TopMost = true;
            moveTimer.Interval = 1000;
            moveTimer.Tick += new EventHandler(moveTimer_Tick);
            moveTimer.Start();
        }

        private void moveTimer_Tick(object sender, System.EventArgs e)
        {
            textLabel.Left = rand.Next(Math.Max(1, Bounds.Width - textLabel.Width));
            textLabel.Top = rand.Next(Math.Max(1, Bounds.Height - textLabel.Height));
        }

        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }
    }
}