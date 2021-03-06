using Calculator.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Tests
{
    [TestClass]
    public class SubtractionTests
    {
        private Mock<IParser> _parser;
        private Subtraction _target;

        [TestInitialize]
        public void Initialize()
        {
            _parser = new Mock<IParser>();
            _target = new Subtraction(_parser.Object);
        }

        [TestMethod]
        public void Sub76Test()
        {
            var input = "1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12 });

            var result = _target.Compute(input);

            Assert.AreEqual("1-2-3-4-5-6-7-8-9-10-11-12 = -76", result);
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
            
            Assert.AreEqual("1-5000 = -4999", result);
        }

        [TestMethod]
        public void ValidInput2Test()
        {
            var input = "4,-3";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 4, -3 });

            var result = _target.Compute(input);

            Assert.AreEqual("4--3 = 7", result);
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
        public void EmptyEmptyTest()
        {
            var input = ",";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 0 });

            var result = _target.Compute(input);

            Assert.AreEqual("0-0 = 0", result);
        }

        [TestMethod]
        public void ValidEmptyTest()
        {
            var input = ",1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("0-1 = -1", result);
        }

        [TestMethod]
        public void InvalidValid1Test()
        {
            var input = "jjkjld,1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("0-1 = -1", result);
        }

        [TestMethod]
        public void ValidInvalid1Test()
        {
            var input = "1,kkd";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 0 });

            var result = _target.Compute(input);

            Assert.AreEqual("1-0 = 1", result);
        }
    }
}
