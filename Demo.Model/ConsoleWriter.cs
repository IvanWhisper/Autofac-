using Demo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class ConsoleWriter : IWriter
    {
        public void OutWt(string str)
        {
            Console.WriteLine(str);
        }
    }
}
