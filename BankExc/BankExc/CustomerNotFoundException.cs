using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    class CustomerNotFoundException:Exception
    {
        public override string Message => "Customer Not Found";
        public CustomerNotFoundException()
        {
            Console.WriteLine($"{this.Message}  {this.StackTrace}");
        }
    }
}
