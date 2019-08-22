using System;

namespace CQRS_Example
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                string command = null;
                var ledger = new Ledger();
                var adder = new Adder(ledger);
                var adderQuery = new AdderQuery(ledger);
                do
                {
                    Console.Write($"Please enter a command:\n{string.Join("\n", Enum.GetNames(typeof(Options)))}. \nCommand > ");
                    command = Console.ReadLine();
                    switch (Enum.Parse<Options>(command))
                    {
                        case Options.AddCommand:
                            adder.Add(GetNumber());
                            break;
                        case Options.AddAtCommand:
                            adder.AddAt(GetNumber(), GetIndex());
                            break;
                        case Options.UpdateCommand:
                            adder.Update(GetNumber(), GetIndex());
                            break;
                        case Options.DeleteCommand:
                            adder.Delete(GetIndex("delete"));
                            break;
                        case Options.DisplayQuery:
                            Console.WriteLine(adderQuery.Display());
                            break;
                        case Options.SumQuery:
                            Console.WriteLine(adderQuery.Sum());
                            break;
                    }

                } while (Enum.Parse<Options>(command) != Options.QuitCommand);
            }
            catch (Exception e)
            {
            }
        }

        private static int GetNumber()
        {
            Console.Write("What number would you like to add: ");
            int number = int.Parse(Console.ReadLine());
            return number;
        }

        private static int GetIndex(string key = "add")
        {
            Console.Write($"Where would you like to {key}: ");
            int number = int.Parse(Console.ReadLine());
            return number;
        }

        enum Options
        {
            QuitCommand,
            AddCommand,
            AddAtCommand,
            UpdateCommand,
            DeleteCommand,
            SumQuery,
            DisplayQuery
        }
    }
}
