using Library;
using Forms;
using System;
using System.Windows.Forms;

namespace CoinMachine
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        private static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            new Inicio().Show();
            Application.Run();
        }
    }
}