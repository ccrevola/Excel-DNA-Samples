using System;
using Calculators;
using ExcelDna.Integration;

namespace ExcelDNAAutofac
{
    public static class ExcelFunctionsDelegated
    {
        private static Func<string, ICalculation> factory;
        public static void SetDelegate(Func<string, ICalculation> factoryDelegate)
        {
            factory = factoryDelegate;
        }

        [ExcelFunction(Description = "My Add Delegated")]
        public static string MyAddD(int value1, int value2)
        {
            var calc = factory("default");
            return $"Calc Hash Code: {calc.GetHashCode()} Value: {calc.Add(value1, value2)}";
        }

        [ExcelFunction(Description = "My Add Delegated Singleton Calc")]
        public static string MyAddD2(int value1, int value2)
        {
            var calc = factory("singleton");
            return $"Calc Hash Code: {calc.GetHashCode()} Value: {calc.Add(value1, value2)}";
        }
    }
}