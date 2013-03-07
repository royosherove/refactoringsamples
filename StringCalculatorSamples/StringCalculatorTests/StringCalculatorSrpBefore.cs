using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace StringCalculatorSRPBefore
{
    public class StringCalculator
    {
        private const char DefaultDelimiter = ',';

        public int Add(string input)
        {
            if (string.IsNullOrEmpty(input))
                return 0;

            IEnumerable<string> tokens = Tokenize(input);
            IEnumerable<int> numbers = tokens.Select(ConvertToNumber).ToList();

            CheckForNegativeNumbers(numbers);

            return numbers.Sum();
        }

        private static void CheckForNegativeNumbers(IEnumerable<int> numbers)
        {
            List<int> negativeNumbers = numbers.Where(number => number < 0).ToList();

            if (negativeNumbers.Count > 0)
                throw new ArgumentException("Negatives not allowed: " + FormatNegativeNumbers(negativeNumbers));
        }

        private static string FormatNegativeNumbers(IEnumerable<int> negativeNumbers)
        {
            return string.Join(" ", negativeNumbers);
        }

        private static IEnumerable<string> Tokenize(string input)
        {
            char delimiter = DefaultDelimiter;

            if (CustomDelimiterSpecified(input))
            {
                delimiter = ParseCustomDelimiter(ref input);
            }
            else
            {
                input = ReplaceAlternativeDelimitersWithCommas(input);
            }

            return input.Split(delimiter);
        }

        private static char ParseCustomDelimiter(ref string input)
        {
            char customDelimiter = input[2];
            input = input.Substring(4);
            return customDelimiter;
        }

        private static bool CustomDelimiterSpecified(string input)
        {
            return input.StartsWith("//");
        }

        private static string ReplaceAlternativeDelimitersWithCommas(string input)
        {
            return input.Replace("\n", ",");
        }

        private static int ConvertToNumber(string input)
        {
            return int.Parse(input);
        }
    }



    [TestFixture]
    public class StringCalculatorTest
    {
        private StringCalculator calculator = new StringCalculator();

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
