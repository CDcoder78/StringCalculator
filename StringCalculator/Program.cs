using System;
using System.Text.RegularExpressions;
using Calculator;
using Calculator.Contracts;
using SimpleInjector;

namespace StringCalculator
{
    public class Program
    {
        private const string _menuText =
            @"**********************************************
* String Calculator - exit: Ctrl+C           *
* Supported: delimiters {0}        *
* Mode: Addition                             *
**********************************************";

        private static void Main(string[] args)
        {
            var done = false;
            var defaultColor = Console.ForegroundColor;

            // Stretch goal #4 DI IoC container
            var container = new Container();
            container.Register<IParser, Parser>(Lifestyle.Singleton);
            container.Register<IAdd, Add>(Lifestyle.Singleton);
            container.Verify();

            // Stretch goal #2 exit with Ctrl+C
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                done = true;
                Console.WriteLine("Exiting.");
            };

            void PrintMenu()
            {
                Console.ForegroundColor = ConsoleColor.Yellow;

                Console.WriteLine(_menuText, container.GetInstance<IParser>().GetDelimiters());

                Console.ForegroundColor = defaultColor;
            }

            try
            {
                while (!done)
                {
                    PrintMenu();

                    try
                    {
                        var input = Console.ReadLine();

                        if (input == null)
                            break;
                        
                        Console.WriteLine($"{container.GetInstance<IAdd>().Compute(Regex.Unescape(input))}");
                    }
                    catch (Exception e)
                    {
                        if (!done) Console.WriteLine(e);
                    }
                }
            }
            finally
            {
                // set the color for the console back to the default
                Console.ForegroundColor = defaultColor;
            }
        }
    }
}