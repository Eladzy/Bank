using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankExc
{
    [Serializable]
    public class Account
    {
        private static int numberOfAcc=0;
        private readonly int accountNumber;
        private readonly Customer accountOwner;
        private int maxMinusAllowed;
        public int AccountNumber { get=>accountNumber; }
        public int Balance { get; private set; }
        public Customer AccountOwner { get=>accountOwner; }
        public int MaxMinusAllowed { get => maxMinusAllowed; }
        public Account(Customer customer,int monthlyIncome)
        {
            this.accountOwner = customer;
            this.accountNumber = numberOfAcc;
            this.maxMinusAllowed = monthlyIncome * 3;
            numberOfAcc++;
        }
        public void Add(int amount)
        {
            this.Balance += amount;
        }
        public void Subtract(int amount)
        {
            this.Balance -= amount;
        }
        public static bool operator ==(Account a, Account b)
        {
            if (Equals(b, null))
                return false;
            if (a.AccountNumber == b.AccountNumber)
                return true;
            return false;
        }
        public static bool operator !=(Account a, Account b)
        {
            if(Equals(b, null))
                return true;
            if (a.AccountNumber != b.AccountNumber)
                return true;
            return false;
        }
        public override bool Equals(object obj)
        {
            if (Equals(obj, null))
                return false;
            Account a = obj as Account;
            if (!Equals(a, null))
            {
                return this.AccountNumber == a.AccountNumber;
            }
            return false;
        }
        public override int GetHashCode()
        {
            return this.AccountNumber;
        }
        public static Account operator +(Account a,Account b)
        {
            Account account = new Account(a.accountOwner, ((a.MaxMinusAllowed / 3) + (b.MaxMinusAllowed / 3)));
            account.Balance = a.Balance + b.Balance;
            return account;
        }
        public override string ToString()//for testing
        {
            return $"Owner {accountOwner} Number {AccountNumber} Balance {Balance}  MaxMinus {maxMinusAllowed}";
        }
    }   
}
