using Library;
using Printer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoinMachine.Library
{
    public class Wallet
    {
        public Action<float> Spend;
        public Action<float> Earned;

        public Wallet()
        {
        }

        public float Debit { get; set; } = 0;

        public void EarnMoney(float value)
        {
            Debit = Debit + value;
            Earned(Debit);
        }

        // type : greyscale or color
        // pages : quantity of pages

        public Boolean EnoughMoney(float printing_cost)
        {
            Boolean result = false;
            if (Debit >= printing_cost)
            {
                result = true;
            }
            return result;
        }

        public void SpendMoney(float value)
        {
            Debit = Debit - value;
            Spend(Debit);
        }
    }
}