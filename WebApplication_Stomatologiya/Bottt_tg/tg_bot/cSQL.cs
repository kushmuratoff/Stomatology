using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace tg_bot
{
    class cSQL
    {
        SqlConnection cn;
        // OleDbConnection cn;
        string ConnectionString;

        public void cSQL_init(string ConnectionString)
        {
            this.ConnectionString = ConnectionString;
        }

        public void Connect()
        {
            try
            {
                cn = new SqlConnection();
                cn.ConnectionString = ConnectionString;
                cn.Open();
                // System.Windows.Forms.MessageBox.Show("ddd");

            }

            catch (Exception exp)
            {
               // System.Windows.Forms.MessageBox.Show("Can't connect to SQL Server" + exp.Message);
                Console.WriteLine("Can't connect to SQL Server" + exp.ToString());

                return;
            }
        }

        public void Disconnect()
        {
            try
            {
                cn.Close();
            }
            catch (Exception exp)
            {
              //  System.Windows.Forms.MessageBox.Show("Can't connect to SQL Server" + exp.Message);
                Console.WriteLine("Can't connect to SQL Server" + exp.ToString());

                return;
            }
        }

        public DataTable Query(string SQLstring)
        {
            SqlCommand cm = null;
            try
            {
                cm = new SqlCommand(SQLstring, cn);
                SqlDataAdapter da = new SqlDataAdapter();
                da.SelectCommand = cm;

                DataTable _table = new DataTable();
                da.Fill(_table);
                da.Dispose();
                cm.Dispose();
                return _table;
            }
            catch (Exception exp)
            {
                //System.Windows.Forms.MessageBox.Show(exp.Message);
                Console.WriteLine(exp.ToString());

                cm.Dispose();
                return null;
            }

        }

        public int SetCommand(string SQLstring)
        {
            SqlCommand cm = null;
            int res = -1;
            try
            {
                cm = new SqlCommand(SQLstring, cn);
                res = cm.ExecuteNonQuery();
                cm.Dispose();
            }
            catch (Exception exp)
            {
               // System.Windows.Forms.MessageBox.Show("SQL Base Drive.\n" + exp.Message);
                Console.WriteLine("SQL Base Drive.\n" + exp.ToString());

                cm.Dispose();
                return res;
            }
            return res;

        }

    }
}
