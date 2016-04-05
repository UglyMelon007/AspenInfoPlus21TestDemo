using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace RTDB.IP21
{
    internal abstract class AggregatedData
    {
        public enum Type {DOUBLE, TIME, SHORT};

        private short[] codes;
        private int maxPeriods;
        protected object buffer;
        private GCHandle bufferHandle;

        public short[] Codes
        {
            get { return this.codes; }
            set { this.codes = value; }
        }

        public int MaxPeriods
        {
            get { return this.maxPeriods; }
        }

        public IntPtr AllocBuffer(int maxPeriods)
        {
            this.maxPeriods = maxPeriods;
            this.buffer = AllocMyBuffer();
            this.bufferHandle = GCHandle.Alloc(this.buffer, GCHandleType.Pinned);
            return this.bufferHandle.AddrOfPinnedObject();
        }

        public void FreeBuffer()
        {
            this.bufferHandle.Free();
        }

        protected abstract object AllocMyBuffer();

        public object DataAt(int period, short code)
        {
            for (int codeIndex = 0; codeIndex < this.codes.Length; codeIndex ++)
            {
                if (this.codes[codeIndex] == code)
                {
                    return MyDataAt(period, codeIndex);
                }
            }

            return null;
        }

        protected abstract object MyDataAt(int codeIndex, int period);

        public static AggregatedData CreateData(Type type)
        {
            switch (type)
            {
                case Type.DOUBLE:
                    return new AggregatedDouble();
                case Type.TIME:
                    return new AggregatedTime();
                case Type.SHORT:
                    return new AggregatedShort();
                default:
                    return null;
            }
        }
    }
}
