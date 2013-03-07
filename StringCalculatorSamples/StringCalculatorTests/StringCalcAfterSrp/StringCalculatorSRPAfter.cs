using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StringCalculatorTests
{
    public class StringCalculatorSrpAfter
    {
        private NegativeChecker _validator;
        private NumberParser _parser;
        private Tokenizer _tokenizer;

        public StringCalculatorSrpAfter(NegativeChecker validator, NumberParser parser, Tokenizer tokenizer)
        {
            _validator = validator;
            _parser = parser;
            _tokenizer = tokenizer;
        }

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            IEnumerable<string> tokens = _tokenizer.Tokenize(input);
            IEnumerable<int> numbers = _parser.Parse(tokens);

            _validator.Check(numbers);

            return numbers.Sum();
        }
    }


    [TestFixture]
    public class StringCalculatorTestAfter
    {
        private StringCalculatorSrpAfter calculator = new StringCalculatorSrpAfter(new NegativeChecker(), new NumberParser(), new Tokenizer());

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
