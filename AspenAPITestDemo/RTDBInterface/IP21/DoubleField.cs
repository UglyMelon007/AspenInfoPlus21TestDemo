using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    internal class DoubleField : Field
    {
        public DoubleField(string name)
            : base(name)
        {
        }

        public override ushort DataType
        {
            get { return infoplus21_api.DTYPDUBL; }
        }

        protected override object AllocMyBuffer()
        {
            return new double[this.bufferSize];
        }

        public override object DataAt(int index)
        {
            return ((double[])this.buffer)[index];
        }
    }
}
