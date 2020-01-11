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
        public void ParserInvalidHandleCommand()
        {
            _target.HandleCommand("-AddDelimiter:'1'");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserInvalidSpaceHandleCommand()
        {
            _target.HandleCommand("-AddDelimiter:' '");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserInvalidEmptyHandleCommand()
        {
            _target.HandleCommand("-AddDelimiter:''");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserInvalidCharsHandleCommand()
        {
            _target.HandleCommand("-AddDelimiter:'xxxx'");
        }

        [TestMethod]
        public void ParserValidCharHandleCommand()
        { ;
            Assert.IsTrue(_target.HandleCommand("-AddDelimiter:'x'"));
        }

        [TestMethod]
        public void ParserRemoveValidCharHandleCommand()
        {
            ;
            Assert.IsTrue(_target.HandleCommand("-RemoveDelimiter:','"));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserRemoveInvalidHandleCommand()
        {
            _target.HandleCommand("-RemoveDelimiter:'1'");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserRemoveInvalidSpaceHandleCommand()
        {
            _target.HandleCommand("-RemoveDelimiter:' '");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserRemoveInvalidEmptyHandleCommand()
        {
            _target.HandleCommand("-RemoveDelimiter:''");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserRemoveInvalidCharsHandleCommand()
        {
            _target.HandleCommand("-RemoveDelimiter:'xxxx'");
        }

        [TestMethod]
        public void ParserDenyNegativesHandleCommand()
        {
            Assert.IsTrue(_target.HandleCommand("-DenyNegatives"));
            Assert.IsTrue(_target.DenyNegative);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserInvalidUpperBoundsZeroHandleCommand()
        {
            _target.HandleCommand("-UpperBounds:0");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void ParserInvalidUpperBoundsHandleCommand()
        {
            _target.HandleCommand("-UpperBounds:-1");
        }

        [TestMethod]
        public void ParserValidUpperBoundsHandleCommand()
        {
            Assert.IsTrue(_target.HandleCommand("-UpperBounds:1"));
            Assert.AreEqual(1u, _target.UpperBound);
        }

        [TestMethod]
        public void ParserValidSpaceUpperBoundsHandleCommand()
        {
            Assert.IsTrue(_target.HandleCommand("-UpperBounds: 1"));
            Assert.AreEqual(1u, _target.UpperBound);
        }

        [TestMethod]
        public void ParserAllowNegativesHandleCommand()
        {
            Assert.IsTrue(_target.HandleCommand("-AllowNegatives"));
            Assert.IsFalse(_target.DenyNegative);
        }

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

        [TestMethod]
        public void ParserCustomArguments()
        {
            var input = "//#\n2#5";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(2, results.Length);
            Assert.AreEqual(2, results[0]);
            Assert.AreEqual(5, results[1]);
        }

        [TestMethod]
        public void ParserCustom1Arguments()
        {
            var input = "//,\n2,ff,100";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(3, results.Length);
            Assert.AreEqual(2, results[0]);
            Assert.AreEqual(0, results[1]);
            Assert.AreEqual(100, results[2]);
        }

        [TestMethod]
        public void ParserCustom2Arguments()
        {
            var input = "//[***]\n11***22***33";
            var results = _target.ParseIntegers(input);
            Assert.AreEqual(3, results.Length);
            Assert.AreEqual(11, results[0]);
            Assert.AreEqual(22, results[1]);
            Assert.AreEqual(33, results[2]);
        }

        [TestMethod]
        public void ParserCustom3Arguments()
        {
            var input = "//[*][!!][r9r]\n11r9r22*hh*33!!44";
            var results = _target.ParseIntegers(input);
            Assert.AreEqual(5, results.Length);
            Assert.AreEqual(11, results[0]);
            Assert.AreEqual(22, results[1]);
            Assert.AreEqual(0, results[2]);
            Assert.AreEqual(33, results[3]);
            Assert.AreEqual(44, results[4]);
        }
    }
}
