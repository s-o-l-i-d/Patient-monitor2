using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PatientMonitor1_4
{
    public class AlarmClass
    {
        //these values are the minimum levels for the respective readings
        public static double min_pulse = 50;
        public static double min_breathing = 10;
        public static double min_blood_systolic = 90;
        public static double min_blood_diastolic = 60;
        public static double min_temperature = 30;


        //these values are the maximum levels for the respective readings

        public static double max_pulse = 70;
        public static double max_breathing = 30;
        public static double max_blood_systolic = 110;
        public static double max_blood_diastolic = 80;
        public static double max_temperature = 40;

        
        //method to change min and max value limits
        public void ChangeMinMax(int min, int max, string selection, int patientNumber)
        {
            //read trough selection
            //if selection is "pulse"
            //change min and max values according to user selection
            if (selection == "Pulse")
            {
                min_pulse = min;
                max_pulse = max;
                
            }
            //do the same for breathing
            if (selection == "Breathing")
            {
                min_breathing = min;
                max_breathing = max;
            }
            //and so on with systolic/Diastolic blood pressure and temperature as well
            if (selection == "Systolic Blood Pressure")
            {
                min_blood_systolic = min;
                max_blood_systolic = max;

            }
            if (selection == "Diastolic Blood Pressure")
            {
                min_blood_diastolic = min;
                max_blood_diastolic = max;
            }
            if (selection == "Temperature")
            {
                min_temperature = min;
                max_temperature = max;
            }
        }
        public List<double> grab()
        {
            //this exists to help other classes to read the min max values
            List<double> package = new List<double>();
            package.Add(min_pulse);
            package.Add(min_breathing);
            package.Add(min_blood_systolic);
            package.Add(min_blood_diastolic);
            package.Add(min_temperature);
            package.Add(max_pulse);
            package.Add(max_breathing);
            package.Add(max_blood_systolic);
            package.Add(max_blood_diastolic);
            package.Add(max_temperature);


            return package;
        }




    }
}
