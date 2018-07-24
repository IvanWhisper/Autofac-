using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo08
{
    class Program
    {
        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<MyComponent>().As<IService>();
            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                //如果您有可能注册或可能未注册的服务，则可以使用ResolveOptional()或TryResolve()尝试对服务进行条件解析：
                //如果IService 已注册，则会被解析; 如果未注册，则返回值为空。
                var service = scope.ResolveOptional<IService>();

                //如果IProvider已注册，则提供者变量将保存该值; 否则你可以采取一些其他的行动。
                IService provider = null;
                if (scope.TryResolve<IService>(out provider))
                {
                    //用已解析的提供值做一些事情。
                }
            }
        }
    }
}
