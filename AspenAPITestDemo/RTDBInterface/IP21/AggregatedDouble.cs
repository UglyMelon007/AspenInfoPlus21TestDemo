using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    internal class AggregatedDouble : AggregatedData
    {
        protected override object AllocMyBuffer()
        {
            return new double[this.MaxPeriods, this.Codes.Length];
        }

        protected override object MyDataAt(int period, int index)
        {
            return ((double[,])this.buffer)[period, index];
        }
    }
}
