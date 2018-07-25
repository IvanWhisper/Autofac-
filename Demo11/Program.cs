using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo11
{
    class Program
    {

        static void Main(string[] args)
        {
            var builder = new ContainerBuilder();
            //通常你只想通过接口暴露这个类型： 
            builder.RegisterType<Service>().As<IService>();

            //将您的事务级共享组件注册为InstancePerMatchingLifetimeScope，并为其提供一个“已知标记”，您将在启动新事务时使用它。
            builder.RegisterType<EmailSender>()
               .As<IEmailSender>()
               .InstancePerMatchingLifetimeScope("transaction");

            // 订单处理器和收据管理器都需要发送电子邮件通知。
            builder.RegisterType<OrderProcessor>()
                   .As<IOrderProcessor>();
            builder.RegisterType<ReceiptManager>()
                   .As<IReceiptManager>();

            var container = builder.Build();
            //生命周期 域
            using (var scope = container.BeginLifetimeScope())
            {
                //从作为根容器子项的作用域来解析服务
                var service = scope.Resolve<IService>();

                //您也可以创建嵌套的作用域...
                using (var unitOfWorkScope = scope.BeginLifetimeScope())
                {
                    var anotherService = unitOfWorkScope.Resolve<IOther>();
                }
            }

            //使用标签创建事务范围
            using (var transactionScope = container.BeginLifetimeScope("transaction"))
            {
                using (var orderScope = transactionScope.BeginLifetimeScope())
                {
                    //这将解析一个IEmailSender使用，但IEmailSender将“活”在父事务范围内，并由于该标记在事务范围的任何子节点之间共享。
                    var op = orderScope.Resolve<IOrderProcessor>();
                    op.ProcessOrder();
                }

                using (var receiptScope = transactionScope.BeginLifetimeScope())
                {
                    //这也将解析一个IEmailSender使用，但它会找到父范围内的现有IEmailSender，并使用它。 这将是订单处理器使用的相同实例。
                    var rm = receiptScope.Resolve<IReceiptManager>();
                    rm.SendReceipt();
                }
            }
            //额外注册
            using (var scope = container.BeginLifetimeScope(
              bd =>
              {
                  builder.RegisterType<Service>().As<IService>();
                  //builder.RegisterModule<MyModule>();
              }))
            {
                // 额外的注册将只在这个生命周期范围内可用。
            }
        }
    }
}
