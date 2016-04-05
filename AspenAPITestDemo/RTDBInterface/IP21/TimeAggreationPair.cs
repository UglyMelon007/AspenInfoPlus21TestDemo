using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21 {
    public class TimeAggreationPair {
        public DateTime Time { get; set; }
        public AggreationData AggreationData { get; set; }

        public TimeAggreationPair(DateTime time, AggreationData aggreationData) {
            this.AggreationData = aggreationData;
            this.Time = time;
        }
    }
}
