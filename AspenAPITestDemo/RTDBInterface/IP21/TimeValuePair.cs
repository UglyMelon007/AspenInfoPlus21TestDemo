using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    public class TimeValuePair
    {
        private DateTime time;
        private double value;

        public TimeValuePair()
        {
        }

        public TimeValuePair(DateTime time, double value)
        {
            this.time = time;
            this.value = value;
        }

        public DateTime Time
        {
            get { return this.time; }
            set { this.time = value; }
        }

        public double Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }
}
