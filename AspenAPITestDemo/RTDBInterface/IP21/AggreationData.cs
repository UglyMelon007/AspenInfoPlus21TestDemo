using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21 {
    public class AggreationData {
        public double Max { get; set; }
        public double Min { get; set; }
        public double Avg { get; set; }
        public double Sum { get; set; }

        public AggreationData(double min, double avg, double max, double sum) {
            this.Avg = avg;
            this.Max = max;
            this.Min = min;
            this.Sum = sum;
        }

        public AggreationData() {
            
        }
    }
}
