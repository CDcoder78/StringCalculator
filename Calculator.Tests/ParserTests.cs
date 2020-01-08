using System;
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
        [ExpectedException(typeof(ArgumentNullException))]
        public void ParserTestInvalidArgument()
        {
            _target.ParseIntegers(null);
        }

        [TestMethod]
        public void ParserTest2ValidArguments()
        {
            var input = "1,1";
            var results = _target.ParseIntegers(input);

            Assert.AreEqual(2, results.Length);
            foreach(var i in results)
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
            for(int i = 1; i < results.Length; ++i)
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
    }
}
