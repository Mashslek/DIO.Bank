using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Reflection;

namespace Bank
{
    enum AccountType {[Description("Pessoa Fisica")] PhysicalPerson = 1, [Description("Pessoa Jurídica")] LegalPerson = 2}
    enum BankManagementOptions 
    {
        [Description("Listar Contas")] ListAccounts = 1,
        [Description("Inserir Nova Conta")] InsertAccount = 2,
        [Description("Deletar Conta")] DeleteAccount = 3,
        [Description("Sair")] Exit = 4
    }
    enum AccountManagementOptions
    {
        [Description("Ver Saldo")] GetBalance = 1,
        [Description("Tranferir")] Transfer = 2,
        [Description("Sacar")] Withdraw = 3,
        [Description("Depositar")] Deposit = 4,
        [Description("Sair")] Exit = 5
    }

    static class EnumsUtils
    {
        public static string GetDescription<T>(this T enumerationValue)
        where T : struct
        {
            Type type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "enumerationValue");
            }

            //Tries to find a DescriptionAttribute for a potential friendly name
            //for the enum
            MemberInfo[] memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo != null && memberInfo.Length > 0)
            {
                object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                if (attrs != null && attrs.Length > 0)
                {
                    //Pull out the description value
                    return ((DescriptionAttribute)attrs[0]).Description;
                }
            }
            //If we have no description attribute, just return the ToString of the enum
            return enumerationValue.ToString();
        }
        public static string GetEnumAsOptList(Type EnumType) 
        {
            List<string> result = new List<string>();
            if (EnumType.IsEnum)
            {
                foreach (var enumItem in Enum.GetValues(EnumType))
                {
                    string EnumItemDesc = "";
                    MemberInfo[] memberInfo = EnumType.GetMember(enumItem.ToString());
                    if (memberInfo != null && memberInfo.Length > 0)
                    {
                        object[] attrs = memberInfo[0].GetCustomAttributes(typeof(DescriptionAttribute), false);

                        if (attrs != null && attrs.Length > 0)
                        {
                            EnumItemDesc = ((DescriptionAttribute)attrs[0]).Description;
                        }
                    }
                    else
                        EnumItemDesc = enumItem.ToString();
                    result.Add(((int)enumItem).ToString() + " - " + EnumItemDesc);
                }
            }
            else 
            {
                throw new ArgumentException("EnumerationValue must be of Enum type", "EnumType");
            }
            return string.Join('\n', result);
        } 
    }
}
