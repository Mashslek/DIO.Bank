using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Bank> Bancos = new List<Bank> { new Bank("DIO International Bank"),
                                                 new Bank("Santander"), 
                                                 new Bank("Banco do Brasil") };
            Bancos[0].BankAcounts.Add(new BankAccount("Victor Hugo", 1500.50, 500, AccountType.LegalPerson));
            string strInput = "";
            int choosenBankId = int.MinValue;
            do
            {
                if (strInput == "")
                    showBankOptMenu(Bancos);
                do
                {                    
                    if (choosenBankId == int.MinValue)
                    {
                        strInput = Console.ReadLine();
                        int.TryParse(strInput, out choosenBankId);
                    }    
                    else
                    {
                        strInput = Console.ReadLine();
                        Console.WriteLine("Opção inválida");
                        showBankOptMenu(Bancos);
                        int.TryParse(strInput, out choosenBankId);
                    }
                } while (strInput != "x" && choosenBankId <= 0 && Bancos.Count() > choosenBankId);
                if (strInput != "x")
                {
                    Bank choosenBank = Bancos[choosenBankId - 1];
                    Console.WriteLine("Selectione a Opcao");
                    Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(BankManagementOptions)));
                    int bankAccessOpt = 0;
                    do
                    {
                        if (bankAccessOpt == 0)
                        {
                            strInput = Console.ReadLine();
                            int.TryParse(strInput, out bankAccessOpt);
                        }
                        else
                        {
                            strInput = Console.ReadLine();
                            Console.WriteLine("Opção inválida");
                            Console.WriteLine("Selectione a Opcao");
                            Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(BankManagementOptions)));
                            int.TryParse(strInput, out bankAccessOpt);
                        }
                    } while (!Enum.IsDefined(typeof(BankManagementOptions), bankAccessOpt));
                    switch ((BankManagementOptions)bankAccessOpt)
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
                                    Console.WriteLine("Conta Não encontrada\n Favor digite novamente o numero da conta");
                                    accountKey = Console.ReadLine();
                                }
                            }while(accountKey!= "x" && choosenBank.BankAcounts.FindIndex(x => x.AccountNumber.ToString() == accountKey) != 1);
                            if (choosenBank.BankAcounts.FindIndex(x => x.AccountNumber.ToString() == accountKey) != 1) 
                            {
                                BankAccount bankAccount = choosenBank.BankAcounts.Find(x => x.AccountNumber.ToString() == accountKey);
                                Console.WriteLine("Selectione a Opcao");
                                Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(AccountManagementOptions)));
                                string accountOpt = null;
                                do
                                {
                                    if (accountOpt == null)
                                    {
                                        accountOpt = Console.ReadLine();
                                    }
                                    else
                                    {
                                        accountOpt = Console.ReadLine();
                                        Console.WriteLine("Opção inválida");
                                        Console.WriteLine("Selectione a Opcao");
                                        Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(AccountManagementOptions)));
                                    }
                                } while (!accountOpt.Any(x => !char.IsDigit(x)) && !Enum.IsDefined(typeof(AccountManagementOptions), int.Parse(accountOpt)));
                                switch ((AccountManagementOptions)int.Parse(accountOpt)) 
                                {
                                    case AccountManagementOptions.GetBalance:
                                        Console.WriteLine("Seu saldo é R${0}",bankAccount.Balance);
                                        break;
                                    case AccountManagementOptions.Deposit:
                                        Console.WriteLine("Digite o Valor do deposito");
                                        bankAccount.Deposit(double.Parse(Console.ReadLine()));
                                        break;
                                    case AccountManagementOptions.Transfer:
                                        Console.WriteLine("Digite o Valor da tranferencia");
                                        bankAccount.Deposit(double.Parse(Console.ReadLine()));
                                        break;
                                    case AccountManagementOptions.Withdraw:
                                        Console.WriteLine("Digite o Valor do saque");
                                        bankAccount.Deposit(double.Parse(Console.ReadLine()));
                                        break;
                                    case AccountManagementOptions.Exit:
                                        break;
                                }
                            }
                            break;
                        case BankManagementOptions.DeleteAccount:
                            choosenBank.RemoveAccount();
                            break;
                        case BankManagementOptions.InsertAccount:
                            choosenBank.AddNewAccount();
                            break;
                        case BankManagementOptions.ListAccounts:
                            Console.WriteLine( choosenBank.BankAcounts.ToString());
                            break;
                        case BankManagementOptions.Exit:

                            break;
                    }
                }
            } while (strInput != "x");
            
        }
        static void showBankOptMenu(List<Bank> Bancos) {
            Console.WriteLine("Selecione seu Banco");
            Console.WriteLine(string.Join('\n', Bancos.Select((x, index) => (index + 1).ToString() + " - " + x.Name)));
        }
    }
}
