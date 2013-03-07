using System.Collections.Generic;

namespace StringCalculatorTests
{
    public interface IChecker
    {
        void Check(IEnumerable<int> numbers);
    }
}