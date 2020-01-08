using Calculator.Contracts;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Calculator.Tests
{
    [TestClass]
    public class AddTests
    {
        private Mock<IParser> _parser;
        private Add _target;

        [TestInitialize]
        public void Initialize()
        {
            _parser = new Mock<IParser>();
            _target = new Add(_parser.Object);
        }

        [TestMethod]
        [ExpectedException(typeof(NumbersExceededException))]
        public void NumbersExceededExceptionTest()
        {
            var input = "1,1,1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 1, 1 });

            _target.Compute(input);                        
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
            var input = "1.5000";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 5000 });

            var result = _target.Compute(input);
            
            Assert.AreEqual("1+5000 = 5001", result);
        }

        [TestMethod]
        public void ValidInput2Test()
        {
            var input = "4,-3";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 4, -3 });

            var result = _target.Compute(input);

            Assert.AreEqual("4+-3 = 1", result);
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

            Assert.AreEqual("0+0 = 0", result);
        }

        [TestMethod]
        public void ValidEmptyTest()
        {
            var input = ",1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("0+1 = 1", result);
        }

        [TestMethod]
        public void InvalidValid1Test()
        {
            var input = "jjkjld,1";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 0, 1 });

            var result = _target.Compute(input);

            Assert.AreEqual("0+1 = 1", result);
        }

        [TestMethod]
        public void ValidInvalid1Test()
        {
            var input = "1,kkd";

            _parser.Setup(a => a.ParseIntegers(input)).Returns(new int[] { 1, 0 });

            var result = _target.Compute(input);

            Assert.AreEqual("1+0 = 1", result);
        }
    }
}
