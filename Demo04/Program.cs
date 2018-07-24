using Autofac;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo04
{
    //Autofac04注册组件之将参数传递给注册者
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            //1)lambda表达式组件
            builder.Register(c => new ConfigReader("sectionName")).As<IConfigReader>();

            //2)参数反射
            //    //a.使用NAMED参数：
            //    builder.RegisterType<ConfigReader>()
            //           .As<IConfigReader>()
            //           .WithParameter("configSectionName", "sectionName");

            //    //b.使用TYPED参数：
            //    builder.RegisterType<ConfigReader>()
            //           .As<IConfigReader>()
            //           .WithParameter(new TypedParameter(typeof(string), "sectionName"));

            //    //c.使用RESOLVED参数：
            //    builder.RegisterType<ConfigReader>()
            //           .As<IConfigReader>()
            //           .WithParameter(
            //             new ResolvedParameter(
            //               (pi, ctx) => pi.ParameterType == typeof(string) && pi.Name == "configSectionName",
            //               (pi, ctx) => "sectionName"));

            //3)包含Lambda表达式组件的参数
            //builder.Register((c, p) =>
            //     new ConfigReader(p.Named<string>("configSectionName")))
            //   .As<IConfigReader>();

            Container = builder.Build();

            using (var scope = Container.BeginLifetimeScope())
            {
                var reader = scope.Resolve<IConfigReader>(new NamedParameter("configSectionName", "sectionName"));
            }
            Console.ReadLine();
        }
    }
}
