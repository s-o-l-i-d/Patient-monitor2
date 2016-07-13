using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Diagnostics;
using System.IO;
using System.Threading.Tasks;

namespace PatientMonitor1_4
{
    public class stopwatcher
    {

        //basicly this whole class is just a stopwatch for response times

        Stopwatch stopWatch1 = new Stopwatch();
        public void start()
        {
            stopWatch1.Start();
        }
        public string stop()
        {
            stopWatch1.Stop();

            TimeSpan ts = stopWatch1.Elapsed;
            string elapsedTime = String.Format("{0:00}:{1:00}:{2:00}",
                ts.Minutes, ts.Seconds, ts.Milliseconds / 10);
            //Form1MultiPatient.ActiveForm.
            return elapsedTime;
        }
    }
}
