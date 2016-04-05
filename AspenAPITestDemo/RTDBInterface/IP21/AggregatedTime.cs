using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    internal class AggregatedTime : AggregatedData
    {
        protected override object AllocMyBuffer()
        {
            return new infoplus21_api.XUSTS[this.MaxPeriods, this.Codes.Length];
        }

        protected override object MyDataAt(int period, int index)
        {
            return ((infoplus21_api.XUSTS[,])this.buffer)[period, index];
        }
    }
}
