using Library;
using Forms;
using System;
using System.Windows.Forms;

namespace CoinMachine
{
    static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            new Inicio().Show();

            KeyBoardHook keyboard = new KeyBoardHook(true);
            keyboard.KeyUp += c_ThresholdReached;
            Application.Run();
        }

        static void c_ThresholdReached(Keys key, bool Shift, bool Ctrl, bool Alt)
        {
            Console.WriteLine("Down: " + key);
        }
    }
}
