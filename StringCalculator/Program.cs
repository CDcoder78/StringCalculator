using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Calculator;
using Calculator.Contracts;
using SimpleInjector;

namespace StringCalculator
{
    public class Program
    {
        private const string _menuText =
            @"*****************************************************************************************************
*          String Calculator - exit: Ctrl+C           
*          Supported: delimiters {0}        
*          Mode: {1}                             
*          Deny Negatives: {2}           Inputs Upper Bound: {3}
*
{4}
*****************************************************************************************************";

        private static void Main(string[] args)
        {
            var done = false;
            var defaultColor = Console.ForegroundColor;

            // Stretch goal #4 DI IoC container
            var container = new Container();
            container.Register<IParser, Parser>(Lifestyle.Singleton);
            container.Register<IAdd, Add>(Lifestyle.Singleton);
            container.Register<ISubtraction, Subtraction>(Lifestyle.Singleton);
            container.Register<IMultiplication, Multiplication>(Lifestyle.Singleton);
            container.Register<IDivision, Division>(Lifestyle.Singleton);
            container.Verify();

            var modes = new Dictionary<ComputeTypes, ICompute>()
            {
                { ComputeTypes.Add, container.GetInstance<IAdd>() },
                { ComputeTypes.Subtract, container.GetInstance<ISubtraction>() },
                { ComputeTypes.Division, container.GetInstance<IDivision>() },
                { ComputeTypes.Multiplication, container.GetInstance<IMultiplication>() }
            };


            ICompute mode = modes[ComputeTypes.Add];

            // Stretch goal #2 exit with Ctrl+C
            Console.CancelKeyPress += (sender, eventArgs) =>
            {
                done = true;
                Console.WriteLine("Exiting.");
            };

            void PrintMenu()
            {

                Console.ForegroundColor = ConsoleColor.Yellow;

                var parser = container.GetInstance<IParser>();

                Console.WriteLine(_menuText, parser.GetDelimiters(), parser.CurrentMode, parser.DenyNegative, parser.UpperBound, parser.GetCommandText());

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

                        var unescapeInput = Regex.Unescape(input);

                        if (!container.GetInstance<IParser>().HandleCommand(unescapeInput))
                        {
                            Console.WriteLine($"{mode.Compute(unescapeInput)}");
                        }
                        else
                        {
                            mode = modes[container.GetInstance<IParser>().CurrentMode];
                        }
                    }
                    catch (Exception e)
                    {
                        if (!done) Console.WriteLine($"{e.GetType()}: {e.Message}");
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