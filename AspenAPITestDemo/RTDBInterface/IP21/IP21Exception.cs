using System;
using System.Collections.Generic;
using System.Text;

namespace RTDB.IP21
{
    public class IP21Exception : Exception
    {
        private short errCode;
        private long err1;
        private long err2;

        public IP21Exception(string msg) : base(msg)
        {
        }

        public short ErrCode
        {
            get { return this.errCode; }
            set { this.errCode = value; }
        }

        public long Err1
        {
            get { return this.err1; }
            set { this.err1 = value; }
        }

        public long Err2
        {
            get { return this.err2; }
            set { this.err2 = value; }
        }
    }
}
