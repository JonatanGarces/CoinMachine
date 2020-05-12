using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine.Library
{
    public class Serial
    {
        public SerialPort serialport = new SerialPort();
        public Action<byte[]> DataReceived;

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
    }
}