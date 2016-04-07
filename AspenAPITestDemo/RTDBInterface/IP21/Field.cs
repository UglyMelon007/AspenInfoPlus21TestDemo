﻿using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;

namespace RTDB.IP21
{
    internal abstract class Field
    {
        protected object buffer;
        private GCHandle bufferHandle;
        protected ulong bufferSize;

        private string name;
        public Field(string name)
        {
            this.name = name;
        }

        public string Name
        {
            get { return this.name; }
        }

        public ulong Id
        {
            get { return Util.FieldId(this.name); }
        }

        public abstract ushort DataType
        {
            get;
        }

        public IntPtr AllocBuffer(ulong size)
        {
            this.bufferSize = size;
            this.buffer = AllocMyBuffer();
            this.bufferHandle = GCHandle.Alloc(this.buffer, GCHandleType.Pinned);
            return this.bufferHandle.AddrOfPinnedObject();
        }

        public void FreeBuffer()
        {
            this.bufferHandle.Free();
        }

        protected abstract object AllocMyBuffer();

        public abstract object DataAt(int index);

        public static Field CreateField(string name)
        {
            if (name == "IP_TREND_VALUE")
            {
                return new DoubleField(name);
            }
            else if (name == "IP_TREND_TIME")
            {
                return new TimeField(name);
            }
            else
            {
                return new DoubleField(name);
            }
        }
    }
}
