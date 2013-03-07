using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StringCalculatorTests.StringCalcAfterSrp
{
    public class StringCalculatorSrpAfter
    {
        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            IEnumerable<string> tokens = Tokenize(input);
            IEnumerable<int> numbers = ConvertToNumbers(tokens);
            CheckForProblematicNumbers(numbers);

            return numbers.Sum();
        }

        private  IEnumerable<string> Tokenize(string input)
        {
            Tokenizer tok = new Tokenizer();
            return tok.Tokenize(input);
        }

        private List<int> ConvertToNumbers(IEnumerable<string> tokens)
        {
            NumberParser parser = new NumberParser();
            return parser.Parse(tokens);
        }

        private void CheckForProblematicNumbers(IEnumerable<int> numbers)
        {
            NegativeChecker check = new NegativeChecker();
            check.Check(numbers);
        }


    }


    [TestFixture]
    public class StringCalculatorTestAfter
    {
        private StringCalculatorSrpAfter calculator = new StringCalculatorSrpAfter();

        [Test]
        public void Add_EmptyString_ReturnsZero()
        {
            int result = calculator.Add("");

            Assert.AreEqual(0, result);
        }

        [Test]
        public void Add_OneNumber_ReturnsNumber()
        {
            int result = calculator.Add("42");

            Assert.AreEqual(42, result);
        }

        [Test]
        public void Add_TwoNumbersDelimitedWithComma_ReturnsSum()
        {
            int result = calculator.Add("1,2");

            Assert.AreEqual(3, result);
        }

        [Test]
        public void Add_MultipleNumbersDelimitedWithComma_ReturnsSum()
        {
            int result = calculator.Add("1,2,3");

            Assert.AreEqual(6, result);
        }

        [Test]
        public void Add_TwoNumbersDelimitedWithNewLine_ReturnsSum()
        {
            int result = calculator.Add("1\n2");

            Assert.AreEqual(3, result);
        }


        [Test]
        public void Add_TwoNumbersDelimitedWithCustomDelimiter_ReturnsSum()
        {
            string input = "//;\n1;2";
            int result = calculator.Add(input);

            Assert.AreEqual(3, result);
        }

        [Test]
        [ExpectedException(typeof(ArgumentException))]
        public void Add_NegativeNumber_ThrowsArgumentException()
        {
            calculator.Add("-1");
        }

        [Test]
        public void Add_NegativeNumber_ErrorMessageContainsNumber()
        {
            try
            {
                calculator.Add("-1");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Negatives not allowed: -1", e.Message);
            }
        }

        [Test]
        public void Add_MultipleNegativeNumbers_ErrorMessageContainsAllNegativeNumbers()
        {
            try
            {
                calculator.Add("-1,2,-3");
                Assert.Fail();
            }
            catch (ArgumentException e)
            {
                Assert.AreEqual("Negatives not allowed: -1 -3", e.Message);
            }
        }
    }


}
