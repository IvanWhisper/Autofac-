using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lib07
{
    public class AModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new AComponent()).As<AComponent>();
            Console.WriteLine("A load");
        }
    }
    public class BModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.Register(c => new BComponent()).As<BComponent>();
            Console.WriteLine("B load");

        }
    }
    public class AComponent
    {
        public void show() { Console.WriteLine("A comp show"); }

    }
    public class BComponent
    {
        public void show() { Console.WriteLine("B comp show"); }

    }
}
