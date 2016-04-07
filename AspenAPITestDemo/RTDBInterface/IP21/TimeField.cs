using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    internal class TimeField : Field
    {
        public TimeField(string name)
            : base(name)
        {
        }

        public override ushort DataType
        {
            get { return infoplus21_api.DTYPXTIM; }
        }

        protected override object AllocMyBuffer()
        {
            return new infoplus21_api.XTSBLOCK[this.bufferSize];
        }

        public override object DataAt(int index)
        {
            return ((infoplus21_api.XTSBLOCK[])this.buffer)[index];
        }
    }
}
