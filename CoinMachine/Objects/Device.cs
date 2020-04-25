namespace Objects
{
    public class Device
    {
        public string Name { get; set; }
        public string Port { get; set; }

        public Device(string Name, string Port)
        {
            this.Name = Name;
            this.Port = Port;
        }
    }
}
