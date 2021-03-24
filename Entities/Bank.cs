using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank
{
    class Accounts : List<BankAccount> 
    {
        public override string ToString()
        {
            return string.Join("\n",this.Select(x => x.ToString()));
        }
    }
    class Bank
    {
        public Bank(string name, List<BankAccount> accounts = null)
        {
            this.Name = name;
            this.BankAcounts = accounts == null? new Accounts(): (accounts as Accounts);
        }
        public string Name { get; private set; }
        public Accounts BankAcounts { get; private set; }
        public bool AddNewAccount() 
        {
            AccountType accType;
            Console.WriteLine("Selecione o tipo da Conta:");
            Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(AccountType)));
            int chooseOpt;
            while(!((int.TryParse(Console.ReadLine(), out chooseOpt) && Enum.IsDefined(typeof(AccountType), chooseOpt))))
                Console.WriteLine("Opção Invalida");
            accType = (AccountType)chooseOpt;
            Console.WriteLine("Insira o nome do Cliente:");
            string choosenName = "";
            do
            {
                if (choosenName == "")
                    choosenName = Console.ReadLine();
                else
                {
                    Console.WriteLine("Cliente já Cadastrado ! \n Deseja Cancelar a operação ? \n Tecle 'x' para Sair. Caso Queira prosseguir insira outro nome");
                    choosenName = Console.ReadLine();
                    if (choosenName.ToLower() == "x")
                    {
                        Console.WriteLine("Cadastro não terminado");
                        return false;
                    }
                }
            } while (BankAcounts.FindIndex(x => x.AccountOwnerName == choosenName) != -1);
            double choosenBalance = 0;
            Console.WriteLine("Insira o Saldo Inicial:");
            while(!double.TryParse(Console.ReadLine().Replace(',','.'),out choosenBalance))
                Console.WriteLine("Valor Invalido");
            double choosenCredit = 0;
            Console.WriteLine("Insira o Saldo Inicial:");
            while (!double.TryParse(Console.ReadLine().Replace(',', '.'), out choosenCredit))
                Console.WriteLine("Valor Invalido");
            this.BankAcounts.Add(new BankAccount(choosenName, choosenCredit, choosenBalance, accType));
            return true;
        }
        public void RemoveAccount()
        {
            string temp = "";
            Console.WriteLine("Favor digite o nome do Cliente ou o número da Conta");
            do
            {
                if (temp == "")
                    temp = Console.ReadLine();
                else
                {
                    Console.WriteLine("Cliente não encontrado ! \n Deseja Cancelar a operação ? \n Tecle 'x' para Sair. Caso Queira prosseguir insira outro nome ou numero de conta");
                    temp = Console.ReadLine();
                    if (temp.ToLower() == "x")
                    {
                        Console.WriteLine("Nenhum cliente removido");
                        return ;
                    }
                }
            } while (this.BankAcounts.FindIndex(x => (temp == x.AccountOwnerName || temp == x.AccountNumber.ToString())) == -1);
            this.BankAcounts = (Accounts)this.BankAcounts.Where(x => !(temp == x.AccountOwnerName || temp == x.AccountNumber.ToString())).ToList();
        }
        public string ListAccounts() {
            return this.BankAcounts.ToString();
        }
    }
}
