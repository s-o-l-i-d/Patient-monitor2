using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor1_4
{
    class ConStaDatabase
    {
   
        private static string sql_string;
        private static string strCon;
        System.Data.SqlClient.SqlDataAdapter da_1;

          
        public string Sql
        {
            set { sql_string = value; }
        }

        public string connection_string
        {
            set { strCon = value; }
        }
        //hold the data from the table and using MyDtaset to connect and flll the dataset
        public System.Data.DataSet GetConnection
        {
            get
            { return MyDataSet(); }
        }
        private System.Data.DataSet MyDataSet()
        {
            System.Data.SqlClient.SqlConnection con = new System.Data.SqlClient.SqlConnection(strCon);
            //open connecton to database
            con.Open(); 
            //to open the datathe table in the database
            da_1 = new System.Data.SqlClient.SqlDataAdapter(sql_string, con);
            //set DataSet object
            System.Data.DataSet dat_set = new System.Data.DataSet();
            //fill method of our DataAdapter object
            da_1.Fill(dat_set, "Table_Data_1");

            con.Close();

            return dat_set;
        }

        public void UpdateDatabase(System.Data.DataSet ds)
        {
            System.Data.SqlClient.SqlCommandBuilder cb = new System.Data.SqlClient.SqlCommandBuilder(da_1);

            cb.DataAdapter.Update(ds.Tables[0]);
        }

    }
 }
