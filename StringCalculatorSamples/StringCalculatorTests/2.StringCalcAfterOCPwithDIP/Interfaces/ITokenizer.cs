using System.Collections.Generic;

namespace StringCalculatorTests
{
    public interface ITokenizer
    {
        IEnumerable<string> Tokenize(string input);
    }
}