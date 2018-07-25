using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo09
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ConfigReader>().As<IConfigReader>();
            var container = builder.Build();

            //参数与反射组件
            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<ConfigReader>(
                    new NamedParameter("configSectionName", "sectionName")
                //    new TypedParameter(typeof(Guid), Guid.NewGuid()),
                //    new ResolvedParameter(
                //  (pi, ctx) => pi.ParameterType == typeof(ILog) && pi.Name == "logger",
                //  (pi, ctx) => LogManager.GetLogger("service"))
                    );
            }

            //包含Lambda表达式组件的参数
            builder.Register((c, p) =>
                 new ConfigReader(p.Named<string>("configSectionName")))
                   .As<IConfigReader>();
            using (var scope = container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<IConfigReader>(
                    new NamedParameter("configSectionName", "sectionName")
                    );
            }

                Console.ReadLine();
        }
    }
}
