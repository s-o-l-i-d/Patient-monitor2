using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;


namespace PatientMonitor1_4
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }
        ConStaDatabase objConnect;
        string conString;

        DataSet ds;
        DataRow dRow;

        int MaxRows;
        int inc;

        bool validate = false;

        private void Form2_Load(object sender, EventArgs e)
        {

            try
            {
                objConnect = new ConStaDatabase();
                conString = PatientMonitor.Properties.Settings.Default.ConStaDBConnString;
                objConnect.connection_string = conString;
                objConnect.Sql = PatientMonitor.Properties.Settings.Default.SQL;

                ds = objConnect.GetConnection;
                MaxRows = ds.Tables[0].Rows.Count;

            }
            catch(Exception err)
            {
                MessageBox.Show(err.Message);
            }
             
        }

        private void NavigateRecords()
        {
            dRow = ds.Tables[0].Rows[inc];
            textBox1.Text = dRow.ItemArray.GetValue(1).ToString();
            textBox2.Text = dRow.ItemArray.GetValue(2).ToString();
            textBox1.Text = dRow.ItemArray.GetValue(3).ToString();
        }

        private void Validation()
        {           
            for (int i = 0; i < MaxRows; i++)
            {
                dRow = ds.Tables[0].Rows[i];
                Console.WriteLine( dRow.ItemArray.GetValue(0).ToString());
                Console.WriteLine(dRow.ItemArray.GetValue(1).ToString());

                string password = dRow.ItemArray.GetValue(1).ToString();
                string staffid = dRow.ItemArray.GetValue(0).ToString();

                if ( ( textBox1.Text == staffid  ) &&  (textBox2.Text == password)) {
               
                    validate = true;
                    string login_time = DateTime.Now.ToString();
                   Console.WriteLine( dRow.ItemArray.Length.ToString());
                   // dRow.ItemArray.SetValue(login_time,3);
                   // objConnect.UpdateDatabase(ds);

                   // Console.WriteLine(dRow.ItemArray.GetValue(3).ToString());
                }          
            }
            
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Validation();
            if(validate)
            {
                Console.WriteLine("Success");
            }
            else
            {
                Console.WriteLine("really");
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
        
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
            
        }
    }
}

