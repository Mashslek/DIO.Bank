using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank;

namespace Bank.Controlers
{
    abstract class Menu
    {
        public Menu(OptionChooseFunction choosenOptFunction,string exitMenuInput)
        {
            this.ExitMenuInput = exitMenuInput;
            this.ChoosenOptFunction = choosenOptFunction;     
        }
        protected abstract void ShowMenuOptions();
        public delegate void OptionChooseFunction(string input);
        public delegate bool ValidateInput(string input);
        protected abstract bool IsValidInput(string input);
        protected OptionChooseFunction ChoosenOptFunction;
        protected string ExitMenuInput;
        public void InstanceMenu() 
        {
            string choosenOpt = null;
            Console.WriteLine("Selecione uma das Opções abaixo : ");
            this.ShowMenuOptions();
            do
            {
                do
                {
                    if (choosenOpt == null)
                        choosenOpt = Console.ReadLine();
                    else
                    {
                        if(!this.IsValidInput(choosenOpt))
                            Console.WriteLine("Opção Invalida");
                        Console.WriteLine("\nSelecione uma das Opções abaixo : ");
                        this.ShowMenuOptions();
                        choosenOpt = Console.ReadLine();
                    }
                }
                while (!this.IsValidInput(choosenOpt));
                if(this.ExitMenuInput != choosenOpt)
                    this.ChoosenOptFunction(choosenOpt);
            } while (this.ExitMenuInput != choosenOpt);
        }
    }
    class EnumMenu: Menu
    {
        public EnumMenu(Type enumBase, OptionChooseFunction choosenOptFunction, string exitMenuInput):base(choosenOptFunction, exitMenuInput)
        {
            this.EnumBase = enumBase;
        }
        private Type EnumBase;
        protected override void ShowMenuOptions()
        {
            Console.WriteLine(EnumsUtils.GetEnumAsOptList(this.EnumBase) + "\n"+this.ExitMenuInput + " - Sair");
        }
        protected override bool IsValidInput(string input) {
            int opt;
            if (int.TryParse(input, out opt))
                return Enum.IsDefined(this.EnumBase, opt) || input == this.ExitMenuInput;
            return input == this.ExitMenuInput;
        }
    }
    class DynamicMenu : Menu
    {
        
        public DynamicMenu(Dictionary<string,string>optionsList, OptionChooseFunction choosenOptFunction,  string exitMenuInput) : base(choosenOptFunction, exitMenuInput)
        {
            this.OptionsList = optionsList;
        }
        private Dictionary<string, string> OptionsList;
        protected override void ShowMenuOptions()
        {
            Console.WriteLine(string.Join('\n', this.OptionsList.Select(x => x.Key + " - " + x.Value)) + "\n" + this.ExitMenuInput + " - Sair");
        }
        protected override bool IsValidInput(string input)
        {
            return this.OptionsList.Keys.Contains(input) || input == this.ExitMenuInput;
        }
    }
}
