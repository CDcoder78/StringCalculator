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

        /// '\n' => "\\n" == on Windows Environment.NewLine is "\r\n"   
        private char[] _delimiters;

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

            return strings.Select(
                a =>
                {
                    int.TryParse(a, out var result);
                    return result;
                }).ToArray();
        }
    }
}