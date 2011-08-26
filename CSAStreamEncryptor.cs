using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CSA
{
    class CSAStreamEncryptor
    {
        private byte[] _data;

        private const uint _DVBCSA_KEYSBUFF_SIZE = 56;
        private const uint _DVBCSA_CWBITS_SIZE = 64;
        private const uint _DVBCSA_DATA_SIZE = 8;

        private CSAKeyStruct _ks;
        private int _len;

        public CSAStreamEncryptor(CSAKeyStruct ks, byte[] data_in, int len)
        {
            _data = data_in;

            _ks = ks;

            _len = len;
        }

        public void CSAStreamEncrypt()
        {
            int i = 0;

            ulong A, B;

            int pqzyx = 0;
            int cfed = 0;

            A = _ks.cw;
            B = (_ks.cw + 4);

            for (i = 0; i < 8; i++)
            {
                //csa_stream_init_round(iv[i], &A, &B, &pqzyx, &cfed);
                //csa_stream_init_round(swap_nbl(iv[i]), &A, &B, &pqzyx, &cfed);
                //csa_stream_init_round(iv[i], &A, &B, &pqzyx, &cfed);
                //csa_stream_init_round(swap_nbl(iv[i]), &A, &B, &pqzyx, &cfed);
            }

            for(i = 0; i < _len; i++)	/* 4 round = 1 stream byte */
            {
                //static const uint8_t csa_stream_out[16] =
	            //{
	                //0x00, 0x55, 0x55, 0x00, 0xaa, 0xff, 0xff, 0xaa,
	                //0xaa, 0xff, 0xff, 0xaa, 0x00, 0x55, 0x55, 0x00,
	            //};

                //csa_stream_round(&A, &B, &pqzyx, &cfed);
                //data[i] ^= csa_stream_out[GETD(cfed) & 0xf] & 0xc0;

                //csa_stream_round(&A, &B, &pqzyx, &cfed);
                //data[i] ^= csa_stream_out[GETD(cfed) & 0xf] & 0x30;

                //csa_stream_round(&A, &B, &pqzyx, &cfed);
                //data[i] ^= csa_stream_out[GETD(cfed) & 0xf] & 0x0c;

                //csa_stream_round(&A, &B, &pqzyx, &cfed);
                //data[i] ^= csa_stream_out[GETD(cfed) & 0xf] & 0x03;
            }
        }
    }
}
