using MaSoft.Code;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Printer
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

            PrintQueueMonitor pm = new PrintQueueMonitor("Microsoft Print To PDF");
            pm.OnJobStatusChange += OnJobStatusChange;
            Application.Run(new Form1());
        }
        //                            OnJobStatusChange(this, new PrintJobChangeEventArgs(intJobID, _spoolerName, strDriveName, strJobName, jStatus, pji, intJobSize));

        public static void OnJobStatusChange(object Sender, PrintJobChangeEventArgs e)
        {
            //((PrintQueueMonitor)Sender);
            Console.WriteLine(string.Format("{0} {1} {2} {3}", e.JobID,e.JobSize,e.JobStatus,e.PrintName));
            if (e.JobStatus == JOBSTATUS.JOB_STATUS_SPOOLING && e.JobStatus != JOBSTATUS.JOB_STATUS_PAUSED)
            {
                try
                {
                    var pDefault = new PrinterApi.PRINTER_DEFAULTS();
                    IntPtr phPrinter;
                    if (PrinterApi.OpenPrinter(e.PrintName, out phPrinter, pDefault))
                    {
                        PrinterApi.SetJob(phPrinter, e.JobID, 0, IntPtr.Zero, PrinterApi.PrintJobControlCommands.JOB_CONTROL_PAUSE);
                        PrinterApi.ClosePrinter(phPrinter);
                    }
                }
                catch (Exception exception)
                {
                   Console.WriteLine(exception);
                }
            }
        }
    }
}
