using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using NUnit.Framework;

namespace Mike.Katas
{
    public class StringCalculator
    {
        public int Add(string numbers)
        {
            return AddStatic(numbers);
        }

        public static int AddStatic(string numbers)
        {

            if (string.IsNullOrEmpty(numbers))
                return 0;

            Regex multipleSeparatorExpression = new Regex(@"^//(\[(?<separator>.+?)])+\n?(?<numbers>.*)$");
            Regex singleSeparatorExpression = new Regex(@"^//(?<separator>.+?){1}\n?(?<numbers>.*?)$");
            string separatorPattern = string.Empty;
            List<string> nums = new List<string>();

            Match singleSeparatorMatch = singleSeparatorExpression.Match(numbers);
            Match multipleSeparatorMatch = multipleSeparatorExpression.Match(numbers);

            if (multipleSeparatorMatch.Success)
            {

                CaptureCollection separators = multipleSeparatorMatch.Groups["separator"].Captures;

                for (int i = 0; i < separators.Count; i++)
                    separatorPattern += Regex.Escape(separators[i].Value) + (i < separators.Count - 1 ? "|" : null);

                numbers = multipleSeparatorMatch.Groups["numbers"].Value;

            }
            else if (singleSeparatorMatch.Success)
            {

                separatorPattern = Regex.Escape(singleSeparatorMatch.Groups["separator"].Value);
                numbers = singleSeparatorMatch.Groups["numbers"].Value;

            }
            else
            {
                separatorPattern = @"\n|,";
            }

            nums.AddRange(Regex.Split(numbers, separatorPattern));

            int[] parsednumbers = (from n in nums select int.Parse(n)).ToArray();

            var negativeNumbers = (from n in parsednumbers where n < 0 select n.ToString()).ToArray();

            if (negativeNumbers.Length > 0)
            {
                var ExceptionMessage = FormatMessage(negativeNumbers);

                throw new InvalidOperationException(ExceptionMessage);
            }


            return (from n in parsednumbers where n < 1000 select n).Sum();


        }

        private static string FormatMessage(string[] negativeNumbers)
        {
            MessageFormatter mf = new MessageFormatter();
            return mf.Format(negativeNumbers);
        }
    }

    internal class MessageFormatter
    {
        public string Format(string[] strings)
        {
            string MessageNumbers = string.Join(", ", strings);
            string ExceptionMessage = string.Format("negatives not allowed [{0}]", MessageNumbers);
            return ExceptionMessage;
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
        [ExpectedException(typeof(InvalidOperationException))]
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
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("negatives not allowed [-1]", e.Message);
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
            catch (InvalidOperationException e)
            {
                Assert.AreEqual("negatives not allowed [-1, -3]", e.Message);
            }
        }
    }

}