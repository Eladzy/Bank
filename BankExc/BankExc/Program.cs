using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Runtime.Serialization.Formatters.Binary;


namespace BankExc
{
    class Program
    {
        static void Main(string[] args)
        {
            Customer c1 = new Customer(1234, "MRT", 7791166);
            Customer c2 = new Customer(1244, "Marcus", 2364532);
            Customer c3 = new Customer(3423, "Maya", 35352);
            Bank b1 = new Bank("GB National Bank","NY");
            b1.AddnewCustomer(c1);
            b1.AddnewCustomer(c2);
            b1.AddnewCustomer(c3);
            Account a1 = new Account(c1, 23000);
            Account a2 = new Account(c1, 54542);
            Account a3 = new Account(c2, 2442);
            Account a4 = new Account(c3, 33535);
            Account a5 = new Account(c1, 43535);         
            b1.AddNewAccount(a1, c1);
            b1.AddNewAccount(a2, c1);
            b1.AddNewAccount(a5, c1);
            b1.AddNewAccount(a3, c2);
            b1.AddNewAccount(a4, c3);
            b1.Deposite(65656, a1);
            b1.Deposite(656, a2);
            b1.Deposite(156, a3);
            b1.Deposite(3256, a4);            
           b1.ChargeAnnualComission(10f);
            Console.WriteLine();
            Console.WriteLine();            
            b1.ShowAccounts();            
            b1.JoinAccounts(a1, a2);
            b1.ShowAccounts();
            Console.WriteLine(b1.GetAccountByNumber(5));
            Console.WriteLine(b1.GetAccountsByCustomer(c3));
            Console.WriteLine(b1.GetCustomerByNumber(2));
            Console.WriteLine(b1.GetCustomerTotalBalance(c2));
            Console.WriteLine(b1.GetCustomerByID(1234));
            Console.WriteLine(c1.GetHashCode());
            // b1.SaveXml();
          
        }
    }
}
