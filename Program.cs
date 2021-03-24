using System;
using System.Collections.Generic;

namespace Bank
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(new BankAccount("Victor Hugo",1500.50,500,AccountType.LegalPerson).ToString());
            Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(BankManagementOptions)));
            Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(AccountType)));
            Console.WriteLine(EnumsUtils.GetEnumAsOptList(typeof(AccountManagementOptions)));
        }
    }
}
