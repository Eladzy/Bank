using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    class CustomerAlreadyExistException:Exception
    {
        public override string Message => "Customer Already Exist";
            public CustomerAlreadyExistException()
            {
                Console.WriteLine($"{this.Message}  {this.StackTrace}");
            }
    }
}
