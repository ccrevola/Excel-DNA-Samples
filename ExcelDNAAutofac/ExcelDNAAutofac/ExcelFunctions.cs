using Autofac;
using Calculators;
using ExcelDna.Integration;

namespace ExcelDNAAutofac
{
    public static class ExcelFunctions
    {
        private static IContainer container;
        static ExcelFunctions()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Calculation>().As<ICalculation>();
            builder.RegisterType<Calculation>().SingleInstance();
            container = builder.Build();
        }

        [ExcelFunction(Description = "My Add")]
        public static string MyAdd(int value1, int value2)
        {
            var calc = container.Resolve<ICalculation>();
            return $"Calc Hash Code: {calc.GetHashCode()} Value: {calc.Add(value1, value2)}";
        }

        [ExcelFunction(Description = "My Add Singleton Calc")]
        public static string MyAdd2(int value1, int value2)
        {
            var calc = container.Resolve<Calculation>();
            return $"Calc Hash Code: {calc.GetHashCode()} Value: {calc.Add(value1, value2)}";
        }
    }
}