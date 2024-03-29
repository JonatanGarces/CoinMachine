﻿using EventHook;
using MaSoft.Code;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Printing.IndexedProperties;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Printer
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
                Console.WriteLine("Printer '{0}' currently printing {1} pages ", e.EventData.PrinterName,
                    e.EventData.Pages);
            };
            Application.Run(new Form1());

            Console.Read();

            printWatcher.Stop();

            eventHookFactory.Dispose();

            //PrintQueueMonitor pm = new PrintQueueMonitor("Microsoft Print to PDF");
            //pm.OnJobStatusChange += OnJobStatusChange;
        }

        //                            OnJobStatusChange(this, new PrintJobChangeEventArgs(intJobID, _spoolerName, strDriveName, strJobName, jStatus, pji, intJobSize));

        public static void OnJobStatusChange(object Sender, PrintJobChangeEventArgs e)
        {
            //((PrintQueueMonitor)Sender);
            Console.WriteLine(string.Format("{0} {1} {2} {3} {4} ", e.JobID, e.JobSize, e.JobStatus, e.PrintName, e.Pages));
            // var jobInfo = new PrinterApi.JOB();
            //PrinterApi.GetJobInfo(ref jobInfo, e.PrintName, e.JobID);
            // jobInfo.MediaType;
            //  jobInfo.Duplex,
            //PrintPropertyDictionary printQueueProperties = e.JobInfo.PropertiesCollection;
            // foreach (DictionaryEntry entry in printQueueProperties)
            // {
            //    PrintProperty property = (PrintProperty)entry.Value;

            //   if (property.Value != null)
            //   {
            //        Console.WriteLine(property.Name + "\t(Type: {0})", property.Value.GetType().ToString());
            //  }
            //  }

            var devMode = PrinterHelper.GetPrinterDevMode(e.PrintName);
            string s = String.Format("{0} : {1} x {2} . {3} . {4} . {5} . {6}", (PrinterHelper.PaperSize)devMode.dmPaperSize, devMode.dmPaperWidth, devMode.dmPaperLength, (PrinterHelper.PageColor)devMode.dmColor, devMode.dmCopies, (PrinterHelper.PageDuplex)devMode.dmDuplex, (PrinterHelper.PageDisplayFlags)devMode.dmDisplayFlags);
            Console.WriteLine(s);

            //   jobInfo.Color,
            //   e.JobInfo.NumberOfPages,
            //   jobInfo.Copyes,
            //  jobInfo.Orientation
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

        public enum PrintJobControlCommands
        {
            JOB_CONTROL_SETJOB,
            JOB_CONTROL_PAUSE,
            JOB_CONTROL_RESUME,
            JOB_CONTROL_CANCEL,
            JOB_CONTROL_RESTART,
            JOB_CONTROL_DELETE,
            JOB_CONTROL_SENT_TO_PRINTER,
            JOB_CONTROL_LAST_PAGE_EJECTED
        }
    }
}