using Autofac;
using Lib07;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo07
{
    //扫描模块
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var assembly = typeof(AComponent).Assembly;
            var builder = new ContainerBuilder();

            //注册这两个模块
            builder.RegisterAssemblyModules(assembly);
            //注册模块但不包含BModule
            //builder.RegisterAssemblyModules<AModule>(assembly);
            //builder.RegisterAssemblyModules(typeof(AModule), assembly);
            var con = builder.Build();

            con.Resolve<AComponent>().show();
            con.Resolve<BComponent>().show();

            Console.ReadLine();
        }
    }
}
