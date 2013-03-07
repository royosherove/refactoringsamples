using System.Collections.Generic;

namespace StringCalculatorTests
{
    public class Tokenizer2
    {
        private const char DefaultDelimiter = ',';
        public IEnumerable<string> Tokenize(string input)
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
        private  char ParseCustomDelimiter(ref string input)
        {
            char customDelimiter = input[2];
            input = input.Substring(4);
            return customDelimiter;
        }

        private  bool CustomDelimiterSpecified(string input)
        {
            return input.StartsWith("//");
        }

        private  string ReplaceAlternativeDelimitersWithCommas(string input)
        {
            return input.Replace("\n", ",");
        }

    }
}