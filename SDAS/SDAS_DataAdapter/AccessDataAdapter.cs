﻿using SDAS_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS_DataAdapter
{
    public class AccessDataAdapter
    {
        private OleDbConnection ODConnection;
        private OleDbDataAdapter OCAdapter;

        public AccessDataAdapter(string path)
        {
            OCAdapter = new OleDbDataAdapter();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
            ODConnection = new OleDbConnection(connectionString);
        }

        private DataTable DoSearch(string querystring)
        {
            DataTable Data = new DataTable();
            OleDbCommand command = new OleDbCommand(querystring, ODConnection);
            OCAdapter.SelectCommand = command;

            ODConnection.Open();
            OCAdapter.Fill(Data);
            ODConnection.Close();

            return Data;
        }

        public User Login(string UserName, string Password)
        {
            User result = null;

            string querystring = "SELECT * FROM 用户信息 WHERE 用户名='" + UserName + "' AND 密码='" + Password + "'";
            DataTable Data = DoSearch(querystring);

            if (Data.Rows.Count > 0)
            {
                result = new User();
                result.ID = (int)Data.Rows[0]["ID"];
                result.Name = Data.Rows[0]["姓名"].ToString();
                result.Role = Data.Rows[0]["角色"].ToString();
            }

            return result;
        }

        public List<Order> GetAllOrderByUserID(int UserID)
        {
            List<Order> Orders = new List<Order>();

            string querystring = "SELECT * FROM 订单信息 WHERE 销售=" + UserID;
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                Order Order = new Order();
                Order.ID = (int)row["ID"];

                Order.Saler.ID = (int)row["销售"];
                Order.Saler = GetUserByID(Order.Saler.ID);

                Order.Customer.ID = (int)row["客户"];
                Order.Customer = GetCustomerByID(Order.Customer.ID);

                Order.State = row["状态"].ToString();
                int.TryParse(row["认购ID"].ToString(),out Order.Offer.ID);
                int.TryParse(row["审核ID"].ToString(), out Order.Verify.ID);
                int.TryParse(row["签约ID"].ToString(), out Order.Contract.ID);
                int.TryParse(row["产权ID"].ToString(), out Order.PropertyRights.ID);
                DateTime.TryParse(row["放款日期"].ToString(),out Order.LoanDate);
                DateTime.TryParse(row["入住日期"].ToString(), out Order.CheckInDate);
                Orders.Add(Order);
            }

            return Orders;
        }

        public User GetUserByID(int UserID)
        {
            User user = new User();

            string querystring = "SELECT * FROM 用户信息 WHERE ID=" + UserID;
            DataTable Data = DoSearch(querystring);

            if (Data.Rows.Count > 0)
            {
                user.ID = (int)Data.Rows[0]["ID"];
                user.Name = Data.Rows[0]["姓名"].ToString();
                user.Role = Data.Rows[0]["角色"].ToString();
            }
            else
            {
                return null;
            }

            return user;
        }

        public Customer GetCustomerByID(int CustomerID)
        {
            Customer customer = new Customer();

            string querystring = "SELECT * FROM 客户信息 WHERE ID=" + CustomerID;
            DataTable Data = DoSearch(querystring);

            if (Data.Rows.Count > 0)
            {
                customer.ID = (int)Data.Rows[0]["ID"];
                customer.Name = Data.Rows[0]["姓名"].ToString();
                customer.Sex = Data.Rows[0]["性别"].ToString();

                int age;
                int.TryParse(Data.Rows[0]["年龄"].ToString(), out age);
                customer.Age = age;

                customer.IDNumber = Data.Rows[0]["身份证号"].ToString();
                customer.PhoneNumber = Data.Rows[0]["手机号"].ToString();

                string Residence = GetAdministrativeNameByCode(Data.Rows[0]["居住地区"].ToString());
                customer.Residence = string.IsNullOrEmpty(Residence) ? "未找到" : Residence;

                string WorkPlace = GetAdministrativeNameByCode(Data.Rows[0]["工作地区"].ToString());
                customer.WorkPlace = string.IsNullOrEmpty(WorkPlace) ? "未找到" : WorkPlace;

                int.TryParse(Data.Rows[0]["家庭人数"].ToString(), out customer.FamilyNumber);
                customer.Channel = Data.Rows[0]["首访渠道"].ToString();
                customer.EducationalBackground = Data.Rows[0]["文化水平"].ToString();
                customer.VisitWay = Data.Rows[0]["首访途径"].ToString();
            }
            else
            {
                return null;
            }

            return customer;
        }

        public string GetAdministrativeNameByCode(string Code)
        {
            string name = "";

            string querystring = "SELECT * FROM 行政区划 WHERE 代码='" + Code + "'";
            DataTable Data = DoSearch(querystring);

            if (Data.Rows.Count > 0)
            {
                name = Data.Rows[0]["名称"].ToString();
            }

            return name;
        }
    }
}
