using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace BankExc
{
    [Serializable]
    public class Bank : IBank,ISerializable
    {
        [XmlAttribute]
        #region
        public string Name { get; }
        public string Address { get; }
        public int CustomerCount { get=>customerLst.Count; }
        private List<Account> accountLst = new List<Account>();
        private List<Customer> customerLst = new List<Customer>();
        private Dictionary<int, Customer> dicCustomerByID = new Dictionary<int, Customer>();
        private Dictionary<int, Customer> dicCustomerByNumber = new Dictionary<int, Customer>();
        private Dictionary<int, Account> dicByAccountNum = new Dictionary<int, Account>();
        private Dictionary<Customer, List<Account>> customerAccounts = new Dictionary<Customer, List<Account>>();
        private int totalMoneyInBank;
        private int profits;
        #endregion
        #region
        public Bank()
        {

        }
        internal Bank(string name,string address)
        {
            this.Name = name;
            this.Address = address;
        }
        #endregion
        internal void AddnewCustomer(Customer customer)
        {
            if ((customerLst.Contains(customer)))
            {
                throw new CustomerAlreadyExistException();
               
            }
            customerLst.Add(customer);
            dicCustomerByID.Add(customer["ID"], customer);
            dicCustomerByNumber.Add(customer["NUMBER"], customer);                       
            foreach (Customer item in customerLst)// this is for testing
            {
                Console.WriteLine(item);
            }
        }
        internal void AddNewAccount(Account account,Customer customer)
        {

            if (!customerLst.Contains(customer))
            {
                throw new CustomerNotFoundException();
            }
            if (accountLst.Contains(account))
            {
                AccountAlreadyExistException e = new AccountAlreadyExistException();
               
            }            
            accountLst.Add(account);
            dicByAccountNum.Add(account.AccountNumber, account);
            List<Account> totalCustomerAccounts = new List<Account>();
            totalCustomerAccounts.AddRange(accountLst.FindAll(p => p.AccountOwner == customer));
            if (customerAccounts.ContainsKey(customer))
            {
                customerAccounts[customer].Add(account);
            }
            else
            {
                customerAccounts.Add(customer,totalCustomerAccounts);
            }          
        }
        internal void CloseAccount(Customer customer,Account account)
        {
           
            if (!accountLst.Contains(account))
            {
                AccountNotFoundException e = new AccountNotFoundException();               
            }
            if (!customerLst.Contains(customer))
            {
                CustomerNotFoundException e = new CustomerNotFoundException();
            }
            accountLst.Remove(account);
            customerAccounts[customer].Remove(account);//fixed          
            dicByAccountNum.Remove(account.AccountNumber);           
        }
        internal int GetCustomerTotalBalance(Customer customer)
        {
            if (!customerLst.Contains(customer))
            {
                CustomerNotFoundException e = new CustomerNotFoundException();
            }
            if (customerAccounts[customer] == null)
                throw new AccountNotFoundException();
            int total=0;
             foreach (Account item in customerAccounts[customer])
            {
                total += item.Balance;
            }

            return total;
            
        }
        internal Customer GetCustomerByID(int id)
        {


            foreach (Customer item in customerLst)
            {
                if (item.CustomerID == id)
                {
                    return item;
                }
            }
             throw new CustomerNotFoundException();            
        }
        internal Customer GetCustomerByNumber(int number)
        {
            foreach (Customer item in customerLst)
            {
                if (item.CustomerNumber == number)
                {
                    return item;
                }
            }
            CustomerNotFoundException e = new CustomerNotFoundException();
            Console.WriteLine(e.Message + "/n" + e.StackTrace);
            throw e;
        }
        internal Account GetAccountByNumber(int number)
        {
            foreach (Account item in accountLst)
            {
                if (item.AccountNumber == number)
                {
                    return item;
                }
            }
            throw new AccountNotFoundException();         
        }
        internal List<Account> GetAccountsByCustomer(Customer customer)
        {
            if (!customerLst.Contains(customer))
            {
                CustomerNotFoundException e = new CustomerNotFoundException();
                Console.WriteLine(e.Message + "/n" + e.StackTrace);
                throw e;
            }
            List<Account> accounts = new List<Account>();
            accounts.AddRange(customerAccounts[customer]);
            if (accounts == null)
            {
                AccountNotFoundException e = new AccountNotFoundException();
                Console.WriteLine(e.Message + "/n" + e.StackTrace);
                throw e;
            }
            return accounts;
        }
        internal void Deposite(int amount, Account account)
        {
            if (account == null)
            {
               throw new AccountNotFoundException();                
            }
            account.Add(amount);
            totalMoneyInBank += amount;
        }
        internal void Withdraw(int amount, Account account)
        {
            if (account == null)
            {
                AccountNotFoundException e = new AccountNotFoundException();
                Console.WriteLine(e.Message+"/n"+ e.StackTrace);
                throw e;
            }
            if ((account.Balance - amount) >= (account.MaxMinusAllowed * -1))
            {
                account.Subtract(amount);
                totalMoneyInBank -= amount;
            }
            else
            {
                throw new BalanceException();
            }
            
        }
        internal void ChargeAnnualComission(float precentage)
        {
            Console.WriteLine(totalMoneyInBank+" total");//test
            foreach (Account account in accountLst)
            {
                float comission = (precentage / 100f);
                if ((account.MaxMinusAllowed * -1) >= account.Balance)
                {
                    profits += Math.Abs(Convert.ToInt32(account.Balance * comission *2));
                    account.Subtract(Math.Abs(Convert.ToInt32(account.Balance * comission * 2)));
                }
                else
                {
                    profits += Math.Abs(Convert.ToInt32(account.Balance * comission));
                    account.Subtract(Math.Abs(Convert.ToInt32(account.Balance * comission )));
                }
            }
            Console.WriteLine(this.profits+" profits");//testing
            
        }
        internal void JoinAccounts(Account x, Account y)//done
        {
            if (x.AccountOwner != y.AccountOwner)
            throw new NotSameCustomerException();
                // nextln might cause bug   
                var accountX = x + y;
                AddNewAccount(accountX,x.AccountOwner);
                CloseAccount(x.AccountOwner,x);
                CloseAccount(y.AccountOwner, y);
           

        }
        internal void ShowAccounts()//this is for testing
        {
            Console.WriteLine();
            Console.WriteLine();
            foreach (KeyValuePair<Customer,List<Account>> item in customerAccounts)
            {
                Console.WriteLine(item.Key);
                foreach (Account account in customerAccounts[item.Key])
                {
                    Console.WriteLine(account);
                }
            }
        }
        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Name", this.Name);
            info.AddValue("Address", this.Address);
            info.AddValue("Customers count",this.CustomerCount);
            info.AddValue("Customers and accounts", this.customerAccounts);
            info.AddValue("Total money in the bank", this.totalMoneyInBank);
            info.AddValue("Profits", this.profits);

        }
       public void SaveXml()
        {
          //not done
            const string path = @"C:\CreatedFiles\Bank.xml";
            XmlSerializer BankSerializer = new XmlSerializer(typeof(Bank));
            using (TextWriter file=new StreamWriter(path))
            {
                BankSerializer.Serialize(file,this);
            }
            Console.WriteLine("Serialization-Done");
           
        }//not done
        public void BSerialize()
        {
          using ( Stream bStream = File.Open("BankData.dat", FileMode.OpenOrCreate))//TODo to ask what is wrong
          {
              BinaryFormatter bf = new BinaryFormatter();
              bf.Serialize(bStream, this);               
                bStream.Close();
          }
        }//TODO to ask what is wrong
        public void ReSerialize()
        {
            Stream binStream = File.Open("BankData.dat", FileMode.Create);
            BinaryFormatter frmt = new BinaryFormatter();
            var b2 = (Bank)frmt.Deserialize(binStream);
            binStream.Close();
            Console.WriteLine("------------");
            b2.ShowAccounts();
        }//TODO to ask what is wrong
     
    }
}
