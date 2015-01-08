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

        public bool Login(string UserName,string Password)
        {
            bool result = false;

            DataTable Data = new DataTable();
            string querystring = "SELECT * FROM 用户信息 WHERE 用户名='" + UserName + "' AND 密码='" + Password + "'";
            OleDbCommand command = new OleDbCommand(querystring, ODConnection);
            OCAdapter.SelectCommand = command;

            ODConnection.Open();
            OCAdapter.Fill(Data);
            ODConnection.Close();

            if (Data.Rows.Count > 0)
            {
                result = true;
            }

            return result;
        }

        public bool GetOrder(string UserName, string Password)
        {
        }
    }
}
