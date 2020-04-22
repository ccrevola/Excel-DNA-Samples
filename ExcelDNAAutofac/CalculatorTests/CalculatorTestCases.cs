using Calculators;
using NUnit.Framework;

namespace CalculatorTests
{
    [TestFixture]
    public class CalculatorTestCases
    {

        [Test]
        public void TestAdd()
        {
            var calc = new Calculation();
            Assert.AreEqual(3, calc.Add(1,2));
        }
    }
}
