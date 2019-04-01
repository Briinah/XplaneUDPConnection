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

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XPlaneUDP
{
    public struct XplaneDREFPacket : IXplanePacket
    {
        private byte[] header; // 5 bytes
        private byte[] value; // 4 bytes
        private byte[] drefPath; // 500 bytes

        /// <summary>
        /// Write reference value to xplane
        /// </summary>
        /// <param name="value"></param>
        /// <param name="drefPath"></param>
        public XplaneDREFPacket(float value, string drefPath)
        {
            this.header = new byte[] { (byte)'D', (byte)'R', (byte)'E', (byte)'F', 0 };
            this.value = BitConverter.GetBytes(value);
            this.drefPath = new byte[500];

            // initialize name byte array with spaces
            for (int i = 0; i < this.drefPath.Length; i++)
            {
                this.drefPath[i] = (byte)' ';
            }

            // fill byte array with dataref name ending with a null terminator ( = char minvalue)
            var nameAsBytes = Encoding.ASCII.GetBytes(drefPath + char.MinValue);
            Buffer.BlockCopy(nameAsBytes, 0, this.drefPath, 0, nameAsBytes.Length);
        }

        public byte[] ToBytes()
        {
            byte[] bytes = new byte[509];

            Buffer.BlockCopy(header, 0, bytes, 0, header.Length);
            Buffer.BlockCopy(value, 0, bytes, 5, value.Length);
            Buffer.BlockCopy(drefPath, 0, bytes, 9, drefPath.Length);

            return bytes;
        }
    };

}
