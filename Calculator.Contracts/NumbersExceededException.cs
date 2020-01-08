using System;

namespace Calculator.Contracts
{
    public class NumbersExceededException : Exception
    {
        private readonly int _maximumNumbers;
        private readonly int _numbers;

        public NumbersExceededException(int maximumNumbers, int numbers)
        {
            _maximumNumbers = maximumNumbers;
            _numbers = numbers;
        }

        public override string ToString()
        {
            return $"Maximum numbers exceeded! Expected {_maximumNumbers} or less, received {_numbers}.";
        }
    }
}