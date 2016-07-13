using System;
using System.Media;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Timers;
using System.Data.Sql;
using System.Data.SqlClient;
using CsvFile;

namespace PatientMonitor1_4
{


    public partial class Form1MultiPatient : Form
    {

        public static string path1 = Application.StartupPath;


        private const int MaxColumns = 64;
        protected string FileName;
        protected bool Modified;

        //path1.Remove(path1.Length - 9);
        //Create a timer 
        //Timer related code from: https://social.msdn.microsoft.com/Forums/windows/en-US/43daf8b2-67ad-4938-98f7-cae3eaa5e63f/how-to-use-timer-control-in-c
        //Ava Heinonen
        System.Windows.Forms.Timer timer = new System.Windows.Forms.Timer();

        //create int i 
        public int i = 0;


        stopwatcher countip = new stopwatcher();

        //create a patient objcts
        Patient myNewPatient;
        Patient myNewPatient2;
        Patient myNewPatient3;
        Patient myNewPatient4;
        Patient myNewPatient5;
        Patient myNewPatient6;
        Patient myNewPatient7;
        Patient myNewPatient8;
        
        public Form1MultiPatient()
        {
            InitializeComponent();

            //Code by Ava Heinonen

            //create an instance of CSVReader class
            CSVReader myNewCSVReader = new CSVReader();

            //Create a string read_bath to path to the CSV files
            string read_path1 = path1 + @"\Bed 1.csv";
            string read_path2 = path1 + @"\Bed 2.csv";
            string read_path3 = path1 + @"\Bed 3.csv";
            string read_path4 = path1 + @"\Bed 4.csv";
            string read_path5 = path1 + @"\Bed 5.csv";
            string read_path6 = path1 + @"\Bed 6.csv";
            string read_path7 = path1 + @"\Bed 7.csv";
            string read_path8 = path1 + @"\Bed 8.csv";

           
            //Create a new objects from Patient class using readFromCSVFile method
            myNewPatient = myNewCSVReader.readFromCSVFile(read_path1);
             myNewPatient2 = myNewCSVReader.readFromCSVFile(read_path2);
            myNewPatient3 = myNewCSVReader.readFromCSVFile(read_path3);
            myNewPatient4 = myNewCSVReader.readFromCSVFile(read_path4);
            myNewPatient5 = myNewCSVReader.readFromCSVFile(read_path5);
            myNewPatient6 = myNewCSVReader.readFromCSVFile(read_path6);
            myNewPatient7 = myNewCSVReader.readFromCSVFile(read_path7);
            myNewPatient8 = myNewCSVReader.readFromCSVFile(read_path8);

            //create a new eventhandler for timer_tick to be done
            //every time timer "ticks"
            timer.Tick += new EventHandler(timer_Tick);

            //set timer interval to 5 seconds
            timer.Interval = (5000) * (1);

            //Enable the timer
            timer.Enabled = true;

            //start the timer 
            timer.Start();


        }

        public void watch_update(string time)
        {
            
        }

        //1406677
        void alarm_logic_3()
        {
            string state = "fine";
            Patient[] beds = new Patient[] { myNewPatient, myNewPatient2, myNewPatient3, myNewPatient4, myNewPatient5, myNewPatient6, myNewPatient7, myNewPatient8 };
            foreach (Patient x in beds )
            {
                //a try catch is used in case of dammaged csv files
                try
                {
                    // these check if they should set of the basic alarm
                    if (x.BreathingMin > Convert.ToInt32(x.BreathingList[i])) { state = "alarm"; }
                    if (x.TemperatureMin > Convert.ToInt32(x.TemperatureList[i])) { state = "alarm"; }
                    if (x.SysBloodMin > Convert.ToInt32(x.SysBloodList[i])) { state = "alarm"; }
                    if (x.PulseMin > Convert.ToInt32(x.PulseList[i])) { state = "alarm"; }
                    if (x.DiasBloodMin > Convert.ToInt32(x.DiasBloodList[i])) { state = "alarm"; }

                    if (x.BreathingMax < Convert.ToInt32(x.BreathingList[i])) { state = "alarm"; }
                    if (x.TemperatureMax < Convert.ToInt32(x.TemperatureList[i])) { state = "alarm"; }
                    if (x.SysBloodMax < Convert.ToInt32(x.SysBloodList[i])) { state = "alarm"; }
                    if (x.PulseMax < Convert.ToInt32(x.PulseList[i])) { state = "alarm"; }
                    if (x.DiasBloodMax < Convert.ToInt32(x.DiasBloodList[i])) { state = "alarm"; }

                }
                catch
                {

                }

                //these check if they should ovverride previous decisions and set of the non mutable alarm
                if (x.BreathingList[i] == "0") { state = "nonmute"; }
                if (x.DiasBloodList[i] == "0") { state = "nonmute"; }
                if (x.TemperatureList[i] == "0") { state = "nonmute"; }
                if (x.SysBloodList[i] == "0") { state = "nonmute"; }
                if (x.PulseList[i] == "0") { state = "nonmute"; }
            }

            // system may set off non mute alarm
            if (state == "nonmute")
            {
                nonmutable_bleep();
                countip.start();
                
            }
            else
            {
                if (state == "fine")
                {
                    //if nothing is wrong then it stops the counter counting the response times
                    this.mute.ForeColor = Color.Black;
                    try
                    {

                        this.watch.Text = "most recent time = " + countip.stop();
                    }
                    catch
                    {

                    }
                    
                }
                else
                {
                    //if something is wrong start the response time clock
                    countip.start();
                    if (this.mute.Checked == true)
                    {
                        this.mute.ForeColor = Color.Red;
                    }
                    else
                    {
                        mutable_bleep();
                    }
                }
            }
        } 

        //Create a method program does
        //every time timer "ticks"
        //code by Ava Heinonen
        void timer_Tick(object sender, EventArgs e)
        {
            
            //increase the counter by 1
            i ++;

            alarm_logic_3();

            //call displayData
            displayData();
        }
        

        private void Form1_Load(object sender, EventArgs e)
        {


            InitializeGrid();
            ClearFile();
            //call displayData
            displayData();


        }

        //Method to display the data from Patient object
        //in the labels on the main form
        //Code by Ava Heinonen
        public void displayData()
        {
            //if the checkbox on top of patient data labels is checked
            //change the text on the labels to be
            //"Measured body function: " + correct entry from list
            if (pulseBox1.Checked)
            {

                P1PulseLabel.Text = "Pulse: " + myNewPatient.PulseList[i];
            }
            //if the box is not checked
            //change the label text to be "body function not currently monitored"
            else
            {
                P1PulseLabel.Text = "Pulse not currently monitored";
            }

            if (BreathingBox1.Checked)
            {
                patient1BreathingLabel.Text = "Breathing Rate: " + myNewPatient.BreathingList[i];
            }
            else
            {
                patient1BreathingLabel.Text = "Breathing Rate not currently monitored";
            }
            if (sysBloodBox1.Checked)
            {
                patient1SysBloodLabel.Text = "Systolic Blood Pressure: " + myNewPatient.SysBloodList[i];
            }
            else
            {
                patient1SysBloodLabel.Text = "Systolic Blood Pressure not currently monitored";
            }
            if (diasBloodBox1.Checked)
            {
                patient1DiasBloodLabel.Text = "Diastolic Blood Pressure: " + myNewPatient.DiasBloodList[i];
            }
            else
            {
                patient1DiasBloodLabel.Text = "Diastolic Blood Pressure not currently monitored";
            }
            if (temperatureBox1.Checked)
            {
                patient1TemperatureLabel.Text = "Temperature: " + myNewPatient.TemperatureList[i];
            }
            else
            {
                patient1TemperatureLabel.Text = "Temperature not currently measured";
            }


            //do the same on labels on patient 2 tab page
            if (p2PulseBox.Checked)
            {
                P2PulseLabel.Text = "Pulse: " + myNewPatient2.PulseList[i];
            }
            else
            {
                P2PulseLabel.Text = "Pulse not currently monitored";
            }
            if (p2BreathingBox.Checked)
            {
                P2BreathingLabel.Text = "Breathing Rate: " + myNewPatient2.BreathingList[i];
            }
            else
            {
                P2BreathingLabel.Text = "Breathing Rate not currently monitored";
            }
            if (p2SysBloodBox.Checked)
            {

                P2SysBloodLabel.Text = "Systolic Blood Pressure: " + myNewPatient2.SysBloodList[i];
            }
            else
            {
                P2SysBloodLabel.Text = "Systolic Blood Pressure not currently monitored";
            }
            if (p2DiasBloodBox.Checked)
            {
                P2DiasBloodLabel.Text = "Diastolic Blood Pressure: " + myNewPatient2.DiasBloodList[i];
            }
            else
            {
                P2DiasBloodLabel.Text = "Diastolic Blood Pressure not currently monitored";
            }
            if (p2TemperatureBox.Checked)
            {
                P2TemperatureLabel.Text = "Temperature: " + myNewPatient2.TemperatureList[i];
            }
            else
            {
                P2TemperatureLabel.Text = "Body Temperature not currently monitored";
            }

            //do the same for patient 3
            if (p3PulseBox.Checked)
            {
                P3PL.Text = "Pulse: " + myNewPatient3.PulseList[i];
            }
            else
            {
                P3PL.Text = "Pulse not currently monitored";

            }
            if (p3BreathingBox.Checked)
            {
                P3BL.Text = "Breathing Rate: " + myNewPatient3.BreathingList[i];
            }
            else
            {
                P3BL.Text = "Breathing rate not currently monitored";
            }
            if (p3SysBloodBox.Checked)
            {
                P3SBL.Text = "Systolic Blood Pressure: " + myNewPatient3.SysBloodList[i];
            }
            else
            {
                P3SBL.Text = "Systolic Blood Pressure not currently monitored";
            }
            P3DBL.Text = "Diastolic Blood Pressure: " + myNewPatient3.DiasBloodList[i];
            P3TL.Text = "Temperature: " + myNewPatient3.TemperatureList[i];

            P4PL.Text = "Pulse: " + myNewPatient4.PulseList[i];
            P4BL.Text = "Breathing Rate: " + myNewPatient4.BreathingList[i];
            P4SBL.Text = "Systolic Blood Pressure: " + myNewPatient4.SysBloodList[i];
            P4DBL.Text = "Diastolic Blood Pressure: " + myNewPatient4.DiasBloodList[i];
            P4TL.Text = "Temperature: " + myNewPatient4.TemperatureList[i];

            P5PL.Text = "Pulse: " + myNewPatient5.PulseList[i];
            P5BL.Text = "Breathing Rate: " + myNewPatient5.BreathingList[i];
            P5SBL.Text = "Systolic Blood Pressure: " + myNewPatient5.SysBloodList[i];
            P5DBL.Text = "Diastolic Blood Pressure: " + myNewPatient5.DiasBloodList[i];
            P5TL.Text = "Temperature: " + myNewPatient5.TemperatureList[i];

            P6PL.Text = "Pulse: " + myNewPatient6.PulseList[i];
            P6BL.Text = "Breathing Rate: " + myNewPatient6.BreathingList[i];
            P6SBL.Text = "Systolic Blood Pressure: " + myNewPatient6.SysBloodList[i];
            P6DBL.Text = "Diastolic Blood Pressure: " + myNewPatient6.DiasBloodList[i];
            P6TL.Text = "Temperature: " + myNewPatient6.TemperatureList[i];

            P7PL.Text = "Pulse: " + myNewPatient7.PulseList[i];
            P7BL.Text = "Breathing Rate: " + myNewPatient7.BreathingList[i];
            P7SBL.Text = "Systolic Blood Pressure: " + myNewPatient7.SysBloodList[i];
            P7DBL.Text = "Diastolic Blood Pressure: " + myNewPatient7.DiasBloodList[i];
            P7TL.Text = "Temperature: " + myNewPatient7.TemperatureList[i];

            P8PL.Text = "Pulse: " + myNewPatient8.PulseList[i];
            P8BL.Text = "Breathing Rate: " + myNewPatient8.BreathingList[i];
            P8SBL.Text = "Systolic Blood Pressure: " + myNewPatient8.SysBloodList[i];
            P8DBL.Text = "Diastolic Blood Pressure: " + myNewPatient8.DiasBloodList[i];
            P8TL.Text = "Temperature: " + myNewPatient8.TemperatureList[i];





        }
        public void mutable_bleep()
        {
            // set off alarm
            // path will need to be figured out
            SoundPlayer simpleSound = new SoundPlayer(path1 + @"\Mutable.wav");
            simpleSound.Play();
        }

        public void nonmutable_bleep()
        {
            // set off alarm
            // path will need to be figured out
            SoundPlayer simpleSound2 = new SoundPlayer(path1 + @"\NonMutable.wav");
            simpleSound2.Play();
        }

        private void label2_Click(object sender, EventArgs e)
        {
           
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void patient1SetButton_Click(object sender, EventArgs e)
        {
            //Code by Ava Heinonen
            //Create a new instance of the Alarm Class
            AlarmClass myNewAlarmClass = new AlarmClass();
            int selectedPatient = tabControl1.SelectedIndex;

            WhichPatientSelected(selectedPatient);
         }

        //check which patient is selected, and call ChangeMinMax on the selected patient
        public void WhichPatientSelected(int bedNumber)
        {
            //Read user selection from the drop down menu, and store it into a string
            //code from https://social.msdn.microsoft.com/Forums/en-US/dfe62a93-0b35-41ef-9858-7fa22980f1a5/how-to-get-combobox-text-in-a-string?forum=csharplanguage
            string selection = patient1SetCombo.SelectedItem.ToString();

            //take the user input integers from the text boxes
            int min = int.Parse(minPatient1.Text);
            int max = int.Parse(maxPatient1.Text);

            //call the ChangeMinMax method on selected patient object with the min, max and selection values
            if (bedNumber == 0)
            {
                myNewPatient.ChangeMinMax(selection, min, max);


            }
            if (bedNumber == 1)
            {
                myNewPatient2.ChangeMinMax(selection, min, max);
            }
            if (bedNumber == 2)
            {

                myNewPatient3.ChangeMinMax(selection, min, max);
            }
            if (bedNumber == 3)
            {
                myNewPatient4.ChangeMinMax(selection, min, max);
            }
            if (bedNumber == 4)
            {
                myNewPatient5.ChangeMinMax(selection, min, max);
            }
            if (bedNumber == 5)
            {
                myNewPatient6.ChangeMinMax(selection, min, max);
            }
            if (bedNumber == 6)
            {
                myNewPatient7.ChangeMinMax(selection, min, max);
            }
            if (bedNumber == 7)
            {
                myNewPatient8.ChangeMinMax(selection, min, max);
            }


        }
        
        private void testLabel_Click(object sender, EventArgs e)
        {

        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void tabControl1_SelectedIndexChanged(object sender, EventArgs e)
        {
            //when tab index is changed on tabcontrol 1
            //change the text on patientNroLabel to be the tab index
            patientNroLabel.Text = "Patient Selected: " + tabControl1.SelectedIndex+1;

        }



        private void groupBox2_Enter(object sender, EventArgs e)
        {

        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveIfModified())
            {
                if (openFileDialog1.ShowDialog(this) == DialogResult.OK)
                    ReadFile(openFileDialog1.FileName);
            }
        }
        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            if (SaveIfModified())
                ClearFile();
        }

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
        {

        }


        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (FileName != null)
                WriteFile(FileName);
            else
                saveAsToolStripMenuItem_Click(sender, e);
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveFileDialog1.FileName = FileName;
            if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                WriteFile(saveFileDialog1.FileName);

        }
        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {


        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (SaveIfModified())
                Close();
        }

        /// <summary>
        /// //////////////////////////////////////////////
        /// </summary>

        private void InitializeGrid()
        {
            for (int i = 1; i <= MaxColumns; i++)
            {
                dataGridView1.Columns.Add(
                    String.Format("Column{0}", i),
                    String.Format("Column {0}", i));
            }
        }

        private void ClearFile()
        {
            dataGridView1.Rows.Clear();
            FileName = null;
            Modified = false;
        }

        private bool ReadFile(string filename)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                dataGridView1.Rows.Clear();
                List<string> columns = new List<string>();
                using (var reader = new CsvFileReader(filename))
                {
                    while (reader.ReadRow(columns))
                    {
                        dataGridView1.Rows.Add(columns.ToArray());
                    }
                }
                FileName = filename;
                Modified = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error reading from {0}.\r\n\r\n{1}", filename, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return false;
        }

        private bool WriteFile(string filename)
        {
            Cursor = Cursors.WaitCursor;
            try
            {
                // Like Excel, we'll get the highest column number used,
                // and then write out that many columns for every row
                int numColumns = GetMaxColumnUsed();
                using (var writer = new CsvFileWriter(filename))
                {
                    foreach (DataGridViewRow row in dataGridView1.Rows)
                    {
                        if (!row.IsNewRow)
                        {
                            List<string> columns = new List<string>();
                            for (int col = 0; col < numColumns; col++)
                                columns.Add((string)row.Cells[col].Value ?? String.Empty);
                            writer.WriteRow(columns);
                        }
                    }
                }
                FileName = filename;
                Modified = false;
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(String.Format("Error writing to {0}.\r\n\r\n{1}", filename, ex.Message));
            }
            finally
            {
                Cursor = Cursors.Default;
            }
            return false;
        }

        // Determines the maximum column number used in the grid
        private int GetMaxColumnUsed()
        {
            int maxColumnUsed = 0;
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                if (!row.IsNewRow)
                {
                    for (int col = row.Cells.Count - 1; col >= 0; col--)
                    {
                        if (row.Cells[col].Value != null)
                        {
                            if (maxColumnUsed < (col + 1))
                                maxColumnUsed = (col + 1);
                            continue;
                        }
                    }
                }
            }
            return maxColumnUsed;
        }

        private bool SaveIfModified()
        {
            if (!Modified)
                return true;

            DialogResult result = MessageBox.Show("The current file has changed. Save changes?", "Save Changes", MessageBoxButtons.YesNoCancel);
            if (result == DialogResult.Yes)
            {
                if (FileName != null)
                {
                    return WriteFile(FileName);
                }
                else
                {
                    saveFileDialog1.FileName = FileName;
                    if (saveFileDialog1.ShowDialog(this) == DialogResult.OK)
                        return WriteFile(saveFileDialog1.FileName);
                    return false;
                }
            }
            else if (result == DialogResult.No)
            {
                return true;
            }
            else // DialogResult.Cancel
            {
                return false;
            }
        }

        private void dataGridView1_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            Modified = true;
        }








    }
}
