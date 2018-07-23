using Demo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class GodUser:IUser
    {
        public string UserName { get; set; }

        public string AccountId
        {
            get;
            set;
        }

        public GodUser()
        {
            Console.WriteLine("黄金会员-对象-生成");
        }
        
    }
}
