using Demo.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.Service
{
    public class PrintService:IService
    {
        private IWriter _writer;
        private IUser _user;
        public PrintService(IWriter writer)
        {
            this._writer = writer;
        }
        public PrintService(IWriter writer,IUser user)
        {
            this._writer = writer;
            this._user = user;
        }

        public void WriteData(string str)
        {
            if (_user == null)
            {
                _writer.OutWt("输出数据:" + str);
            }
            else
            {
                _writer.OutWt("输出数据:" + str);
            }
        }
    }
}
