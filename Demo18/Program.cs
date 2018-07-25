using Autofac;
using Autofac.Configuration;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo18
{
    class Program
    {
        static void Main(string[] args)
        {
            //需要.net >4.5.1

            //Microsoft.Extensions.Configuration.ConfigurationBuilder
            //将配置添加到ConfigurationBuilder
            var config = new ConfigurationBuilder();
            //config.AddJsonFile来自Microsoft.Extensions.Configuration.Json
            //config.AddXmlFile来自Microsoft.Extensions.Configuration.Xml
            //ConfigurationProvider
              

            //用Autofac注册ConfigurationModule
            var module = new ConfigurationModule(config.Build());
            var builder = new ContainerBuilder();
            builder.RegisterModule(module);
        }
    }
}
