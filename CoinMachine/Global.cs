using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine
{
    public sealed class Global
    {
        private readonly static Global _instance = new Global();

        private Global()
        {
        }

        public static Global Instance
        {
            get
            {
                return _instance;
            }
        }

        public Boolean KeyEnabled { get; set; } = false;
        public Boolean NotificationAppeared { get; set; } = false;
    }
}