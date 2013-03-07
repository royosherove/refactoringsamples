using System;
using System.Collections.Generic;

namespace StringCalculatorTests
{
    static internal class ExceptionMessageMaker
    {
        public static string MakeMessage(List<int> negativeNumbers, string prefix)
        {
            return prefix + " " + FormatNegativeNumbers(negativeNumbers);
        }

        private static string FormatNegativeNumbers(IEnumerable<int> negativeNumbers)
        {
            return String.Join(" ", negativeNumbers);
        }
    }
}