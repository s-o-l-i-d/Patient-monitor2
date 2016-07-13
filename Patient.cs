using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor1_4
{
    //Class to store attributes and methods
    //relating to the patient data from CSV files
    //code from VLE example studentdissertations 
    //Code by Ava Heinonen

    public class Patient
    {
        //create lists to store data read from the csv files
        //and integers to store their min and max values
        private List<string> pulseList;
        private int pulseMin;
        private int pulseMax;

        private List<string> breathingList;
        private int breathingMin;
        private int breathingMax;

        private List<string> sysBloodList;
        private int sysBloodMin;
        private int sysBloodMax;

        private List<string> diasBloodList;
        private int diasBloodMin;
        private int diasBloodMax;

        private List<string> temperatureList;
        private int temperatureMin;
        private int temperatureMax;

        
        

        //create a constructor
        public Patient(List<string>pulseList, List<string>breathingList, List<string>sysBloodList, List<string>diasBloodList, List<string>temperatureList)
        {
            this.pulseList = pulseList;
            this.pulseMin = 0;
            this.pulseMax = 100;

            this.breathingList = breathingList;
            this.breathingMin = 0;
            this.breathingMax = 100;

            this.sysBloodList = sysBloodList;
            this.sysBloodMin = 0;
            this.sysBloodMax = 100;

            this.diasBloodList = diasBloodList;
            this.diasBloodMin = 0;
            this.diasBloodMax = 100;

            this.temperatureList = temperatureList;
            this.temperatureMin = 0;
            this.temperatureMax = 100;
            

        }

        //properties
        //lists for storing the patient data
        //and integers to store the min and max values
        //pulse
        public List<string> PulseList 
            {
            get { return pulseList; }
            set { pulseList = value; }
            }
        public int PulseMin
        {
            get { return pulseMin; }
            set { pulseMin = value; }
        }
        public int PulseMax
        {
            get { return pulseMax; }
            set { pulseMax = value; }
        }
        //breathing
        public List<string> BreathingList
        {
            get { return breathingList; }
            set { breathingList = value; }
        }
        public int BreathingMin
        {
            get { return breathingMin; }
            set { breathingMin = value; }
        }
        public int BreathingMax
        {
            get { return breathingMax; }
            set { breathingMax = value;  }
        }
        //Systolic Blood Pressure
        public List<string> SysBloodList
        {
            get { return sysBloodList; }
            set { sysBloodList = value; }

        }
        public int SysBloodMin
        {
            get { return sysBloodMin; }
            set { sysBloodMin = value; }
        }
        public int SysBloodMax
        {
            get { return sysBloodMax; }
            set { sysBloodMax = value; }
        }
        //Diastolic Blood Pressure
        public List<string> DiasBloodList
        {
            get { return diasBloodList; }
            set { diasBloodList = value; }
        }
        public int DiasBloodMin
        {
            get { return diasBloodMin; }
            set { diasBloodMin = value; }
        }
        public int DiasBloodMax
        {
            get { return diasBloodMax; }
            set { diasBloodMax = value; }
        }
        //temperature
        public List<string> TemperatureList
        {
            get { return temperatureList; }
            set { temperatureList = value; }
        }
        public int TemperatureMin
        {
            get { return temperatureMin; }
            set { temperatureMin = value; }
        }
            public int TemperatureMax
        {
            get { return temperatureMax; }
            set { temperatureMax = value; }
        }

        //change min and max values
        public void ChangeMinMax(string selection, int min, int max)
        {
            
            //if selection is "pulse"
            //change min and max values according to user selection
            if (selection == "Pulse")
            {
                pulseMin = min;
                pulseMax = max;

            }
            //do the same for breathing
            if (selection == "Breathing")
            {
                breathingMin = min;
                breathingMax = max;
            }
            //and so on with systolic/Diastolic blood pressure and temperature as well
            if (selection == "Systolic Blood Pressure")
            {
                sysBloodMin = min;
                sysBloodMax = max;

            }
            if (selection == "Diastolic Blood Pressure")
            {
                diasBloodMin = min;
                diasBloodMax = max;
            }
            if (selection == "Temperature")
            {
                temperatureMin = min;
                temperatureMax = max;
            }
        }




    }

}
