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
        public Action Spend;
        public Action Earned;
        private CountDownTimer cdt;

        public Wallet()
        {
        }

        public float Debit { get; set; }

        public void EarnMoney(float value)
        {
            Debit = Debit + value;
            Earned?.Invoke();
        }

        public void SpendMoney(float value)
        {
            Debit = Debit + value;
            Spend.Invoke();
        }
    }
}