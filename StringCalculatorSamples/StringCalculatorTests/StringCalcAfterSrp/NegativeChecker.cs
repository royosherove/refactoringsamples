using System;
using System.Collections.Generic;
using System.Linq;

namespace StringCalculatorTests
{
    public class NegativeChecker
    {
        public void Check(IEnumerable<int> numbers)
        {
            List<int> negativeNumbers = numbers.Where(number => number < 0).ToList();

            if (negativeNumbers.Count > 0)
                throw new ArgumentException(ExceptionMessageMaker.MakeMessage(negativeNumbers, "Negatives not allowed:"));
        }
    }
}