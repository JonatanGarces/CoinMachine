using Objects;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Library
{
    public class SerialObserver
    {
        public Action Changed;
        private string[] serialPorts;
        private List<string> array_devices = new List<string>();
        public List<Device> devices = new List<Device>();
        private static WqlEventQuery deviceArrivalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
        private static WqlEventQuery deviceRemovalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");

        private string select = "SELECT * FROM Win32_PnPEntity WHERE Name LIKE '%COM%' AND PNPClass = 'Ports'";
        private string scope = "root\\CIMV2";

        //MSSerial_PortName
        //Win32_SerialPort
        private ManagementEventWatcher arrival;

        private ManagementEventWatcher removal;
        private ManagementObjectSearcher ManObjSearch;
        private ManagementObjectCollection ManObjReturn;

        public SerialObserver()
        {
            serialPorts = GetAvailableSerialPorts();
            MonitorDeviceChanges();
        }

        public enum EventType
        {
            Insertion,
            Removal,
        }

        private void MonitorDeviceChanges()
        {
            try
            {
                arrival = new ManagementEventWatcher(deviceArrivalQuery);
                removal = new ManagementEventWatcher(deviceRemovalQuery);

                arrival.EventArrived += (sender, changedArgs) => RaisePortsChangedIfNecessary(EventType.Insertion, changedArgs);
                removal.EventArrived += (sender, changedArgs) => RaisePortsChangedIfNecessary(EventType.Removal, changedArgs);

                // Start listening for events
                arrival.Start();
                removal.Start();
            }
            catch (ManagementException err)
            {
            }
        }

        private void RaisePortsChangedIfNecessary(EventType eventType, EventArrivedEventArgs args)
        {
            devices = new List<Device>();

            lock (serialPorts)
            {
                var availableSerialPorts = GetAvailableSerialPorts();
                array_devices.Clear();
                if (!serialPorts.SequenceEqual(availableSerialPorts))
                {
                    ManObjSearch = new ManagementObjectSearcher(select);
                    ManObjReturn = ManObjSearch.Get();
                    serialPorts = availableSerialPorts;
                    foreach (ManagementObject ManObj in ManObjReturn)
                    {
                        // if (ManObj["Name"].ToString().Contains("Arduino") && ManObj["Status"].ToString().ToLower().Trim() == "ok")
                        //   {
                        string com = "";
                        if (ManObj["DeviceID"] != null && ManObj["DeviceID"].ToString().Contains("COM"))
                        {
                            com = ManObj["DeviceID"].ToString().Trim();
                        }
                        else
                        {
                            com = ManObj["Caption"].ToString().Split('(', ')')[1].Trim();
                        }

                        devices.Add(new Device(ManObj["Name"].ToString(), com));
                        array_devices.Add(com);
                        //  }
                    }
                    // if (array_devices.Any())
                    // {
                    Changed?.Invoke();
                    //}

                    //PortsChanged.Raise(null, new PortsChangedArgs(eventType, _serialPorts));
                }
            }
        }

        public List<Device> GetSerials()
        {
            devices = new List<Device>();
            ManObjSearch = new ManagementObjectSearcher(select);
            ManObjReturn = ManObjSearch.Get();
            foreach (ManagementObject ManObj in ManObjReturn)
            {
                // if (ManObj["Name"].ToString().Contains("Arduino") && ManObj["Status"].ToString().ToLower().Trim() == "ok")
                // {
                string com = "";
                if (ManObj["DeviceID"] != null && ManObj["DeviceID"].ToString().Contains("COM"))
                {
                    com = ManObj["DeviceID"].ToString().Trim();
                }
                else
                {
                    com = ManObj["Caption"].ToString().Split('(', ')')[1].Trim();
                }

                devices.Add(new Device(ManObj["Name"].ToString(), com));
                //  }
            }
            return devices;
        }

        public string[] GetAvailableSerialPorts()
        {
            return SerialPort.GetPortNames();
        }

        // public void Dispose()
        // {
        //    throw new NotImplementedException();
        // }
    }
}