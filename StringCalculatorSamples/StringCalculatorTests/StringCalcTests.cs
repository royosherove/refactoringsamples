using System;
using NUnit.Framework;

namespace StringCalculatorTests
{
    [TestFixture]
    public class StringCalcTests
    {
        [Test]
        public void Add_EmptyInput_ReturnsDefaultValue()
        {
            StringCalc c = MakeCalc();

            int result = c.Add("");

            Assert.AreEqual(0,result);
        }

        private static StringCalc MakeCalc()
        {
            return new StringCalc();
        }


        [TestCase("1",1)]
        [TestCase("2",2)]
        [TestCase("1,2",3)]
        [TestCase("1,3",4)]
        public void Add_SingleNumber_ReturnsThatNumber(string numbers, int expected)
        {
            StringCalc c = MakeCalc();

            int result = c.Add(numbers);

            Assert.That(result,Is.EqualTo(expected));
        }



    }

    public class StringCalc
    {
        public int Add(string numbers)
        {
            if (numbers==String.Empty)
            {
                return 0; 
            }
            if (numbers.Contains(","))
            {
                return Add(numbers[0].ToString()) 
                     + Add(numbers[2].ToString());
            }
            return int.Parse(numbers);
        }
    }
}
