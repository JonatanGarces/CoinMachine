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
        public Action<byte[]> DataReceived;
        public List<Device> devices = new List<Device>();
        private static WqlEventQuery deviceArrivalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
        private static WqlEventQuery deviceRemovalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
        private ManagementEventWatcher arrival;
        private ManagementEventWatcher removal;
        public SerialPort serialport = new SerialPort();

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
                    ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
                    ManObjReturn = ManObjSearch.Get();
                    serialPorts = availableSerialPorts;
                    foreach (ManagementObject ManObj in ManObjReturn)
                    {
                        // if (ManObj["Name"].ToString().Contains("Arduino") && ManObj["Status"].ToString().ToLower().Trim() == "ok")
                        //   {
                        string com = ManObj["DeviceID"].ToString().Trim();
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
            ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
            ManObjReturn = ManObjSearch.Get();
            foreach (ManagementObject ManObj in ManObjReturn)
            {
                // if (ManObj["Name"].ToString().Contains("Arduino") && ManObj["Status"].ToString().ToLower().Trim() == "ok")
                // {
                string com = ManObj["DeviceID"].ToString().Trim();
                devices.Add(new Device(ManObj["Name"].ToString(), com));
                //  }
            }
            return devices;
        }

        public SerialPort Connect(string port)
        {
            if (!serialport.IsOpen)
            {
                serialport.PortName = port;
                serialport.BaudRate = 115200;
                serialport.DataReceived += mySerialPort_DataReceived;
                serialport.Open();
            }
            return serialport;
        }

        public void mySerialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                //no. of data at the port
                int ByteToRead = serialport.BytesToRead;
                //create array to store buffer data
                byte[] inputData = new byte[ByteToRead];
                //read the data and store
                serialport.Read(inputData, 0, ByteToRead);
                var copy = DataReceived;
                if (copy != null) copy(inputData);
            }
            catch (SystemException ex)
            {
                Console.WriteLine(ex.Message);
                //MessageBox.Show(, "Data Received Event");
            }
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