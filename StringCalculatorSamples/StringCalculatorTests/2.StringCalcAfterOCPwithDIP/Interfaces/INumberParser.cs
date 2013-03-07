using System.Collections.Generic;

namespace StringCalculatorTests
{
    public interface INumberParser
    {
        List<int> Parse(IEnumerable<string> tokens);
    }
}