using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    class NotSameCustomerException:Exception
    {
        public override string Message => "Customer does not match";
        public NotSameCustomerException()
        {
            Console.WriteLine($"{this.Message}  {this.StackTrace}");
        }
    }
}
