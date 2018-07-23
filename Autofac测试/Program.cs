using Autofac;
using Demo.Interface;
using Demo.Model;
using Demo.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autofac测试
{
    class Program
    {
        private static IContainer Container { get; set; }
        static void Main(string[] args)
        {
            // 创建你的构建者
            var builder = new ContainerBuilder();
            //通常你只想通过接口暴露这个类型： 
            builder.RegisterType<ConsoleWriter>().As<IWriter>()
                //默认注册
                .PreserveExistingDefaults();
            builder.RegisterType<MyWriter>().As<IWriter>();
            //ConsoleWriter cw = new ConsoleWriter();
            ////单例注册
            //builder.RegisterInstance(cw)
            //   .As<IWriter>()
            //   //用户可以回收
            //   .ExternallyOwned();
            builder.RegisterType<PrintService>().As<IService>()
                //指定构造
                .UsingConstructor(typeof(IWriter));
            //但是，如果你想要两种服务（不常见），你可以这样说：
            //builder.RegisterType<MyWrite>().AsSelf().As<IService>();
            builder.Register<IUser>(
                  (c, p) =>
                  {
                      var accountId = p.Named<string>("AccountId");
                      if (accountId.StartsWith("9"))
                      {
                          return new GodUser();
                      }
                      else
                      {
                          return new NomUser();
                      }
                  });

            Container = builder.Build();
            WriteDate();
            Console.ReadLine();
        }

        public static void WriteDate()
        {
            //创建作用域，解析IDateWriter，使用它，然后处理作用域。
            using (var scope = Container.BeginLifetimeScope())
            {
                var user = scope.Resolve<IUser>(new NamedParameter("AccountId", "12345"));
                var writer = scope.Resolve<IService>();
                writer.WriteData("测试");
            }
        }
    }
}
