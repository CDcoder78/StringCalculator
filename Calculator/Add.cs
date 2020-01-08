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
        private const int DefaultMaximumNumbers = 2;
        private readonly int _maximumNumbers;
        private readonly IParser _parser;

        /// <summary>
        ///     Initializes a new instance of the <see cref="Add" /> class.
        /// </summary>
        /// <param name="parser">String parser instance.</param>
        public Add(IParser parser)
        {
            _parser = parser ?? throw new ArgumentNullException(nameof(parser));
            _maximumNumbers = DefaultMaximumNumbers;
        }

        /// <summary>
        ///     Computes value from input string.
        /// </summary>
        /// <param name="input">String to parser and compute.</param>
        /// <returns>Formatted computed string.</returns>
        public string Compute(string input)
        {
            var args = _parser.ParseIntegers(input);

            if (args.Length > _maximumNumbers) throw new NumbersExceededException(_maximumNumbers, args.Length);

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