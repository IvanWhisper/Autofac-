using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo04
{
    public class ConfigReader : IConfigReader
    {
        public ConfigReader(string configSectionName)
        {
            //存储配置节名称
            Console.WriteLine("构造");
        }

        // ...根据段名称读取配置。
    }
}
