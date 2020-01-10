using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Calculator.Contracts
{
    public class NegativeNumbersException : Exception
    {
        private readonly IList<int> _negativeNumbers;

        public NegativeNumbersException(IList<int> negativeNumbers)
        {
            _negativeNumbers = negativeNumbers ?? throw new ArgumentNullException(nameof(negativeNumbers));
        }

        public override string ToString()
        {
            var negativeNumbers = new StringBuilder();
            foreach (var negativeNumber in _negativeNumbers)
            {
                negativeNumbers.Append($" {negativeNumber}");
            }

            return $"NegativeNumbersException:{negativeNumbers}.";
        }
    }
}