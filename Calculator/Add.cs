using System;
using System.Linq;
using System.Text;
using Calculator.Contracts;

namespace Calculator
{
    public class Add : IAdd
    {
        private const char AdditionSymbol = '+';
        private const char EqualsSymbol = '=';
        private readonly IParser _parser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Add" /> class.
        /// </summary>
        /// <param name="parser">String parser instance.</param>
        public Add(IParser parser)
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

            var formula = new StringBuilder();
            formula.Append(args[0]);
            for (var i = 1; i < args.Length; ++i)
            {
                formula.Append(AdditionSymbol);
                formula.Append(args[i]);
            }

            // Stretch goal #1 formula with result
            return $"{formula} {EqualsSymbol} {args.Sum()}";
        }
    }
}