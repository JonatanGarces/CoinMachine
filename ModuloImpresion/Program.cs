using EventHook;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ModuloImpresion
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
            var eventHookFactory = new EventHookFactory();

            var printWatcher = eventHookFactory.GetPrintWatcher();
            printWatcher.Start();
            printWatcher.OnPrintEvent += (s, e) =>
            {
                Console.WriteLine("Printer '{0}' currently printing {1} pages.", e.EventData.JobStatus,
                    e.EventData.Pages);
            };

            Console.Read();

            Application.Run(new Form1());
            printWatcher.Stop();

            eventHookFactory.Dispose();
        }
    }
}