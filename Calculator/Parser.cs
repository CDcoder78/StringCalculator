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
        private const string AddDelimiter = "AddDelimiter";
        private const string RemoveDelimiter = "RemoveDelimiter";
        private const string DenyNegatives = "DenyNegatives";
        private const string AllowNegatives = "AllowNegatives";
        private const string UpperBounds = "UpperBounds";
        private const char DefaultDelimiter = ',';
        private bool _denyNegative = true;
        private uint _upperBound = 1000;

        /// '\n' => "\\n" == on Windows Environment.NewLine is "\r\n"   
        private List<char> _delimiters;

        private char[] _commandDelimiters = new[] {'-', ':'};

        public bool DenyNegative
        {
            get => _denyNegative;
            set => _denyNegative = value;
        }

        public uint UpperBound
        {
            get => _upperBound;
            set
            {
                if (value == 0)
                {
                    throw new ArgumentException($"Invalid upper bound value: {value}");
                }

                _upperBound = value;
            }
        }

        public Parser()
        {
            var newline = Environment.NewLine.ToCharArray();
            _delimiters = new List<char>(newline);
            _delimiters.Add(DefaultDelimiter);
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

            var customDelimiters = default(char[]);

            if (input.Length >= 4 && input.StartsWith("//") && input[3] == '\n')
            {
                customDelimiters = new char[] {input[2]};

                input = input.Substring(4);
            }

            var strings = input.Split(customDelimiters ?? _delimiters.ToArray());

            var negativeNumbers = _denyNegative ? new List<int>() : default(IList<int>);

            var results = strings.Select(
                a =>
                {
                    int.TryParse(a, out var result);

                    if (result < 0)
                    {
                        negativeNumbers?.Add(result);
                    }

                    if (_upperBound >= 1 && result > _upperBound)
                    {
                        return 0;
                    }

                    return result;
                }).ToArray();

            if (negativeNumbers?.Count > 0)
            {
                throw new NegativeNumbersException(negativeNumbers);
            }

            return results;
        }

        public bool HandleCommand(string input)
        {
            bool HandleDelimiter(string argument, bool add)
            {
                var delimiter = GetCharArgument(argument);
                if (delimiter.HasValue)
                {
                    if (add)
                    {
                        if (!_delimiters.Contains(delimiter.Value))
                        {
                            _delimiters.Add(delimiter.Value);
                        }
                    }
                    else
                    {
                        _delimiters.RemoveAll(a => a == delimiter.Value);
                    }
                    
                    return true;
                }

                return false;
            }

            var commandStrings = input.Split(_commandDelimiters);
            
            if (commandStrings.Length > 1)
            {
                switch (commandStrings[1])
                {
                    case AddDelimiter:
                    case RemoveDelimiter:
                        if (commandStrings.Length == 3 && 
                            HandleDelimiter(commandStrings[2], commandStrings[1].Equals(AddDelimiter)))
                        {
                            return true;
                        }
                        break;

                    case DenyNegatives:
                        if (commandStrings.Length == 2)
                        {
                            DenyNegative = true;
                            return true;
                        }

                        break;
                    case AllowNegatives:
                        if (commandStrings.Length == 2)
                        {
                            DenyNegative = false;
                            return true;
                        }

                        break;
                    case UpperBounds:
                        if (commandStrings.Length == 3)
                        {
                            var upperBounds = GetUIntArgument(commandStrings[2]);

                            if (upperBounds.HasValue)
                            {
                                UpperBound = upperBounds.Value;
                                return true;
                            }
                        }
                        
                        break;
                }

                throw new ArgumentException($"Command Invalid: {input}");
            }

            return false;
        }

        public string GetCommandText()
        {
            return $@"*             Commands                            Usage
*           {AddDelimiter}                       -{AddDelimiter}:'x' 
*           {RemoveDelimiter}                    -{RemoveDelimiter}:'x'
*           {DenyNegatives}                      -{DenyNegatives} 
*           {AllowNegatives}                     -{AllowNegatives}
*           {UpperBounds}                        -{UpperBounds}:1000";
        }

        private char? GetCharArgument(string argument)
        {
            if (argument.Length == 3 && 
                argument[0] == '\'' &&
                argument[2] == '\'')
            {
                if (argument[1] != ' ' && !int.TryParse(argument[1].ToString(), out var _ ))
                {
                    return argument[1];
                }
            }

            return null;
        }

        private uint? GetUIntArgument(string argument)
        {
            if (uint.TryParse(argument, out var result))
            {
                return result;
            }

            return null;
        }
    }
}