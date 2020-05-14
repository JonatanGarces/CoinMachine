using Library;
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

        public void SpendMoney(float value)
        {
            Debit = Debit - value;
            Spend(Debit);
        }
    }
}