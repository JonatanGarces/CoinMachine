using slotmachine.Objects;
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace slotmachine.Librerias
{
    public  class SerialObserver 
    {
        public Action Changed;
        string[] serialPorts;
        List<string> array_devices = new List<string>();
        public Action<byte[]> DataReceived;

        private static WqlEventQuery deviceArrivalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 2");
        private static WqlEventQuery deviceRemovalQuery = new WqlEventQuery("SELECT * FROM Win32_DeviceChangeEvent WHERE EventType = 3");
        ManagementEventWatcher arrival;
        ManagementEventWatcher removal;
        SerialPort serialport = new SerialPort();

        ManagementObjectSearcher ManObjSearch;
        ManagementObjectCollection ManObjReturn;
        public SerialObserver()
        {
           // serialPorts = GetAvailableSerialPorts();

            //MonitorDeviceChanges();
        }
        public enum EventType
        {
            Insertion,
            Removal,
        }

        
      
        private  void MonitorDeviceChanges()
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

        private  void RaisePortsChangedIfNecessary(EventType eventType, EventArrivedEventArgs args)
        {
            lock (serialPorts)
            {
                var availableSerialPorts = GetAvailableSerialPorts();
                array_devices.Clear();
                if (!serialPorts.SequenceEqual(availableSerialPorts))
                {

                   
                     ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
                     ManObjReturn = ManObjSearch.Get();

                    Console.WriteLine(String.Join(", ", args));
                    serialPorts = availableSerialPorts;


                    foreach (ManagementObject ManObj in ManObjReturn)
                    {
                        if (ManObj["Name"].ToString().Contains("Arduino") && ManObj["Status"].ToString().ToLower().Trim() == "ok")
                        {
                            String com = ManObj["DeviceID"].ToString().Trim();
                            array_devices.Add(com);
                        }
                    }
                    if (array_devices.Any())
                    {
                        Changed?.Invoke();
                    }
                    
                    //PortsChanged.Raise(null, new PortsChangedArgs(eventType, _serialPorts));
                }
            }
        }


        public  List<Device> GetSerials()
        {
            List<Device> list = new List<Device>();

            ManObjSearch = new ManagementObjectSearcher("Select * from Win32_SerialPort");
            ManObjReturn = ManObjSearch.Get();
            foreach (ManagementObject ManObj in ManObjReturn)
            {
                if (ManObj["Name"].ToString().Contains("Arduino") && ManObj["Status"].ToString().ToLower().Trim() == "ok")
                {
                    String com = ManObj["DeviceID"].ToString().Trim();
                    list.Add(new Device(ManObj["Name"].ToString(), com));
                }
            }
            return list;
        }


        public SerialPort Connect(String port)
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
              MessageBox.Show(ex.Message, "Data Received Event");
            }
        }

        public  string[] GetAvailableSerialPorts()
        {
            return SerialPort.GetPortNames();
        }

       // public void Dispose()
       // {
        //    throw new NotImplementedException();
       // }

        

    }
}
