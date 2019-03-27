using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Windows.Forms;

namespace BankExc
{
    [Serializable]
    public class Customer
    {
        private static int _numberOfCust=1;
        private readonly int _customerID;
        private readonly int _customerNumber;
        public string Name { get;private set; } 
        public int PhNumber { get; private set; }
        public int CustomerID { get=>_customerID; }
        public int CustomerNumber { get=>_customerNumber; }
        public int this[string attribute]
        {
            get
            {
                switch(attribute.ToLower())
                {
                    case "id":
                        return CustomerID;
                    case "number": return CustomerNumber;
                    default:
                        throw new CustomerNotFoundException();

                }
            }
        }
        public Customer( int customerID,string name,int phone)
        {
            _customerID = customerID;
            this.Name = name;
            this.PhNumber = phone;
            this._customerNumber = _numberOfCust;
            _numberOfCust++;
        }
        
        public static bool operator==(Customer a,Customer b)
        {
            if (Equals(b, null))
                return false;
            if (a.CustomerNumber == b.CustomerNumber)
                return true;
            return false;
        }
        public static bool operator !=(Customer a, Customer b)
        {
            if (Equals(b, null))
                return true;
            if (a.CustomerNumber != b.CustomerNumber)
                return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            if (Equals(obj, null))
                return false;
            Customer c = obj as Customer;
            if (!Equals(c, null))
            {
                return this.CustomerNumber == c.CustomerNumber;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.CustomerNumber;
        }
        public override string ToString()
        {
            return $"Name-{Name} Phone-{PhNumber} ID-{CustomerID} Number-{CustomerNumber}";
        }
    }
}
