using System;
/* Copyright 2019 KLM Team Reimagineers
 *
 * Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), 
 * to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, 
 * and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:
 * 
 * The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.
 * 
 * THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, 
 * FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER 
 * LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS
 * IN THE SOFTWARE.
*/

using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUDP
{
    public struct XplaneDATAPacket : IXplanePacket
    {
        private byte[] header; // 5 bytes 
        private byte[] dataIndex; // 4 bytes
        private float[] data; // 32 bytes

        /// <summary>
        /// Request and/or write data packets. 
        /// For what the data represents, see XPlane docs. 
        /// If you do not want to change the data put -999
        /// </summary>
        /// <param name="index">Index of data packet, can be found in XPlane</param>
        /// <param name="data1"></param>
        /// <param name="data2"></param>
        /// <param name="data3"></param>
        /// <param name="data4"></param>
        /// <param name="data5"></param>
        /// <param name="data6"></param>
        /// <param name="data7"></param>
        /// <param name="data8"></param>
        public XplaneDATAPacket(byte index, float data1, float data2, float data3, float data4,
            float data5, float data6, float data7, float data8)
        {
            this.header = new byte[] { (byte)'D', (byte)'A', (byte)'T', (byte)'A', 0 };
            this.dataIndex = new byte[4];
            this.dataIndex[0] = index;

            this.data = new float[]
            {
                data1, data2, data3, data4, data5, data6, data7, data8
            };
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[41];
            
            Buffer.BlockCopy(header, 0, bytes, 0, header.Length);
            Buffer.BlockCopy(dataIndex, 0, bytes, header.Length, dataIndex.Length);
            
            for (int i = 0; i < data.Length; i++)
            {
                byte[] floatValue = BitConverter.GetBytes(data[i]);
                Buffer.BlockCopy(floatValue, 0, bytes, header.Length + dataIndex.Length + floatValue.Length * i, floatValue.Length);
            }
            
            return bytes;
        }
    }
}
