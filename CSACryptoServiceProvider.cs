using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Security;
using System.Security.Cryptography;

namespace CSA
{
    public class CSACryptoServiceProvider
    {
        // variable defs
        private bool _enableBlock;
        private bool _enableStream;

        // constructor
        public CSACryptoServiceProvider(bool enableBlock, bool enableStream)
        {
            _enableBlock = enableBlock;
            _enableStream = enableStream;

        }
        public byte[] Encrypt(byte[] cw, byte[] data, int len)
        {
            // bypassed bitwise operation, not sure if it works
            // it seems to set alen to length from start of packet to end of last 8 byte block, leaving residue untouched
            // can probably do it in a more .NET way
            //uint alen = len & unchecked((uint)~0x7);

            int alen = len;  //TODO: implement length checking, this is only good for 184 byte packets (or multiples of 8 bytes)

            CSAKeyStruct ks = new CSAKeyStruct(cw);

            int i;

            if (len < 8)
                return data;

            // perform block cipher

            if (_enableBlock)
            {
                CSABlockEncryptor enc = new CSABlockEncryptor(ks, data, len);

                enc.CSABlockEncrypt(alen - 8); //(cw->sch, data + alen - 8, data + alen - 8);

                for (i = alen - 16; i >= 0; i -= 8)
                {
                    enc.CSAXor64(i); //(data + i, data + i + 8);
                    enc.CSABlockEncrypt(i); //(cw->sch, data + i, data + i);
                }
            }
            
            // perform stream cipher

            if (_enableStream)
            {
                // dvbcsa_stream_xor(key->cws, data, data + 8, len - 8);
            }

            return data;
        }

        public byte[] Decrypt(byte[] cw, byte[] data, int len)
        {
            // bypassed bitwise operation, not sure if it works
            // it seems to set alen to length from start of packet to end of last 8 byte block, leaving residue untouched
            // can probably do it in a more .NET way
            //uint alen = len & unchecked((uint)~0x7);

            int alen = len;  //TODO: implement length checking, this is only good for 184 byte packets (or multiples of 8 bytes)

            CSAKeyStruct ks = new CSAKeyStruct(cw);

            int i;

            if (len < 8)
                return data;

            // perform stream decipher

            if (_enableStream)
            {
                // dvbcsa_stream_xor(key->cws, data, data + 8, len - 8);
            }

            // perform block decipher

            if (_enableBlock)
            {
                CSABlockDecryptor enc = new CSABlockDecryptor(ks, data, len);

                enc.CSABlockDecrypt(0);

                for (i = 8; i < 183; i += 8) // this needs a look
                {
                    enc.CSAXor64(i);
                    enc.CSABlockDecrypt(i);
                }
            }

            return data;
        }

        

        

    }
}
