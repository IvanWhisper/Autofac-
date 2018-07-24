using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo05
{
    //Autofac05注册组件之属性和方法注入
    class Program
    {
        private static IContainer Container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            //1)属性注入
                //a)lamda
                builder.Register(c => new A { B = c.Resolve<B>() });
                //循环依赖 
                //builder.Register(c => new A()).OnActivated(e => e.Instance.B = e.Context.Resolve<B>());
                //b)组件是反射组件，请使用PropertiesAutowired()修饰符来注入属性
                builder.RegisterType<A>().PropertiesAutowired();
            //c)一个特定的属性和值来连接，你可以使用WithProperty()修饰符：
            //builder.RegisterType<A>().WithProperty("PropertyName", propertyValue);
            //2)方法注入
            //a)lambda表达式组件，并在激活器中处理方法调用权
            builder.Register(c =>
            {
                var result = new MyObjectType();
                var dep = c.Resolve<TheDependency>();
                result.SetTheDependency(dep);
                return result;
            });
            //b)一个激活事件处理程序
            builder.RegisterType<MyObjectType>().OnActivating(e => {
                      var dep = e.Context.Resolve<TheDependency>();
                      e.Instance.SetTheDependency(dep);
              });
        }
    }
}
