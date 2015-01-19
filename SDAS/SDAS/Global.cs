using SDAS_DataAdapter;
using SDAS_Model;
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
            ADA = new AccessDataAdapter(DBPath);
        }
        public static Global GetInstance()
        {
            return mInstance;
        }

        /// <summary>
        /// Access数据访问层
        /// </summary>
        public AccessDataAdapter ADA;

        /// <summary>
        /// 当前销售
        /// </summary>
        public User CurrentUser;

        /// <summary>
        /// 用户信息缓存
        /// </summary>
        public Dictionary<int, Customer> CustomerCache = new Dictionary<int, Customer>();

        public Customer GetCustomerByID(int CustomerID)
        {
            if(CustomerCache.ContainsKey(CustomerID))
            {
                return CustomerCache[CustomerID];
            }
            else
            {
                Customer customer = new Customer();

                customer = ADA.GetCustomerByID(CustomerID);
                CustomerCache.Add(CustomerID,customer);

                return customer;
            }
        }
    }
}
