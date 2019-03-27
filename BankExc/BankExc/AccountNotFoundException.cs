using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    class AccountNotFoundException:Exception
    {
        public override string Message => "Account Not Found";
        public AccountNotFoundException()
        {
            Console.WriteLine($"{this.Message}  {this.StackTrace}");
        }
    }
}
