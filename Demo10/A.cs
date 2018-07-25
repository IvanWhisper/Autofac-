using Autofac.Features.Indexed;
using Autofac.Features.Metadata;
using Autofac.Features.OwnedInstances;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo10
{
    //直接依赖(B)
    public class A
    {
        public A(B dependency)
        {

        }
    }
    //延迟实例化 (Lazy<B>)
    public class A_Lazy
    {
        Lazy<B> _b;

        public A_Lazy(Lazy<B> b) { _b = b; }

        public void M()
        {
            //实现B的组件是在第一次调用`M()`时创建的
            _b.Value.DoSomething();
        }
    }
    //受控生命周期（Owned<B>） 若B已注册为单例，则重新生成一个b
    public class A_Owned
    {
        Owned<B> _b;

        public A_Owned(Owned<B> b) { _b = b; }

        public void M()
        {
            //_b用于某个任务
            _b.Value.DoSomething();

            //这里_b不再需要，所以它被释放
            _b.Dispose();
        }
    }
    //动态实例化(Func<B>) 参数实例化（Func<X, Y, B>）
    //Autofac将这些视为键入的参数。 这意味着自动生成的函数工厂在输入参数列表中不能有重复的类型
    //可能需要注册该类型，并为其自动生成一个函数工厂
    //使用此关系类型以及使用委托工厂时，生命周期作用域受到重视。
    //如果将对象注册为InstancePerDependency()并多次调用Func<X, Y, B>，
    //则每次都会得到一个新实例。但是，如果将一个对象注册为SingleInstance()并调用Func<X, Y, B>来多次解析对象，
    //那么无论您传入哪个不同的参数，每次都会得到相同的对象实例。
      //不同的参数不会打破生命周期作用域的重用。
    public class A_Func
    {
        Func<B> _b;

        public A_Func(Func<B> b) { _b = b; }

        public void M()
        {
            var b = _b();
            b.DoSomething();
        }
    }
    //元数据询问（Meta<B>, Meta<B, X>） Meta<B, BMetadata> _b;
    public class A_Meta
    {
        Meta<B> _b;
        public A_Meta(Meta<B> b) { _b = b; }

        public void M()
        {
            if (_b.Metadata["SomeValue"] == "yes")
            {
                _b.Value.DoSomething();
            }
        }
    }
    //带键服务查找（IIndex<X, B>）
    public class A_Index
    {
        IIndex<string, B> _b;
        public A_Index(IIndex<string, B> b) { _b = b; }

        public void M()
        {
            var b = this._b["first"];
            b.DoSomething();
        }
    }
}
