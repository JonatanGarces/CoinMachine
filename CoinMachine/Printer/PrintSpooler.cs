﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace Hiz.Interop.Printing
{
    public static partial class PrintSpooler
    {
        //public static bool IsLocalPrinter(string pPrinterName)
        //{
        //    if (string.IsNullOrEmpty(pPrinterName))
        //        throw new ArgumentException();

        //    return !pPrinterName.StartsWith(@"\\");
        //}

        public static object GetPrinterStatus(string pPrinterName)
        {
            return ExecutePrinter(pPrinterName, hPrinter =>
            {
                var o = Native.ThunkGetPrinter(hPrinter, 6);
                if (o != null)
                    return ((PrinterInfo6)o).dwStatus;
                return PrinterStatus.None;
            });
        }

        private static object ExecutePrinter(string pPrinterName, Func<IntPtr, object> action)
        {
            PrinterDefaults pDefault = null;
            //if (pPrinterName != null)
            pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.PrinterGenericExecute };
            //else // String.Empty 将抛异常: 打印机名无效;
            //    pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.ServerEnumerate };

            var hPrinter = IntPtr.Zero;
            try
            {
                if (!Native.OpenPrinter(pPrinterName, out hPrinter, pDefault))
                    Error.ThrowLastError();

                return action(hPrinter);
            }
            finally
            {
                if (hPrinter != IntPtr.Zero)
                    Native.ClosePrinter(hPrinter);
            }
        }

        public static PrinterInfo4[] GetPrintersWithLevel4()
        {
            /* [本地打印机] = { pServerName = null, pPrinterName = "...", Attributes = ScheduleCompletedJobsFirst | Local; }
             * a). 本地共享的打印机: Attributes |= Shared;
             * b). 本地离线的打印机: Attributes |= WorkOffline;
             *
             * [本地传真机] = { pServerName = null, pPrinterName = "...", Attributes = Fax | Local; }
             *
             * [链接的共享打印机] = { pServerName = "\\{MachineName}\{PrinterName}", pPrinterName = "...", Attributes = ScheduleCompletedJobsFirst | Network | Shared; }
             */

            // System.Drawing.Printing.PrinterSettings.InstalledPrinters.Cast<string>().ToArray();
            var level = 4;
            var array = Native.ThunkEnumPrinters(PrinterFlags.Local | PrinterFlags.Connections, null, level).Cast<PrinterInfo4>().ToArray();
            return array;
        }

        // 在系统中操作: 纸张名称不区分大小写; 如果已经存在名称: Test, 不能添加 test;
        // TODO: 代码待测试;
        // 如果删除已存在的纸张, 并且该纸张被其它打印设备设为默认纸张, 那会怎样? 注册表的 DevMode 数据是否改变?
        public static void DeleteFormOfServer(string pPrinterName, string pFormName)
        {
            PrinterDefaults pDefault = null;
            pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.ServerGenericRead };
            var hPrinter = IntPtr.Zero;
            try
            {
                if (!Native.OpenPrinter(pPrinterName, out hPrinter, pDefault))
                    Error.ThrowLastError();

                Native.ThunkDeleteForm(hPrinter, pFormName);
            }
            finally
            {
                Native.ClosePrinter(hPrinter);
                hPrinter = IntPtr.Zero;
            }
        }

        public static void DeleteFormOfPrinter(string pPrinterName, string pFormName)
        {
            PrinterDefaults pDefault = null;
            pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.PrinterGenericExecute };
            var hPrinter = IntPtr.Zero;
            try
            {
                if (!Native.OpenPrinter(pPrinterName, out hPrinter, pDefault))
                    Error.ThrowLastError();

                Native.ThunkDeleteForm(hPrinter, pFormName);
            }
            finally
            {
                Native.ClosePrinter(hPrinter);
                hPrinter = IntPtr.Zero;
            }
        }

        public static void AddFormOfServer(string pPrinterName, string pFormName, int width, int height)
        {
            PrinterDefaults pDefault = null;
            //if (pPrinterName != null)
            //    pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.PrinterGenericExecute };
            //else // String.Empty 将抛异常: 打印机名无效;
            pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.ServerGenericRead };

            var hPrinter = IntPtr.Zero;
            try
            {
                if (!Native.OpenPrinter(pPrinterName, out hPrinter, pDefault))
                    Error.ThrowLastError();

                var form = new FormInfo1()
                {
                    Flags = FormInfoFlags.User,
                    pName = pFormName,
                    Size = new SIZE(width, height),
                    ImageableArea = new RECT(0, 0, width, height),
                };

                // Native.ThunkSetForm(hPrinter, "Werp214/3", form);

                // Add
                Native.ThunkAddForm(hPrinter, form);
            }
            finally
            {
                Native.ClosePrinter(hPrinter);
                hPrinter = IntPtr.Zero;
            }
        }

        public static void GetPrinterDevMode(string pPrinterName)
        {
            PrinterDefaults pDefault = null;
            //if (pPrinterName != null)
            pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.PrinterGenericExecute };
            //else // String.Empty 将抛异常: 打印机名无效;
            //pDefault = new PrinterDefaults() { DesiredAccess = (int)PrintAccessRights.ServerGenericRead };

            var hPrinter = IntPtr.Zero;
            try
            {
                if (!Native.OpenPrinter(pPrinterName, out hPrinter, pDefault))
                    throw new Win32Exception(Marshal.GetLastWin32Error());

                int level;
                object info;
                var pDevMode = IntPtr.Zero;

                if (pDevMode == IntPtr.Zero)
                {
                    /* Per-User DevMode:
                     *
                     * 本地打印设备:
                     * HKEY_CURRENT_USER\Printers\DevModes2
                     * "{PrinterName}"
                     *
                     * 远程打印设备:
                     * HKEY_CURRENT_USER\Printers\DevModes2\
                     * "\\{MachineName}\{PrinterName}"
                     *
                     * HKEY_CURRENT_USER\Printers\Connections\",,{MachineName},{PrinterName}"\
                     * "DevMode"
                     */
                    level = 9; // Per-User DEVMODE
                    info = Native.ThunkGetPrinter(hPrinter, level);
                    if (info != null)
                    {
                        // 此处只有修改过 "打印首选项" 才会有值;
                        pDevMode = ((PrinterInfo9)info).pDevMode;
                    }
                }

                if (pDevMode == IntPtr.Zero)
                {
                    /* Global DevMode:
                     *
                     * 本地打印设备:
                     * HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Control\Print\Printers\{PrinterName}
                     * Default DevMode
                     *
                     * 远程打印设备:
                     * HKEY_LOCAL_MACHINE\SOFTWARE\Microsoft\Windows NT\CurrentVersion\Print\Providers\Client Side Rendering Print Provider\Servers\{MachineName}\Printers\{PrinterGuid}
                     * Default DevMode
                     */
                    level = 8; // Global DEVMODE
                    info = Native.ThunkGetPrinter(hPrinter, level);
                    if (info != null)
                    {
                        pDevMode = ((PrinterInfo8)info).pDevMode;
                    }
                }

                if (pDevMode != IntPtr.Zero)
                {
                    var dm = (DevMode)Marshal.PtrToStructure(pDevMode, typeof(DevMode));

                    // var size = Marshal.SizeOf(typeof(DevMode));

                    foreach (var f in typeof(DevMode).GetFields(System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public))
                    {
                        Debug.WriteLine("{0}: {1}", f.Name, f.GetValue(dm));
                    }
                    Debug.WriteLine(string.Empty);
                    Debug.WriteLine(string.Empty);
                }
            }
            finally
            {
                if (hPrinter != IntPtr.Zero)
                {
                    Native.ClosePrinter(hPrinter);
                    hPrinter = IntPtr.Zero;
                }
            }
        }

        //public static void SetPrinterDevMode(string pPrinterName)
        //{
        //}

        #region DeviceCapabilities

        /* System.Printing.PageMediaSize // 长度单位: 像素
         * System.Drawing.Printing.PaperSize // 长度单位: 百分之一英寸
         *
         * System.Drawing.dll
         * System.Drawing.Printing.PrinterSettings.Get_PaperSizes()
         */

        public static void TestDeviceCapabilities(string pPrinterName, string pPort = null)
        {
            // 横板旋转角度
            var orientation = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.Orientation, IntPtr.Zero, IntPtr.Zero);

            // 最大拷贝数量
            var copies = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.Copies, IntPtr.Zero, IntPtr.Zero);

            // 支持逐份打印 (支持: 1; 否则: 0; 失败: -1)
            var collate = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.Collate, IntPtr.Zero, IntPtr.Zero);

            // 纸张介质类型
            // GetPrinterMediaTypes(pPrinterName, pPort);

            // 纸张支持范围
            var min = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MinExtent, IntPtr.Zero, IntPtr.Zero);
            var minw = (min & 0xFFFF) / 10.0;
            var minh = (min >> 0x10) / 10.0;
            var max = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MaxExtent, IntPtr.Zero, IntPtr.Zero);
            var maxw = (max & 0xFFFF) / 10.0;
            var maxh = (max >> 0x10) / 10.0;

            // 支持双面打印
            var duplex = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.Duplex, IntPtr.Zero, IntPtr.Zero);

            // GetPrinterMediaReady(pPrinterName, pPort);

            var memory = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.PrinterMemory, IntPtr.Zero, IntPtr.Zero);

            var staple = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.Staple, IntPtr.Zero, IntPtr.Zero);

            var PrintRatePagesPerMinute = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.PrintRatePagesPerMinute, IntPtr.Zero, IntPtr.Zero);
            var PrintRate = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.PrintRate, IntPtr.Zero, IntPtr.Zero);
            var PrintRateUnit = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.PrintRateUnit, IntPtr.Zero, IntPtr.Zero);

            var color = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.ColorDevice, IntPtr.Zero, IntPtr.Zero);

            GetPrinterResolutions(pPrinterName, pPort);
        }

        private static void GetPrinterResolutions(string pPrinterName, string pPort = null)
        {
            var count = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.EnumResolutions, IntPtr.Zero, IntPtr.Zero);

            if (count > 0)
            {
                var pTypes = IntPtr.Zero;
                try
                {
                    var tBytes = 8; // SizeOf(LONG)
                    pTypes = Marshal.AllocCoTaskMem(tBytes * count);
                    if (count != Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.EnumResolutions, pTypes, IntPtr.Zero))
                        throw new InvalidOperationException();

                    var result = new SIZE[count];
                    for (var i = 0; i < count; i++)
                    {
                        var type = Marshal.ReadInt64((IntPtr)checked((long)pTypes + i * tBytes));

                        var w = (int)(type & 0xFFFFFFFF);
                        var h = (int)(type >> 0x20);
                        result[i] = new SIZE(w, h);
                    }
                }
                finally
                {
                    // 释放内存
                    if (pTypes != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pTypes);
                }
            }
        }

        // "打印机属性" 对话框里的 "可用纸张" 列表;
        private static void GetPrinterMediaReady(string pPrinterName, string pPort = null)
        {
            var count = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MediaReady, IntPtr.Zero, IntPtr.Zero);

            if (count > 0)
            {
                var pNames = IntPtr.Zero;
                try
                {
                    var nBytes = 2 * 0x40; // // SizeOf(WChar) * 64 characters; // Unicode
                    pNames = Marshal.AllocCoTaskMem(checked(nBytes * count)); // 检查溢出
                    if (count != Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MediaReady, pNames, IntPtr.Zero))
                        throw new InvalidOperationException();

                    var result = new string[count];
                    for (var i = 0; i < count; i++)
                    {
                        var name = Marshal.PtrToStringUni((IntPtr)checked((long)pNames + i * nBytes)); // 检查地址溢出;
                        result[i] = name;
                    }
                }
                finally
                {
                    // 释放内存
                    if (pNames != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pNames);
                }
            }
        }

        private static void GetPrinterMediaTypes(string pPrinterName, string pPort = null)
        {
            var count = Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MediaTypes, IntPtr.Zero, IntPtr.Zero);

            if (count > 0)
            {
                var pNames = IntPtr.Zero;
                var pTypes = IntPtr.Zero;
                try
                {
                    var tBytes = 4; // SizeOf(DWORD)
                    pTypes = Marshal.AllocCoTaskMem(tBytes * count);
                    if (count != Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MediaTypes, pTypes, IntPtr.Zero))
                        throw new InvalidOperationException();

                    var nBytes = 2 * 0x40; // // SizeOf(WChar) * 64 characters; // Unicode
                    pNames = Marshal.AllocCoTaskMem(checked(nBytes * count)); // 检查溢出
                    if (count != Native.DeviceCapabilities(pPrinterName, pPort, PrinterCapabilities.MediaTypeNames, pNames, IntPtr.Zero))
                        throw new InvalidOperationException();

                    var result = new object[count];
                    for (var i = 0; i < count; i++)
                    {
                        var type = Marshal.ReadInt32((IntPtr)checked((long)pTypes + i * tBytes));

                        var name = Marshal.PtrToStringUni((IntPtr)checked((long)pNames + i * nBytes)); // 检查地址溢出;
                        if (name.Length > 0x40)
                            name = name.Remove(0x40);

                        result[i] = new
                        {
                            Type = type,
                            Name = name,
                        };
                    }
                }
                finally
                {
                    // 释放内存
                    if (pTypes != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pTypes);

                    if (pNames != IntPtr.Zero)
                        Marshal.FreeCoTaskMem(pNames);
                }
            }
        }

        #endregion DeviceCapabilities

        public static string GetPrinterPort(string printer)
        {
            var port = (string)ExecutePrinter(printer, hPrinter =>
            {
                var o = Native.ThunkGetPrinter(hPrinter, 5);
                if (o != null)
                    return ((PrinterInfo5)o).pPortName;
                return null;
            });
            return port;
        }
    }
}