using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace slotmachine.Objects
{
    public class Device
    {
        public string Name { get; set; }
        public string Port { get; set; }

       public  Device(string Name,string Port)
        {
            this.Name = Name;
            this.Port= Port;
        }
    }
}
