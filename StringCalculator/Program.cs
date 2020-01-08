using System;
using Calculator;
using Calculator.Contracts;
using SimpleInjector;

namespace StringCalculator
{
    public class Program
    {
        private const string _menuText =
            @"*********************************************
*        String Calculator - exit: Ctrl+C   *
*        Supported: delimiters ','          *
*        Maximum numbers:     2             *
*        Mode: Addition                     *
*********************************************";

        private static void Main(string[] args)
        {
            var done = false;
            var defaultColor = Console.ForegroundColor;

            // Stretch goal #4 DI IoC container
            var container = new Container();
            container.Register<IParser, Parser>();
            container.Register<IAdd, Add>();
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

                Console.WriteLine(_menuText);

                Console.ForegroundColor = defaultColor;
            }

            while (!done)
            {
                PrintMenu();

                try
                {
                    var input = Console.ReadLine();

                    if (input != null)
                    {
                        Console.WriteLine($"{container.GetInstance<IAdd>().Compute(input)}");
                    }
                }
                catch (Exception e)
                {
                    if(!done)
                    {
                        Console.WriteLine(e);
                    }
                }
            }

            // set the color for the console back to the default
            Console.ForegroundColor = defaultColor;
        }
    }
}