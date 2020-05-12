using Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine.Library
{
    class Conversion
    {
        ConfigManager configmanager =new ConfigManager();
        float money = 0;
        int seconds = 0;
        int minutes = 0;

        public int getSeconds(float money)
        {
            //1 peso es igual a _____ minuto
            money = money * float.Parse(configmanager.ReadSetting("CoinMinute"), CultureInfo.InvariantCulture.NumberFormat)*60;
            int seconds = (int)Math.Ceiling(money);
            return seconds;
        }

    }
}
