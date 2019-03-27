using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    class AccountAlreadyExistException:Exception
    {
        public override string Message => "Account Already Exist";
        public AccountAlreadyExistException()
        {
            Console.WriteLine($"{this.Message}  {this.StackTrace}");
        }
    }
}
