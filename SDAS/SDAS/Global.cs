using SDAS_DataAdapter;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS
{
    public class Global
    {
        private static readonly Global mInstance = new Global();
        private Global()
        {
            string DBPath = ConfigurationManager.AppSettings["DBPath"];
            ADA = new AccessDataAdapter("..\\..\\..\\DB\\SDAS.accdb");
        }
        public static Global GetInstance()
        {
            return mInstance;
        }

        /// <summary>
        /// Access数据访问层
        /// </summary>
        public AccessDataAdapter ADA;
    }
}
