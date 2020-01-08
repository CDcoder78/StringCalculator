using System;
using System.Linq;
using Calculator.Contracts;

namespace Calculator
{
    public class Parser : IParser
    {
        private readonly char[] Delimiters = {','};

        public int[] ParseIntegers(string input)
        {
            if (input == null) throw new ArgumentNullException(nameof(input));

            var strings = input.Split(Delimiters);

            return strings.Select(
                a =>
                {
                    int.TryParse(a, out var result);
                    return result;
                }).ToArray();
        }
    }
}