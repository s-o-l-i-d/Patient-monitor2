using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor1_4
{
    public class CSVReader
    {
        //Methods and attributes to display data from CSV files 
        //on the GUI

        //Declare variables to store the file path
        protected static string readline;
        protected static string readpath;

        //method to read data from the CSV file  return it as patient object
        //code from http://stackoverflow.com/questions/5282999/reading-csv-file-and-storing-values-into-an-array
        public Patient readFromCSVFile(string readpath)
        {
            //code from:https://msdn.microsoft.com/en-us/library/f2ke0fzy(v=vs.110).aspx

            //Connect to streamreader using the filepath
            //correct string mannips
            using (System.IO.StreamReader readings = new System.IO.StreamReader(readpath))
            {
                //lists to store the values 
                List<string> pulse = new List<string>();
                List<string> breathing = new List<string>();
                List<string> sysBlood = new List<string>();
                List<string> diasBlood = new List<string>();
                List<string> temperature = new List<string>();

                readings.ReadLine();

                //Read the file until the end
                while (!readings.EndOfStream)
                {
                    
                    var line = readings.ReadLine();
                    var values = line.Split(',');

                    

                    //add read values into lists
                    pulse.Add(values[0]);
                    breathing.Add(values[1]);
                    sysBlood.Add(values[2]);
                    diasBlood.Add(values[3]);
                    temperature.Add(values[4]);


                }

                //Create a patient object from the 5 lists read from CSV files
                //return the patient object
                Patient myCoolPatient = new Patient(pulse, breathing, sysBlood, diasBlood, temperature);
                return myCoolPatient;
            }
        }
    }
}
