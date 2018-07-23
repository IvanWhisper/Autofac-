using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Interface
{
    public interface IUser
    {
        string AccountId { get; set; }
        string UserName { get; set; }
    }
}
