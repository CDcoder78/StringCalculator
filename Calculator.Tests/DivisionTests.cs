using Calculator.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System;

namespace Calculator.Tests
{
    [TestClass]
    public class DivisionTests
    {
        private Mock<IParser> _parser;
        private Division _target;

        [TestInitialize]
        public void Initialize()
        {
            _parser = new Mock<IParser>();
            _target = new Division(_parser.Object);
        }

        [TestMethod]
        public void Divide6by2by1Test()
        {
            var input = "6, 2, 1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 6, 2, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("6/2/1 = 3", result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void DivideInvalidDivisionTest()
        {
            var input = "6, 0, 0";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 6, 0, 0 });

            var result = _target.Compute(input);
        }

        [TestMethod]
        public void Divide1and2Test()
        {
            var input = "1, 2";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 2 });

            var result = _target.Compute(input);

            Assert.AreEqual("1/2 = 0.5", result);
        }


        [TestMethod]
        public void Valid20Test()
        {
            var input = "20";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 20 });

            var result = _target.Compute(input);

            Assert.AreEqual("20 = 20", result);
        }

        [TestMethod]
        public void Valid1InputTest()
        {
            var input = "1,5000";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 5000 });

            var result = _target.Compute(input);
            
            Assert.AreEqual("1/5000 = 0.0002", result);
        }

        [TestMethod]
        public void ValidInput2Test()
        {
            var input = "4,-2";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 4, -2 });

            var result = _target.Compute(input);

            Assert.AreEqual("4/-2 = -2", result);
        }

        [TestMethod]
        public void EmptyTest()
        {
            var input = "";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0 });

            var result = _target.Compute(input);

            Assert.AreEqual("0 = 0", result);
        }

        [TestMethod]
        public void InvalidTest()
        {
            var input = "klkkd";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0 });

            var result = _target.Compute(input);

            Assert.AreEqual("0 = 0", result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void EmptyEmptyTest()
        {
            var input = ",";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 0 });

            var result = _target.Compute(input);
        }

        [TestMethod]
        public void ValidEmptyTest()
        {
            var input = ",1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("0/1 = 0", result);
        }

        [TestMethod]
        public void InvalidValid1Test()
        {
            var input = "jjkjld,1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("0/1 = 0", result);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ValidInvalid1Test()
        {
            var input = "1,kkd";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 0 });

            var result = _target.Compute(input);
        }

        [TestMethod]
        [ExpectedException(typeof(DivideByZeroException))]
        public void ValidDivideByZero1Test()
        {
            var input = "1,0";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 0 });

            var result = _target.Compute(input);
        }
    }
}
