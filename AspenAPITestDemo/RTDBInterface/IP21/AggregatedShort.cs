using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    internal class AggregatedShort : AggregatedData
    {
        protected override object AllocMyBuffer()
        {
            return new short[this.MaxPeriods, this.Codes.Length];
        }

        protected override object MyDataAt(int period, int index)
        {
            return ((short[,])this.buffer)[period, index];
        }
    }
}
