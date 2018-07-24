using Autofac;
using InterfaceLib06;
using Lib06;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Demo06
{
    //扫描类型
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            //扫描类型
            var dataAccess = Assembly.LoadFrom("Lib06.Dll");

            builder.RegisterAssemblyTypes(dataAccess)
                   //LINQ查找
                   .Where(t => t.Name.EndsWith("Repository"))
                   //拍出指定
                   .Except<MyRepository>(
                //ct =>ct.As<ISpecial>().SingleInstance()可以自定义注册
                )
                   //将类型注册为将其所有公共接口提供为服务（不包括IDisposable）
                   .AsImplementedInterfaces();
            //指定服务
            //.As<IRes>();
            //注册可分配给已打开泛型类型的已关闭实例的类型
            //AsClosedTypesOf(open)
            //默认值：注册类型为自己的 - 当用另一个服务规范覆盖默认值时也很有用.
            //AsSelf()
            var con = builder.Build();
            var a=con.Resolve<IRes>();
            a.show();
            Console.ReadLine();
        }
    }
}
