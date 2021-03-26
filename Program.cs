using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;
using Bank.Controlers;

namespace Bank
{
    class Program
    {

        static void Main(string[] args)
        {
            BankAccount CurrentAcc = null;
            Bank CurrentBank = null;
            List<Bank> Bancos = new List<Bank> { new Bank("DIO International Bank"),
                                                 new Bank("Santander"),
                                                 new Bank("Banco do Brasil") };
            Bancos[0].BankAcounts.Add(new BankAccount("Victor Hugo", 1500.50, 500, AccountType.LegalPerson));

            /* Account Management Menu Criation */
            EnumMenu AccManagementMenu = new EnumMenu(
                typeof(AccountManagementOptions),
                ipt =>
                    {
                    switch (Enum.Parse(typeof(AccountManagementOptions), ipt))
                    {
                        case AccountManagementOptions.GetBalance:
                            Console.WriteLine("Seu saldo é R${0}", CurrentAcc.Balance);
                            break;
                        case AccountManagementOptions.Deposit:
                            Console.WriteLine("Digite o Valor do deposito");
                            CurrentAcc.Deposit(double.Parse(Console.ReadLine()));
                            break;
                        case AccountManagementOptions.Transfer:
                            Console.WriteLine("Digite o Valor da tranferencia");
                            double tranferValue = double.Parse(Console.ReadLine());
                            Console.WriteLine("Digite o numero da conta do Destinatário");
                            string accNumber = Console.ReadLine();
                            BankAccount Favorecido = CurrentBank.BankAcounts.Find(x => x.AccountNumber == accNumber);
                            CurrentAcc.Transfer(tranferValue, ref Favorecido);
                            CurrentBank.BankAcounts[CurrentBank.BankAcounts.FindIndex(x => x.AccountNumber == accNumber)] = Favorecido;
                            break;
                            case AccountManagementOptions.Withdraw:
                                Console.WriteLine("Digite o Valor do saque");
                                CurrentAcc.Withdraw(double.Parse(Console.ReadLine()));
                                break;
                        }
                    },
                "5"
            );

            /* Bank Management Menu Criation */
            EnumMenu BankManagementMenu = new EnumMenu(
                typeof(BankManagementOptions),
                ipt =>
                {
                    switch (Enum.Parse(typeof(BankManagementOptions), ipt))
                    {
                        case BankManagementOptions.AccessAccount:
                            string accountKey = null;
                            Console.WriteLine("Digite o numero da conta ou precione 'x' para sair");
                            do
                            {
                                if (accountKey == null)
                                    accountKey = Console.ReadLine();
                                else
                                {
                                    Console.WriteLine("Conta Não encontrada\n Favor digite novamente o numero da conta ou precione 'x' para sair");
                                    accountKey = Console.ReadLine();
                                }
                            } while (accountKey != "x" && CurrentBank.BankAcounts.FindIndex(x => x.AccountNumber.ToString() == accountKey) == 1);
                            if (accountKey != "x")
                            {
                                CurrentAcc = CurrentBank.BankAcounts.Find(x => x.AccountNumber.ToString() == accountKey);
                                /* Display Account Management Menu  */
                                AccManagementMenu.InstanceMenu();
                            }
                            break;
                        case BankManagementOptions.DeleteAccount:
                            CurrentBank.RemoveAccount();
                            break;
                        case BankManagementOptions.InsertAccount:
                            CurrentBank.AddNewAccount();
                            break;
                        case BankManagementOptions.ListAccounts:
                            Console.WriteLine(CurrentBank.BankAcounts.ToString());
                            break;
                    }
                },
                "5"
            );

            new DynamicMenu(
                new Dictionary<string,string> (Bancos.Select((x,index) => new KeyValuePair<string, string>(index.ToString(),x.Name))),
                ipt => 
                   {
                       CurrentBank = Bancos[int.Parse(ipt)];
                       BankManagementMenu.InstanceMenu();
                   },
                   "x"
             ).InstanceMenu();    
        }
        static void showBankOptMenu(List<Bank> Bancos) {
            Console.WriteLine("Selecione seu Banco");
            Console.WriteLine(string.Join('\n', Bancos.Select((x, index) => (index + 1).ToString() + " - " + x.Name)));
        }
    }
}
