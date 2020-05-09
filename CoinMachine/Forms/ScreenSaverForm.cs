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
            //textLabel.Parent = pictureBox1;
            //// textLabel.BackColor = Color.Transparent;
            // textLabel.BringToFront();
            this.Bounds = Bounds;
            Global.Instance.KeyEnabled = false;
            KeyBoardHook keyboard = new KeyBoardHook(true);
            keyboard.KeyDown += c_ThresholdReached;
            this.BackColor = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundColor")));
            picBackgroundImage.Image = Image.FromFile(configmanager.ReadSetting("BackgroundImage"));

            picBackgroundImage.Controls.Add(pictureBox1);
            Font font = new Font("Microsoft Sans Serif", 48.0f, FontStyle.Bold);
            Color col = Color.FromArgb(int.Parse(configmanager.ReadSetting("BackgroundMessageColor")));
            Image imagen = DrawText(configmanager.ReadSetting("BackgroundMessage"), font, col, Color.Transparent);
            pictureBox1.Image = imagen;
        }

        private static void c_ThresholdReached(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
        }

        private void ScreenSaverForm_Load(object sender, EventArgs e)
        {
            Cursor.Hide();
            /// textLabel.ForeColor = );

            //textLabel.BackColor = System.Drawing.Color.Transparent;
            //textLabel.Text = configmanager.ReadSetting("BackgroundMessage");
            TopMost = true;
            moveTimer.Interval = 1000;
            moveTimer.Tick += new EventHandler(moveTimer_Tick);
            moveTimer.Start();
        }

        private void moveTimer_Tick(object sender, System.EventArgs e)
        {
            pictureBox1.Left = rand.Next(Math.Max(1, Bounds.Width - pictureBox1.Width));
            pictureBox1.Top = rand.Next(Math.Max(1, Bounds.Height - pictureBox1.Height));
        }

        private void ScreenSaverForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!previewMode)
                Application.Exit();
        }

        private Image DrawText(String text, Font font, Color textColor, Color backColor)
        {
            //first, create a dummy bitmap just to get a graphics object
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);

            //measure the string to see how big the image needs to be
            SizeF textSize = drawing.MeasureString(text, font);

            //free up the dummy image and old graphics object
            img.Dispose();
            drawing.Dispose();

            //create a new image of the right size
            img = new Bitmap((int)textSize.Width, (int)textSize.Height);

            drawing = Graphics.FromImage(img);

            //paint the background
            drawing.Clear(backColor);

            //create a brush for the text
            Brush textBrush = new SolidBrush(textColor);

            drawing.DrawString(text, font, textBrush, 0, 0);

            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            return img;
        }
    }
}