using ExcelDna.Integration;
using System;
using Autofac;
using Autofac.Core;
using Calculators;

namespace ExcelDNAAutofac
{
    public class ExcelAddin : IExcelAddIn
    {
        private IContainer _container;
        public void AutoClose()
        {
            
        }

        public void AutoOpen()
        {
            //Set up the delegate example
            var builder = new ContainerBuilder();
            builder.RegisterType<Calculation>().AsSelf().Named<ICalculation>("default");
            builder.RegisterType<Calculation>().AsSelf().Named<ICalculation>("singleton").SingleInstance();
            builder.Register<Func<string, ICalculation>>(c =>
            {
                var registry = c.Resolve<IComponentContext>();
                return registry.ResolveNamed<ICalculation>;
            });
            _container = builder.Build();
            ExcelFunctionsDelegated.SetDelegate(_container.Resolve<Func<string, ICalculation>>());
        }
    }
}
