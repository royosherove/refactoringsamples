using System.Collections.Generic;

namespace StringCalculatorTests
{
    public interface INegativeChecker
    {
        void Check(IEnumerable<int> numbers);
    }
}