using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo15
{
    class Program
    {
        private static IContainer container { get; set; }
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<Worker>() //失败：将抛出一个BInterfaceSubclass类型的类
                   .As<IService>()          //键入TConcrete
                   .OnActivating(e=> {
                       Console.WriteLine(123);
                   })
                   .OnActivated(e =>
                   {
                       Console.WriteLine(456);
                   })
                   //会替原来的Dispose自动调用
                   .OnRelease(e =>
                   {
                       Console.WriteLine(789);
                       
                   })
                   ;
            container = builder.Build();
            using (var scope1 = container.BeginLifetimeScope())
            {

                scope1.Resolve<IService>().Show();
            }
            Console.ReadLine();   
        }
    }
}
