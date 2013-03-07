using System;
using System.Linq;

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
                if (endIndex > -1)
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
                throw new ArgumentException(string.Format("negatives not allowed: {0}", negatives));
            }

            // Sum and return!
            return numbers.Sum();
        }
    }
}