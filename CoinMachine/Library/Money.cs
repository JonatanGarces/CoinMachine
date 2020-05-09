using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine.Library
{
    internal class Money
    {
        private float money;
        public Action Spent;
        public Action Earned;

        public float Wallet
        {
            get { return money; }
            set { money = value; }
        }

        public void EarnMoney(float value)
        {
            money = money + value;
            Earned?.Invoke();
        }

        public void SpendMoney(float value)
        {
            money = money + value;
            Spent.Invoke();
        }
    }
}