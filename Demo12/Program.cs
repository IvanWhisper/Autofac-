using Autofac;
using Autofac.Features.OwnedInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Demo12
{
    class Program
    {
        private static IContainer container { get; set; }

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            // 这...
            builder.RegisterType<Worker>();

            //每个依赖的实例
            builder.RegisterType<Worker>().InstancePerDependency();
            //单一实例
            builder.RegisterType<SingleWorker>().SingleInstance();
            // //每个生命周期范围实例
            //builder.RegisterType<Worker>().InstancePerLifetimeScope();
            //每个匹配生命周期范围实例
            //builder.RegisterType<Worker>().InstancePerMatchingLifetimeScope("myrequest");
            //每个请求实例
            //builder.RegisterType<Worker>().InstancePerRequest();
            //每个Owned实例
            builder.RegisterType<MessageHandler>();
            builder.RegisterType<ServiceForHandler>().InstancePerOwned<MessageHandler>();

            builder.RegisterType<MyThreadScopedComponent>()
                   .InstancePerLifetimeScope();


            var container = builder.Build();

            using (var scope = container.BeginLifetimeScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    //在这个循环中解析的100个工作者实例中的每一个都将是全新的。
                    var w = scope.Resolve<Worker>();
                    w.DoWork();
                }
            }
            using (var scope1 = container.BeginLifetimeScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    var w1 = scope1.Resolve<SingleWorker>();
                    w1.DoWork();
                    using (var scope2 = scope1.BeginLifetimeScope())
                    {
                        var w2 = scope2.Resolve<SingleWorker>();
                        w2.DoWork();
                        //root，w1和w2总是字面上相同的对象实例。 从哪个生命周期作用域解析或多少次都没有关系。
                    }
                }
            }
            //每个生命周期范围实例
            using (var scope1 = container.BeginLifetimeScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    //每次你在这个范围内解析这个问题，你都会得到同样的实例。
                    var w1 = scope1.Resolve<Worker>();
                }
            }

            using (var scope2 = container.BeginLifetimeScope())
            {
                for (var i = 0; i < 100; i++)
                {
                    //每次你在这个范围内解析这个问题，你都会得到同样的实例，但是这个实例不同于上面的范例中使用的实例。新范围=新实例（New scope = new instance）.
                    var w2 = scope2.Resolve<Worker>();
                }
            }

            //使用标签创建生命周期范围。
            using (var scope1 = container.BeginLifetimeScope("myrequest"))
            {
                for (var i = 0; i < 100; i++)
                {
                    var w1 = scope1.Resolve<Worker>();
                    using (var scope2 = scope1.BeginLifetimeScope())
                    {
                        var w2 = scope2.Resolve<Worker>();

                        //w1和w2始终是相同的对象实例，因为组件是每个匹配的生命周期范围，所以它实际上是在命名范围内的单例。
                    }
                }
            }

            //使用标签创建另一个生命周期范围
            using (var scope3 = container.BeginLifetimeScope("myrequest"))
            {
                for (var i = 0; i < 100; i++)
                {
                    //w3将比早先标记的生命期范围内解决的工作者有所不同
                    var w3 = scope3.Resolve<Worker>();
                    using (var scope4 = scope3.BeginLifetimeScope())
                    {
                        var w4 = scope4.Resolve<Worker>();

                        //w3和w4始终是同一个对象，因为它们处于相同的标记范围内，但它们与先前的工作者（w1，w2）不一样。
                    }
                }
            }

            //如果没有匹配的作用域，则无法解析每个匹配生命周期的组件。
            using (var noTagScope = container.BeginLifetimeScope())
            {
                //这会抛出一个异常，因为这个范围没有预期的标记，也没有任何父范围！
                var fail = noTagScope.Resolve<Worker>();
            }
            Console.WriteLine("___________________________________________________");
            //每个Owned实例
            using (var scope = container.BeginLifetimeScope())
            {
                //消息处理程序本身以及已解析的相关ServiceForHandler服务位于“scope”下的一个小小的生命周期范围内。 请注意，解析Owned<T>意味着您有责任处置。

                var h1 = scope.Resolve<Owned<MessageHandler>>();

                h1.Dispose();

            }
            //线程范围
            using (var myScope = container.BeginLifetimeScope())
            {
                var s = new ThreadCreator(myScope);
                Thread td0 = new Thread(new ThreadCreator(myScope).ThreadStart);
                Thread td1= new Thread(new ThreadCreator(myScope).ThreadStart);
                Thread td2 = new Thread(new ThreadCreator(myScope).ThreadStart);
                Thread td3 = new Thread(new ThreadCreator(myScope).ThreadStart);

                td0.Start();
                td1.Start();
                td2.Start();
                td3.Start();

                Thread.Sleep(1000);
            }

            Console.ReadLine();
        }

    }
    public class ThreadCreator
    {
        private ILifetimeScope _parentScope;

        public ThreadCreator(ILifetimeScope parentScope)
        {
            this._parentScope = parentScope;
        }

        public void ThreadStart()
        {
            using (var threadLifetime = this._parentScope.BeginLifetimeScope())
            {
                var thisThreadsInstance = threadLifetime.Resolve<MyThreadScopedComponent>();
                thisThreadsInstance.Show();
            }
        }
    }
}
