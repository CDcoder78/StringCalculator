using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using Calculator.Contracts;

namespace Calculator
{
    public class Parser : IParser
    {
        private const char DefaultDelimiter = ',';
        private bool _denyNegative = true;

        /// '\n' => "\\n" == on Windows Environment.NewLine is "\r\n"   
        private char[] _delimiters;

        public bool DenyNegative => _denyNegative;

        public Parser()
        {
            var newline = Environment.NewLine.ToCharArray();
            _delimiters = new char[newline.Length + 1 ];

            Array.Copy(newline, _delimiters, newline.Length);
            _delimiters[^1] = DefaultDelimiter;
        }

        public string GetDelimiters()
        {
            var delimiters = new StringBuilder();

            foreach (var del in _delimiters)
            {
                delimiters.Append($"'{Regex.Escape(del.ToString())}' ");
            }

            return delimiters.ToString().TrimEnd();
        }

        public int[] ParseIntegers(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var strings = input.Split(_delimiters);

            var negativeNumbers = _denyNegative ? new List<int>() : default(IList<int>);

            var results = strings.Select(
                a =>
                {
                    int.TryParse(a, out var result);

                    if (result < 0)
                    {
                        negativeNumbers?.Add(result);
                    }

                    return result;
                }).ToArray();

            if (negativeNumbers?.Count > 0)
            {
                throw new NegativeNumbersException(negativeNumbers);
            }

            return results;
        }

        public void SetDenyNegative(bool state)
        {
            _denyNegative = state;
        }
    }
}