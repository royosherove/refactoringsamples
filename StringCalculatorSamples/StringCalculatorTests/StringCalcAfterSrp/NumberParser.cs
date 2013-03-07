using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorTests
{
    internal class NumberParser
    {
        public List<int> Parse(IEnumerable<string> tokens)
        {
            return tokens.Select(ConvertToNumber).ToList();
        }

        private int ConvertToNumber(string input)
        {
            return int.Parse(input);
        }
    }
}