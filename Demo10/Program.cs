using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo10
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<A>();
            builder.RegisterType<B>();

            builder.RegisterType<FirstHandler>().As<IMessageHandler>();
            builder.RegisterType<SecondHandler>().As<IMessageHandler>();
            builder.RegisterType<ThirdHandler>().As<IMessageHandler>();
            builder.RegisterType<MessageProcessor>();

            //带键服务查找（IIndex<X, B>）
            builder.RegisterType<DerivedB>().Keyed<B>("first");
            builder.RegisterType<AnotherDerivedB>().Keyed<B>("second");
            builder.RegisterType<A>();

            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                //B被自动注入到A.
                var a = scope.Resolve<A>();
                //处理器解析后，它将所有注册的处理程序传递给构造函数。
                var processor = scope.Resolve<MessageProcessor>();
                scope.Resolve<IEnumerable<IMessageHandler>>();
                processor.ProcessMessage(new Message());
            }
        }
    }
}
