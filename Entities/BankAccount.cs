using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Bank
{
    
    class BankAccount
    {
        public BankAccount(string costumerName, double credit, double balance, AccountType accType)
        {
            this.Balance = balance;
            this.Credit = credit;
            this.AccountOwnerName = costumerName;
            this.Type = accType;
            this.AccountNumber = this.GetHashCode().ToString();
        }
        public string AccountOwnerName { get; private set; }
        public double Balance { get; private set; }
        public double Credit { get; private set; }
        public AccountType Type { get; private set; }
        public string AccountNumber { get; private set; }
        public bool Withdraw(double value) 
        {
            if (this.Credit + this.Balance >= value) {
                this.Balance -= value;
                Console.WriteLine("Saldo atual da conta de {0} é {1}",this.AccountOwnerName,this.Balance);
                return true;
            }
            Console.WriteLine("Saldo Insuficiente");
            return false;
        }
        public void Deposit(double value)
        {
            this.Balance += value;
            Console.WriteLine("Saldo atual da conta de {0} é {1}", this.AccountOwnerName, this.Balance);
        }
        public bool Transfer(double value,ref BankAccount DestinyAccount)
        {
            if (this.Withdraw(value)) 
            {
                DestinyAccount.Deposit(value);
                return true;
            }
            return false;
        }
        public override string ToString()
        {
            return ("Tipo Conta: @AccType | Nome : @Name | Saldo : @Balance | Credito : @Credit | Numero : @Number")
                   .Replace("@AccType", EnumsUtils.GetDescription(this.Type))
                   .Replace("@Name", this.AccountOwnerName)
                   .Replace("@Balance", this.Balance.ToString("0,00"))
                   .Replace("@Credit", this.Credit.ToString("0,00"))
                   .Replace("@Number", this.AccountNumber.ToString());
        }
    }
}
