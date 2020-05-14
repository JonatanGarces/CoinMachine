using Library;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine.Library
{
    internal class Conversion
    {
        private ConfigManager configmanager = new ConfigManager();

        public float getMinutes(float money)
        {
            //1 peso es igual a _____ minuto
            money = money * float.Parse(configmanager.ReadSetting("CoinMinute"), CultureInfo.InvariantCulture.NumberFormat);
            return money;
        }

        public float getMoney(double minutes)
        {
            float money = Convert.ToSingle(minutes / Convert.ToDouble(configmanager.ReadSetting("CoinMinute")));
            return money;
        }
    }
}