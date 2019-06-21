using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DependencyInjectionDemo.Services
{
    public class SqlDataManger:IDataManager
    {
        public string GetMessage()
        {
            return "From SqlDataManager";
              
        }
    }
}
