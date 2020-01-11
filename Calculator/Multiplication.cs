using System;
using System.Linq;
using System.Text;
using Calculator.Contracts;

namespace Calculator
{
    public class Multiplication : IMultiplication
    {
        private const char MultiplicationSymbol = '*';
        private const char EqualsSymbol = '=';
        private readonly IParser _parser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Multiplication" /> class.
        /// </summary>
        /// <param name="parser">String parser instance.</param>
        public Multiplication(IParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
        }

        /// <summary>
        ///     Computes value from input string.
        /// </summary>
        /// <param name="input">String to parser and compute.</param>
        /// <returns>Formatted computed string.</returns>
        public string Compute(string input)
        {
            var args = _parser.ParseIntegers(input);
            var result = args[0];
            var formula = new StringBuilder();
            formula.Append(args[0]);
            for (var i = 1; i < args.Length; ++i)
            {
                formula.Append(MultiplicationSymbol);
                formula.Append(args[i]);

                result *= args[i];
            }

            // Stretch goal #1 formula with result
            return $"{formula} {EqualsSymbol} {result}";
        }
    }
}