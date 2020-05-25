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

        public Boolean EnoughMoney(PrinterHelper.PageColor PageColor, PrinterHelper.PageDisplayFlags PageDisplayFlags, int? pages, float greyscale_cost, float color_cost)
        {
            return true;
        }

        public void SpendMoney(float value)
        {
            Debit = Debit - value;
            Spend(Debit);
        }
    }
}