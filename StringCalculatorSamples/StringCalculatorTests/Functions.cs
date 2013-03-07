using System;
using System.Linq;
using NUnit.Framework;

namespace StringCalculatorApp
{
    public class StringCalculator
    {
        private const string CustomDelimiterStart = "//";
        private const char CustomDelimiterEnd = '\n';

        private static readonly char[] Seperators = new[] { ',', CustomDelimiterEnd };

        public int Add(string numbers)
        {
            if (string.IsNullOrEmpty(numbers))
            {
                return 0;
            }

            // Found custom delimiter
            if (HasCustomDelimiter(numbers))
            {
                // Remove the first two '//'
                var customDelimiterNumbers = numbers.Remove(0, 2);
                // Get the index where the custom delimiter ends
                int endIndex = customDelimiterNumbers.IndexOf(CustomDelimiterEnd);

                // Found end of custom delimiter  
                if(endIndex > -1)
                {
                    char[] customDelimiter = customDelimiterNumbers.Take(endIndex).ToArray();
                    return customDelimiterNumbers.Substring(endIndex + 1).Split(customDelimiter).Calc();
                }
            }

            return numbers.Split(Seperators).Calc();
        }
        private static bool HasCustomDelimiter(string numbers)
        {
            return numbers.StartsWith(CustomDelimiterStart);
        }
    }

    static class StringCalculatorExtentions
    {
        public static int Calc(this string[] inputs)
        {
            int[] numbers = inputs.Where(input => !string.IsNullOrEmpty(input))
                 .Select(int.Parse)
                 .Where(number => number < 1000) // Ignore numbers smaller than 1000
                 .ToArray();

            // Negative numbers should throw an exception
            var negatives = numbers.Where(number => number < 0).ToArray();
            if (negatives.Any())
            {
                throw new ArgumentException(string.Format("negatives not allowed: [{0}]", string.Join(",",negatives)));
            }

            // Sum and return!
            return numbers.Sum();
        }

    }

    [TestFixture]
    public class StringCalculatorTestAfter
    {
        StringCalculator calculator = new StringCalculator();

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
                Assert.AreEqual("negatives not allowed: [-1]", e.Message);
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
                Assert.AreEqual("negatives not allowed: [-1,-3]", e.Message);
            }
        }
    }

}