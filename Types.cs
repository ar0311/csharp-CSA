using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CSA.Operations;


namespace CSA
{
    
    struct CSABlock
    {
        public byte b;
    }

    //struct CSACw
    //{
    //    public byte[] CSACw = new byte[8];
    //}

    //[StructLayout(LayoutKind.Auto)]
    //struct CSACw
    //{
    //    [FieldOffset(0)]
    //    public byte a;   // 1 byte
    //    [FieldOffset(1)]
    //    public byte b;   // 1 byte
    //    [FieldOffset(2)]
    //    public byte c;   // 1 byte
    //    [FieldOffset(3)]
    //    public byte d;   // 1 byte
    //    [FieldOffset(4)]
    //    public byte e;   // 1 byte
    //    [FieldOffset(5)]
    //    public byte f;   // 1 byte
    //    [FieldOffset(6)]
    //    public byte g;   // 1 byte
    //    [FieldOffset(7)]
    //    public byte h;   // 1 byte
    //}

    struct CSACw
    {

    }

    struct CSAKeyStruct
    {
        public UInt64 cw;
        public UInt64 cws;	/* nibble swapped CW */
        public byte[] sch = new byte[56]; // need to refactor to use global CSA keybuffsize

        //private  UInt64 _cw;

        // constructor
        public CSAKeyStruct(byte[] cw_in)
        {
            this.cw = cw_in.ToUInt64();
            this.cws = ((cw & 0xf0f0f0f0f0f0f0f0) >> 4) | ((cw & 0x0f0f0f0f0f0f0f0f) << 4);  // cw swapped;
            this.sch = Operations.Operations.CSAKeyScheduleBlock(cw_in);
            
        }

        
        
    }
    
    
    class Types
    {
    }
}
