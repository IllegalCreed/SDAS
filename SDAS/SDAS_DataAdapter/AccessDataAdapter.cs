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
            DataSet myDataSet = new DataSet();
            OCAdapter = new OleDbDataAdapter();

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path;
            ODConnection = new OleDbConnection(connectionString);

            OleDbCommand command = new OleDbCommand("SELECT * FROM 客户信息", ODConnection);
            OCAdapter.SelectCommand = command;

            ODConnection.Open();
            OCAdapter.Fill(myDataSet, "客户信息");
            ODConnection.Close();
        }
        
    }
}
