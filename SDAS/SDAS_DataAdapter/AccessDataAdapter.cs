using SDAS_Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDAS_DataAdapter
{
    public class AccessDataAdapter
    {
        private OleDbConnection ODConnection;
        private OleDbDataAdapter ODAdapter;

        public AccessDataAdapter(string path)
        {
            ODAdapter = new OleDbDataAdapter();
            //ODAdapter.RowUpdated += new OleDbRowUpdatedEventHandler(HandleRowUpdated);

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
            ODConnection = new OleDbConnection(connectionString);
        }

        private DataTable DoSearch(string querystring)
        {
            DataTable Data = new DataTable();
            OleDbCommand command = new OleDbCommand(querystring, ODConnection);
            ODAdapter.SelectCommand = command;

            ODConnection.Open();
            ODAdapter.Fill(Data);
            ODConnection.Close();

            return Data;
        }

        private int DoInsert(string querystring)
        {
            OleDbCommand command = new OleDbCommand(querystring, ODConnection);

            ODConnection.Open();
            command.ExecuteNonQuery();
            command.CommandText = "SELECT @@IDENTITY";
            int i = Int32.Parse(command.ExecuteScalar().ToString());
            ODConnection.Close();

            return i;
        }

        //private void HandleRowUpdated(object sender, OleDbRowUpdatedEventArgs e)
        //{
        //    if (e.Status == UpdateStatus.Continue && e.StatementType == StatementType.Insert)
        //    {
        //        OleDbCommand cmdGetIdentity = new OleDbCommand();
        //        cmdGetIdentity.CommandText = "SELECT @@IDENTITY";
        //        cmdGetIdentity.Connection = ODConnection;

        //        // Get the Identity column value
        //        int i = Int32.Parse(cmdGetIdentity.ExecuteScalar().ToString());
        //        e.Row.AcceptChanges();
        //    }
        //}

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
                //Order.Saler = GetUserByID(Order.Saler.ID);

                Order.Customer.ID = (int)row["客户"];
                //Order.Customer = GetCustomerByID(Order.Customer.ID);

                Order.State = row["状态"].ToString();
                int.TryParse(row["认购ID"].ToString(),out Order.Offer.ID);
                int.TryParse(row["审核ID"].ToString(), out Order.Verify.ID);
                int.TryParse(row["签约ID"].ToString(), out Order.Contract.ID);
                int.TryParse(row["产权ID"].ToString(), out Order.PropertyRights.ID);
                DateTime.TryParse(row["放款日期"].ToString(),out Order.LoanDate);
                DateTime.TryParse(row["入住日期"].ToString(), out Order.CheckInDate);

                DateTime firstDate = new DateTime();
                DateTime.TryParse(row["创建日期"].ToString(), out firstDate);
                Order.FirstDate = firstDate;

                DateTime lastDate = new DateTime();
                DateTime.TryParse(row["更新日期"].ToString(), out lastDate);
                Order.LastDate = lastDate;

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
                customer.Residence.Code = Residence;
                customer.Residence.Name = string.IsNullOrEmpty(Residence) ? "未找到" : Residence;

                string WorkPlace = GetAdministrativeNameByCode(Data.Rows[0]["工作地区"].ToString());
                customer.WorkPlace.Code = WorkPlace;
                customer.WorkPlace.Name = string.IsNullOrEmpty(WorkPlace) ? "未找到" : WorkPlace;

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

        public List<VisitLog> GetVisitLogsByOrderID(int ID)
        {
            List<VisitLog> VisitLogs = new List<VisitLog>();

            string querystring = "SELECT * FROM 来电来访日志 WHERE 所属订单=" + ID + " ORDER BY 日期 DESC";
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                VisitLog VisitLog = new VisitLog();
                VisitLog.ID = (int)row["ID"];
                VisitLog.State = row["状态"].ToString();
                VisitLog.VisitWay = row["途径"].ToString();
                DateTime.TryParse(row["日期"].ToString(), out VisitLog.Date);
                VisitLog.Content = row["内容"].ToString();
                VisitLogs.Add(VisitLog);
            }

            return VisitLogs;
        }

        public List<string> GetEducationalBackgrounds()
        {
            List<string> EducationalBackgrounds = new List<string>();

            string querystring = "SELECT * FROM 文化水平";
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                string kind = row[0].ToString();
                EducationalBackgrounds.Add(kind);
            }

            return EducationalBackgrounds;
        }

        public List<string> GetVisitWays()
        {
            List<string> VisitWays = new List<string>();

            string querystring = "SELECT * FROM 途径类型";
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                string kind = row[0].ToString();
                VisitWays.Add(kind);
            }

            return VisitWays;
        }

        public List<string> GetChannels()
        {
            List<string> Channels = new List<string>();

            string querystring = "SELECT * FROM 渠道类型";
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                string kind = row[0].ToString();
                Channels.Add(kind);
            }

            return Channels;
        }

        public List<Administrative> GetProvinces()
        {
            List<Administrative> Provinces = new List<Administrative>();

            string querystring = "SELECT * FROM 行政区划 WHERE 行政级别='省'";
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                Administrative administrative = new Administrative();
                administrative.Code = row["代码"].ToString();
                administrative.Name = row["名称"].ToString();
                Provinces.Add(administrative);
            }

            return Provinces;
        }

        public List<Administrative> GetSubordinateAdministrativeByCode(string code)
        {
            List<Administrative> Administratives = new List<Administrative>();

            string querystring = "SELECT * FROM 行政区划 WHERE 代码 = ANY ( SELECT 下级代码 FROM 行政从属 WHERE 上级代码 = '" + code + "' )";
            DataTable Data = DoSearch(querystring);

            foreach (DataRow row in Data.Rows)
            {
                Administrative administrative = new Administrative();
                administrative.Code = row["代码"].ToString();
                administrative.Name = row["名称"].ToString();
                Administratives.Add(administrative);
            }

            return Administratives;
        }

        public int AddOrder(int CustomerID,int SalerID)
        {
            string querystring = "INSERT INTO 订单信息 (客户,状态,销售,创建日期,更新日期) " + "VALUES (" + CustomerID + ",'首访'," + SalerID + ",'" + DateTime.Now + "','" + DateTime.Now + "')";
            return DoInsert(querystring);
        }

        public int AddCustomer(Customer customer)
        {
            string querystring = "INSERT INTO 客户信息 (姓名,性别,年龄,身份证号,手机号,居住地区,工作地区,家庭人数,首访渠道,文化水平,首访途径) " + "VALUES ('" + customer.Name + "','" + customer.Sex + "'," + customer.Age + ",'" + customer.IDNumber + "','" + customer.PhoneNumber + "','" + customer.Residence.Code + "','" + customer.WorkPlace.Code + "'," + customer.FamilyNumber + ",'" + customer.Channel + "','" + customer.EducationalBackground + "','" + customer.VisitWay+ "')";
            return DoInsert(querystring);
        }
    }
}
