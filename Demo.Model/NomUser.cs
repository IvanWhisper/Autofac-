using Demo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Model
{
    public class NomUser:IUser
    {
        public string AccountId { get; set; }
        public string UserName { get; set; }
        public NomUser()
        {
            Console.WriteLine("普通会员-对象-生成");
        }
    }
}
