using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    class BalanceException:Exception
    {
        public override string Message{ get=>"Insufficient Funds";  }
        public BalanceException()
        {
            Console.WriteLine($"{this.Message}  {this.StackTrace}");
        }
    }
}
