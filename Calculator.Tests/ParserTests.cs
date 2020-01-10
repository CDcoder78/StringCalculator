using System;
using System.Text.RegularExpressions;
using Calculator.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Tests
{
    [TestClass]
    public class ParserTests
    {
        private readonly IParser _target = new Parser();

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserInvalidUpperBoundSetArgument()
        {
            _target.UpperBound = 0;
        }

        [TestMethod]
        public void ParserUpperBoundGet()
        {
            Assert.AreEqual(1000u, _target.UpperBound);
        }

        [TestMethod]
        public void ParserValidUpperBoundSet()
        {
            _target.UpperBound = 1;

            Assert.AreEqual(1u, _target.UpperBound);
        }

        [TestMethod]
        public void ParserDenyNegativeSetTrue()
        {
            _target.DenyNegative = true;

            Assert.AreEqual(true, _target.DenyNegative);
        }

        [TestMethod]
        public void ParserDenyNegativeSetFalse()
        {
            _target.DenyNegative = false;

            Assert.AreEqual(false, _target.DenyNegative);
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeNumbersException))]
        public void ParserTestOneNegative()
        {
            _target.ParseIntegers("-1");
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeNumbersException))]
        public void ParserTestOneNegativeOnePositive()
        {
            _target.ParseIntegers("-1,1");
        }

        [TestMethod]
        [ExpectedException(typeof(NegativeNumbersException))]
        public void ParserTestOneNegativeOnePositive1()
        {
            _target.ParseIntegers("-1\n1");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParserTestInvalidArgument()
        {
            _target.ParseIntegers(null);
        }

        [TestMethod]
        public void ParserGetDelimiterTest()
        {
            Assert.AreEqual($"'{Regex.Escape('\r'.ToString())}' '{Regex.Escape('\n'.ToString())}' ','", _target.GetDelimiters());
        }

        [TestMethod]
        public void ParserTest2ValidArguments()
        {
            var input = "1,1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(2, results.Length);
            foreach (var i in results)
            {
                Assert.AreEqual(1, i);
            }
        }

        [TestMethod]
        public void ParserTest4ValidArguments()
        {
            var input = "1,1,1,1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            foreach (var i in results)
            {
                Assert.AreEqual(1, i);
            }
        }

        [TestMethod]
        public void ParserTest1Invalid3ValidArguments()
        {
            var input = "ccccd,1,1,1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(results[0], 0);
            for (int i = 1; i < results.Length; ++i)
            {
                Assert.AreEqual(1, results[i]);
            }
        }

        [TestMethod]
        public void ParserTest3Valid1InvalidArguments()
        {
            var input = "1,1,1, cccd";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(results[results.Length - 1], 0);
            for (int i = 0; i < results.Length - 1; ++i)
            {
                Assert.AreEqual(1, results[i]);
            }
        }

        [TestMethod]
        public void ParserTest2ValidNewlineArguments()
        {
            var input = "1\n1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(2, results.Length);
            foreach (var i in results)
            {
                Assert.AreEqual(1, i);
            }
        }

        [TestMethod]
        public void ParserTest4ValidNewlineArguments()
        {
            var input = "1\n1\n1\n1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            foreach (var i in results)
            {
                Assert.AreEqual(1, i);
            }
        }

        [TestMethod]
        public void ParserTest1Invalid3ValidNewlineArguments()
        {
            var input = "ccccd\n1\n1\n1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(results[0], 0);
            for (int i = 1; i < results.Length; ++i)
            {
                Assert.AreEqual(1, results[i]);
            }
        }

        [TestMethod]
        public void ParserTest3Valid1InvalidNewlineArguments()
        {
            var input = "1\n1\n1\n cccd";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(results[results.Length - 1], 0);
            for (int i = 0; i < results.Length - 1; ++i)
            {
                Assert.AreEqual(1, results[i]);
            }
        }

        [TestMethod]
        public void ParserTest4ValidAltArguments()
        {
            var input = "1,1\n1,1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            foreach (var i in results)
            {
                Assert.AreEqual(1, i);
            }
        }

        [TestMethod]
        public void ParserTest1Invalid3ValidAltArguments()
        {
            var input = "ccccd\n1,1\n1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(results[0], 0);
            for (int i = 1; i < results.Length; ++i)
            {
                Assert.AreEqual(1, results[i]);
            }
        }

        [TestMethod]
        public void ParserTest3Valid1InvalidAltArguments()
        {
            var input = "1,1\n1\n cccd";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(4, results.Length);
            Assert.AreEqual(results[results.Length - 1], 0);
            for (int i = 0; i < results.Length - 1; ++i)
            {
                Assert.AreEqual(1, results[i]);
            }
        }
    }
}
