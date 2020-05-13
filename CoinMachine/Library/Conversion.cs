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
        ConfigManager configmanager = new ConfigManager();
        
        int seconds = 0;
        int minutes = 0;

        public int getMinutes(float money)
        {
           
            //1 peso es igual a _____ minuto
            money = money * float.Parse(configmanager.ReadSetting("CoinMinute"), CultureInfo.InvariantCulture.NumberFormat);
            int minutes = (int)Math.Ceiling(money);
            return minutes;
        }
        public float getMoney( double minutes)
        {

            float   money = Convert.ToSingle(minutes / Convert.ToDouble(configmanager.ReadSetting("CoinMinute")));
            

            return money;
        }

    }
}
